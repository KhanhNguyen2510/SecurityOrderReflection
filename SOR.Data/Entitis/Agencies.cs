using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;

namespace SOR.Data.Entitis
{
    public class Agencies : EntitisBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Trụ sở
        /// </summary>
        public string Office { get; set; }
        public string NumberPhone { get; set; }
        public User User { get; set; }
    }
}
