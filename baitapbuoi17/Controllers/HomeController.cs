using baitapbuoi17.DataAccess.DBContext;
using baitapbuoi17.DataAccess.DTO;
using baitapbuoi17.DataAccess.IProductsevices;
using baitapbuoi17.DataAccess.Productsevices;
using baitapbuoi17.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Diagnostics;

namespace baitapbuoi17.Controllers
{
    public class HomeController : Controller
    {
       
        private ProductDBcontext dBcontext = new ProductDBcontext();
        private readonly IProductServices _services;

        public HomeController(IProductServices services)
        {
            _services = services;
        }
        public async Task<JsonResult>AddProduct(ProductAddUpdateRequestData requestData)
        {
            var model = new ReturnData();
            try
            {
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.ProductName))
                {
                    model.returnCode = -1;
                    model.returnMsg = "Dữ liệu không được trống";
                    return Json(model);
                }

                var rs = await new ProductServices().AddProduct(requestData);

                model.returnCode = rs.returnCode;
                model.returnMsg = rs.returnMsg;
                return Json(model);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(model);
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

        public async Task<JsonResult> UpDateProduct(ProductAddUpdateRequestData requestData)
        {
            var model = new ReturnData();
            try
            {
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.ProductName))
                {
                    model.returnCode = -1;
                    model.returnMsg = "Dữ liệu không được trống";
                    return Json(model);
                }

                var rs = await new ProductServices().UpdateProduct(requestData);

                model.returnCode = rs.returnCode;
                model.returnMsg = rs.returnMsg;
                return Json(model);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(model);
        }
        public async Task<JsonResult> GetProduct()
        {
            var model = await new ProductServices().GetProducts();
            return Json(model);
        }
        public async Task<JsonResult> DeleteProduct(ProductAddUpdateRequestData requestData)
        {
            var model = new ReturnData();
            try
            {
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.ProductName))
                {
                    model.returnCode = -1;
                    model.returnMsg = "Dữ liệu không được trống";
                    return Json(model);
                }

                var rs = await new ProductServices().DeleteProduct(requestData);

                model.returnCode = rs.returnCode;
                model.returnMsg = rs.returnMsg;
                return Json(model);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(model);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công.");
            }
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
