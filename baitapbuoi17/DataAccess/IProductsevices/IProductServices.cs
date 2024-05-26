using baitapbuoi17.DataAccess.DTO;
using baitapbuoi17.Models;

namespace baitapbuoi17.DataAccess.IProductsevices
{
    public interface IProductServices
    {
        public Returndata Addproduct(Products products);
        public Returndata Updateproduct(Products products);
        public Returndata Deleteproduct(int ProductID);
        public ReturndataReturnProduct Getproduct(int ProductID);
        public ReturndataReturnProduct GetAllproduct();
    }
}
