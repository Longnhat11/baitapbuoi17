using baitapbuoi17.Models;

namespace baitapbuoi17.DataAccess.DTO
{
    public class ReturnData
    {
        public int returnCode {  get; set; }
        public string returnMsg { get; set; }
    }
    public class ReturnDataReturnProduct: ReturnData
    {
        public Products Products { get; set; }
    }
}
