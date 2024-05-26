using baitapbuoi17.DataAccess.DBContext;
using baitapbuoi17.DataAccess.DTO;
using baitapbuoi17.DataAccess.IProductsevices;
using baitapbuoi17.Models;

namespace baitapbuoi17.DataAccess.Productsevices
{
    public class ProductServices : IProductServices
    {
        private ProductDBcontext dbContext=new ProductDBcontext();
        public Returndata Addproduct(Products products)
        {
            Returndata result = new Returndata();
            try
            {
                if (products == null
                    ||products.ProductName==null
                    ||products.ProductType==null
                    ||products.Price<=0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if (checkInput.CheckIsNullOrWhiteSpace(products.ProductName)==true
                    ||checkInput.CheckIsNullOrWhiteSpace(products.ProductType)==true
                    ||checkInput.ContainsNumber(products.ProductName)==true)
                {

                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if (dbContext.Products.Find(products.ProductID) != null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                dbContext.Products.Add(products);
                dbContext.SaveChanges();
                result.returnCode = 1;
                result.returnMsg = "Thêm sản phẩm thành công!";
                return result;
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }

        public Returndata Deleteproduct(int ProductID)
        {
            Returndata result = new Returndata();
            try
            {
                if (ProductID <= 0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if(dbContext.Products.Find(ProductID)==null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                var p = dbContext.Products.Find(ProductID);
                dbContext.Products.Remove(p);
                dbContext.SaveChanges();
                result.returnCode = 1;
                result.returnMsg = "xóa sản phẩm thành công!";
                return result;
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }

        public ReturndataReturnProduct GetAllproduct()
        {
            ReturndataReturnProduct result = new ReturndataReturnProduct();
            try
            {
                result.ListProducts = dbContext.Products.ToList();
                result.returnCode = 1;
                result.returnMsg="Lấy danh sách sản phâmt thành công!";
                return result;    
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }

        public ReturndataReturnProduct Getproduct(int ProductID)
        {
            ReturndataReturnProduct result = new ReturndataReturnProduct();
            try
            {
                if(ProductID <= 0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if(dbContext.Products.Find(ProductID)==null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                result.Products = dbContext.Products.Find(ProductID);
                result.returnCode = 1;
                result.returnMsg = "đã tìm thấy sản phẩm !";
                return result;
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }


        public Returndata Updateproduct(Products products)
        {
            Returndata result = new Returndata();
            try
            {
                if (products == null
                    || products.ProductID<=0
                    || products.ProductName == null
                    || products.ProductType == null
                    || products.Price <= 0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if (checkInput.CheckIsNullOrWhiteSpace(products.ProductName) == true
                    || checkInput.CheckIsNullOrWhiteSpace(products.ProductType) == true
                    || checkInput.ContainsNumber(products.ProductName) == true)
                {

                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if (dbContext.Products.Find(products.ProductID) == null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                dbContext.Products.Update(products);
                dbContext.SaveChanges();
                result.returnCode = 1;
                result.returnMsg = "cập nhật sản phẩm thành công!";
                return result;
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }
    }
}
