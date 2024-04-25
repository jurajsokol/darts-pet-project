using System.ComponentModel.DataAnnotations;

namespace Darts.DAL.Entities
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
