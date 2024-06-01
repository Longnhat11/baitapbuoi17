using System.ComponentModel.DataAnnotations;

namespace baitapbuoi17.Models
{
    public class Orders
    {
        [Key]public int OrderId { get; set; }
        public int LoginID{ get; set; }
        public int Quantity { get; set; }

    }
}
