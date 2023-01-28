using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Appbay.Areas.Manage.ViewModel
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100,MinimumLength =8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
