using System;

namespace SOR.ViewModel.Catalogs.Agencies
{
    public class GetAgenciesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public string NumberPhone { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
