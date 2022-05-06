﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SOR.IntergrationAPI.Catalogs.User;
using SOR.ViewModel.Catalogs.Users;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using SOR.WebSite.Models;

namespace SOR.WebSite.Controllers
{

    public class AuthenticationController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;

        public AuthenticationController(IUserApiClient userApiClient,
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _accessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(GetLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);
                var IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                
                var gUser = await _userApiClient.LoginInWed(request);

                var userPrincipal = this.ValidateToken(gUser);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);
              
                if (gUser == null)
                {
                    ViewBag.SuccessMsg = MessageText.AuthenticateFailed();
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.SuccessMsg = MessageText.AuthenticateFailed();
                return View();
            }
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Registration(GetCreateToUserRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var IP = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var dUser = new GetCreateToUserRequest()
            {
                agenciesId = request.agenciesId,
                iPCreate = IP,
                confirmPassword = request.confirmPassword,
                email = request.email,
                fullName = request.fullName,
                gender = request.gender,
                identification = request.identification,
                numberPhone = request.numberPhone,
                password = request.password,
                userName = User.Identity.Name,  
            };


            var gUser = await _userApiClient.Create(dUser);

            TempData["result"] = gUser.Message;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"].ToString();
            }
            return View();
        }
    }
}
