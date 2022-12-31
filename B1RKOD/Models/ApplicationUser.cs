using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B1RKOD.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string İlce { get; set; }
        public string PostaKodu { get; set; }
        [NotMapped]
        public string Role { get; set; }

    }
}
