using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using baitapbuoi17.DataAccess.DBContext;
using baitapbuoi17.Models;
using baitapbuoi17.DataAccess.IProductsevices;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.InkML;
using OfficeOpenXml;

namespace baitapbuoi17.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDBcontext dBcontext=new ProductDBcontext();
        private readonly IProductServices _services;

        public ProductsController(IProductServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult>AddProduct(Products product)
        {
            if (ModelState.IsValid)
            {
                _services.Addproduct(product);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Products product)
        {
            if (ModelState.IsValid)
            {
                if (_services.Getproduct(product.ProductID).Products != null)
                {
                    _services.Updateproduct(product);

                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
        [HttpGet]
        public IActionResult GetProducts(int ProductID)
        {
            var products = _services.Getproduct(ProductID); 
            return Json(products);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = _services.Getproduct(productId);
            if (product != null)
            {
                _services.Deleteproduct(productId);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
        }
        public IActionResult ExportProductsToExcel()
        {
            var products = dBcontext.Products.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách sản phẩm");
                worksheet.Cells.LoadFromCollection(products, true);

                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachSanPham.xlsx");
            }
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products =  _services.GetAllproduct();
            return Json(products);
        }
    }
}
