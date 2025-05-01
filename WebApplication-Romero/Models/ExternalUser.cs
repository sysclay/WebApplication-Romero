using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Romero.Models
{
    public class ExternalUser
    {
        [Key]
        public int id { get; set; }
        public required string name { get; set; }
        public required string username { get; set; }
        public required string email { get; set; }
        public Address address { get; set; }
        public required string phone { get; set; }
        public required string website { get; set; }
        public Company company { get; set; }
    }
}
