using System.ComponentModel.DataAnnotations;

namespace Darts.DAL.Entities
{
    public class Player
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
