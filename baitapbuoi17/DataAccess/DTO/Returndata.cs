using baitapbuoi17.Models;

namespace baitapbuoi17.DataAccess.DTO
{
    public class Returndata
    {
        public int returnCode {  get; set; }
        public string returnMsg { get; set; }
    }
    public class ReturndataReturnProduct:Returndata {
        public Products Products { get; set; }
        public List<Products> ListProducts { get; set; }   
    }
}
