using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class City:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;
    }
}
