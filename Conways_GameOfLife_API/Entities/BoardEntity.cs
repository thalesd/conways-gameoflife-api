using System.ComponentModel.DataAnnotations;

namespace Conways_GameOfLife_API.Entities
{
    public class BoardEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string GridJson { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
