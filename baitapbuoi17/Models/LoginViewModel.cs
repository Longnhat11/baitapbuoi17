using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace baitapbuoi17.Models
{
    public class LoginViewModel
    {
        [Key]public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
