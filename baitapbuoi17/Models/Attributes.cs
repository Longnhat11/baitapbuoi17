using System.ComponentModel.DataAnnotations;

namespace baitapbuoi17.Models
{
    public class Attributes
    {
        [Key]public int AttributeID{ get; set; }
        public int GroupAttributeID { get; set; }
        public string AttributeName { get; set; }
        public decimal Price { get; set; }
        public decimal Pricesale { get; set; }
        public int Quantity{ get; set; }
    }
}
