using System.ComponentModel.DataAnnotations;

namespace RESTful_Web_API.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }

    }
}
