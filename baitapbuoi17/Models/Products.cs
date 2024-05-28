using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace baitapbuoi17.Models
{
    public class Products
    {
       [Key]public int ProductID { set;get; }
            public string ProductName {  set;get; }
            public int CategoryID { set;get; }
    }
}
