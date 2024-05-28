using baitapbuoi17.DataAccess.DTO;
using baitapbuoi17.Models;

namespace baitapbuoi17.DataAccess.IProductsevices
{
    public interface IProductServices
    {
        Task<List<Products>> GetProducts();
        Task<ReturnDataReturnProduct> AddProduct(ProductAddUpdateRequestData productAddUpdateRequestData);
        Task<ReturnDataReturnProduct> UpdateProduct(ProductAddUpdateRequestData requestData);
        Task<ReturnDataReturnProduct> DeleteProduct(ProductAddUpdateRequestData requestData);
    }
}
