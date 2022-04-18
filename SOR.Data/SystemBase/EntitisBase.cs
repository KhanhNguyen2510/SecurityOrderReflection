using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.SystemBase
{

    public class EntitisBase
    {
        public bool IsDelete { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TimeDelete { get; set; }
        public string UpdateUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate
        {
            get { return CreateDate; }
            set { CreateDate = DateTime.Now; }
        }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate
        {
            get { return UpdateDate; }
            set { UpdateDate = DateTime.Now; }
        }

        public string CreateUser { get; set; }
    }
}
