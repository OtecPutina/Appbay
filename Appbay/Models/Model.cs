using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appbay.Models
{
    public class Model
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string? Name { get; set; }
        [StringLength(maximumLength:100)]
        public string? Description { get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
