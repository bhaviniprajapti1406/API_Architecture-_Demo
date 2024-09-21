using System.ComponentModel.DataAnnotations;

namespace Cumulus.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
