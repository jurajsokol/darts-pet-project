using System.ComponentModel.DataAnnotations;

namespace Darts.DAL.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; } = string.Empty;
    }
}
