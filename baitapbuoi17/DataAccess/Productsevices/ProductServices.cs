using baitapbuoi17.DataAccess.DBContext;
using baitapbuoi17.DataAccess.DTO;
using baitapbuoi17.DataAccess.IProductsevices;
using baitapbuoi17.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;

namespace baitapbuoi17.DataAccess.Productsevices
{
    public class ProductServices : IProductServices
    {
        private ProductDBcontext dbContext=new ProductDBcontext();
        public async Task<List<Products>> GetProducts()
        {
            try
            {
                return dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnDataReturnProduct> AddProduct(ProductAddUpdateRequestData productAddUpdateRequestData)
        {
            ReturnDataReturnProduct result = new ReturnDataReturnProduct();
            var errItem = string.Empty;
            try
            {
               
                if ( productAddUpdateRequestData==null
                    ||productAddUpdateRequestData.ProductName==null
                    ||productAddUpdateRequestData.CategoryId<=0
                    ||productAddUpdateRequestData.GroupAttributesValue==null
                    ||productAddUpdateRequestData.AttributesValue==null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if (checkInput.CheckIsNullOrWhiteSpace(productAddUpdateRequestData.ProductName)
                    ||checkInput.CheckIsNullOrWhiteSpace(productAddUpdateRequestData.GroupAttributesValue)
                    ||checkInput.CheckIsNullOrWhiteSpace(productAddUpdateRequestData.AttributesValue)
                    ||checkInput.ContainsNumber(productAddUpdateRequestData.ProductName))
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                //check trung
                var product = dbContext.Products.ToList().Where(x=>x.ProductName==productAddUpdateRequestData.ProductName);
                if(product != null ) {
                    result.returnCode = -1;
                    result.returnMsg = "Tên sản phẩm đã tồn tại!";
                    return result;
                }
                // them product
                var productAdd = new Products
                {
                    ProductName = productAddUpdateRequestData.ProductName,
                    CategoryID = productAddUpdateRequestData.CategoryId
                };
                //them nhom thuoc tinh
                var GroupAtri_cout= productAddUpdateRequestData.GroupAttributesValue.Split('_').Length;
                for(int i = 0;i< GroupAtri_cout;i++)
                {
                    var item = productAddUpdateRequestData.GroupAttributesValue.Split('_')[i];

                    var Grattr_name = item.Split(',')[0];
                    // kiểm tra xem null 

                    if (string.IsNullOrEmpty(Grattr_name))
                    {
                        errItem += "tên thuộc tính bị trống hoặc không hợp lệ ";
                        continue;
                    }
                    

                    var GroupAttr_Req = new GroupAttribute
                    {
                        GroupAttributeName = Grattr_name,
                        ProductID = productAddUpdateRequestData.ProductId,
                    };

                    dbContext.groupAttributes.Add(GroupAttr_Req);

                }
                //them thuoc tinh
                var attr_count = productAddUpdateRequestData.AttributesValue.Split('_').Length;

                for (int i = 0; i < attr_count; i++)
                {
                    var item = productAddUpdateRequestData.AttributesValue.Split('_')[i];

                    var attr_name = item.Split(',')[0];
                    var attr_Price = item.Split(',')[2];

                    var attr_priceSale = item.Split(',')[3];
                    var attr_Quantity = item.Split(',')[1];

                    // kiểm tra xem null 

                    if (string.IsNullOrEmpty(attr_name))
                    {
                        errItem += "tên thuộc tính bị trống hoặc không hợp lệ ";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_Quantity))
                    {
                        errItem += "thuộc tính số lượng bị trống";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_Price))
                    {
                        errItem += " thuộc tính giá bị trống";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_priceSale))
                    {
                        errItem += " thuộc tính giá sale bị trống";
                        continue;
                    }

                    var attr_Req = new Attributes
                    {
                        AttributeName = attr_name,
                        Quantity = Convert.ToInt32(attr_Quantity),
                        Price = Convert.ToInt32(attr_Price),
                        Pricesale = Convert.ToInt32(attr_priceSale),
                    };

                    dbContext.attributes.Add(attr_Req);
                }
                dbContext.SaveChangesAsync();

                result.returnCode = 1;
                result.returnMsg = "Thêm sản phẩm thành công";
                return result;
                

            }
            catch ( Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }

        public async Task<ReturnDataReturnProduct> UpdateProduct(ProductAddUpdateRequestData requestData)
        {
            ReturnDataReturnProduct result = new ReturnDataReturnProduct();
            var errItem = string.Empty;
            try
            {
                if (requestData == null
                   || requestData.ProductId<=0
                   || requestData.ProductName == null
                   || requestData.CategoryId <= 0
                   || requestData.GroupAttributesValue == null
                   || requestData.AttributesValue == null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if(checkInput.CheckIsNullOrWhiteSpace(requestData.ProductName)
                    || checkInput.CheckIsNullOrWhiteSpace(requestData.GroupAttributesValue)
                    || checkInput.CheckIsNullOrWhiteSpace(requestData.AttributesValue)
                    || checkInput.ContainsNumber(requestData.ProductName))
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                //check trung
                var product = dbContext.Products.Find(requestData.ProductName);
                if (product == null || product.ProductID < 0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Tên sản phẩm khong tồn tại!";
                    return result;
                }
                // them product
                var productAdd = new Products
                {
                    ProductName = requestData.ProductName,
                    CategoryID = requestData.CategoryId
                };
                //them nhom thuoc tinh
                var GroupAtri_cout = requestData.GroupAttributesValue.Split('_').Length;
                for (int i = 0; i < GroupAtri_cout; i++)
                {
                    var item = requestData.GroupAttributesValue.Split('_')[i];

                    var Grattr_name = item.Split(',')[0];
                    // kiểm tra xem null 

                    if (string.IsNullOrEmpty(Grattr_name))
                    {
                        errItem += "tên nhom thuộc tính bị trống hoặc không hợp lệ ";
                        continue;
                    }


                    var GroupAttr_Req = new GroupAttribute
                    {
                        GroupAttributeName = Grattr_name,
                        ProductID =requestData.ProductId,
                    };

                    dbContext.Update(GroupAttr_Req);

                }
                //them thuoc tinh
                var attr_count = requestData.AttributesValue.Split('_').Length;

                for (int i = 0; i < attr_count; i++)
                {
                    var item = requestData.AttributesValue.Split('_')[i];

                    var attr_name = item.Split(',')[0];
                    var attr_Price = item.Split(',')[1];

                    var attr_priceSale = item.Split(',')[2];
                    var attr_Quantity = item.Split(',')[3];

                    // kiểm tra xem null 

                    if (string.IsNullOrEmpty(attr_name))
                    {
                        errItem += "tên thuộc tính bị trống hoặc không hợp lệ ";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_Quantity))
                    {
                        errItem += "thuộc tính số lượng bị trống";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_Price))
                    {
                        errItem += " thuộc tính giá bị trống";
                        continue;
                    }

                    if (string.IsNullOrEmpty(attr_priceSale))
                    {
                        errItem += " thuộc tính giá sale bị trống";
                        continue;
                    }

                    var attr_Req = new Attributes
                    {
                        AttributeName = attr_name,
                        Quantity = Convert.ToInt32(attr_Quantity),
                        Price = Convert.ToInt32(attr_Price),
                        Pricesale = Convert.ToInt32(attr_priceSale),
                    };

                    dbContext.Update(attr_Req);
                }
                dbContext.SaveChanges();

                result.returnCode = 1;
                result.returnMsg = "Thêm sản phẩm thành công";
                return result;
            }
            catch (Exception ex)
            {
                result.returnCode = -1;
                result.returnMsg = "Hệ thống đang bận:" + ex.Message;
                return result;
            }
        }

        public async Task<ReturnDataReturnProduct> DeleteProduct(ProductAddUpdateRequestData requestData)
        {
            ReturnDataReturnProduct result = new ReturnDataReturnProduct();
            var errItem = string.Empty;
            try
            {
                if (requestData == null
                    || requestData.ProductId < 0)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                if(dbContext.Products.Find(requestData.ProductId)==null)
                {
                    result.returnCode = -1;
                    result.returnMsg = "Dữ liệu vào không hợp lệ!";
                    return result;
                }
                var p = dbContext.Products.Find(requestData.ProductId);
                dbContext.Products.Remove(p);
                result.returnMsg = "xóa thành công sản phẩm !";
                result.returnCode = 1;
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
