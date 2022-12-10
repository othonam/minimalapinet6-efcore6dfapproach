using System.ComponentModel.DataAnnotations;

namespace Minimal.Domain.Entities
{
    public class Costumer
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Name")]
        [StringLength(25, MinimumLength = 3,
        ErrorMessage = "Name must be between 3 and 25 characters in length.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Last Name")]
        [StringLength(75, MinimumLength = 3,
        ErrorMessage = "Last Name must be between 3 and 75 characters in length.")]
        public string LastName { get; set; }
    }
}
