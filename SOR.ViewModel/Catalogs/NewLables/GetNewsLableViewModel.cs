using System;

namespace SOR.ViewModel.Catalogs.NewLables
{
    public class GetNewsLableViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
