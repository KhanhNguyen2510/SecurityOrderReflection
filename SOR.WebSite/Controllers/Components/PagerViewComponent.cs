using Microsoft.AspNetCore.Mvc;
using SOR.ViewModel.Common;
using System.Threading.Tasks;

namespace SOR.WebSite.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
