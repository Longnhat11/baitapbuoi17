using System.ComponentModel.DataAnnotations;

namespace baitapbuoi17.Models
{
    public class GroupAttribute
    {
        [Key]public int GroupAttributeID { get;set;}
        public string GroupAttributeName { get;set;}
        public int ProductID { get;set; }
    }
}
