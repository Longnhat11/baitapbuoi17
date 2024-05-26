using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace baitapbuoi17.Models
{
    public class Variants
    {
       [Key]public int VariantID {  get; set; }
       public int ProductID {  get; set; }
       public string DetailSize {  get; set; }
       public string Color {  get; set; }
    }
}
