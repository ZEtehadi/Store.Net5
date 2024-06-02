using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using Clean_Architecture.Common.UploadFile;

namespace Clean_Architecture.Application.Services.Products.Commands.AddNewProduct
{
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;
        private readonly IHostingEnvironment _environment;
        public AddNewProductService(IDataBaseContext context, UploadFilee uploadFilee, IHostingEnvironment environment)
        {
            _context = context;
            _uploadFilee = uploadFilee;
            _environment = environment;
        } 


        public ResultDto Execute(RequestAddNewProductDto request)
        {
            try
            {
                var Category = _context.Categories.Find(request.CategoryId);

                //Create One Product
                Product product = new Product()
                {
                    Name = request.Name,
                    Brand = request.Brand,
                    Description = request.Description,
                    Inventory = request.Inventory,
                    Displayed = request.Displayed,
                    Category = Category,
                    Price = request.Price,
                };
                _context.Products.Add(product);


                //Get Images
                List<ProductImage> ProductImages = new List<ProductImage>();
                foreach (var item in request.Images)
                {
                    var UploadResult = _uploadFilee.UploadFileMethod(item, @"ProductImages\");
                    ProductImages.Add(new ProductImage()
                    {
                        Product = product,
                        Src = UploadResult.FileNameAddress
                    });
                }
                _context.ProductImages.AddRange(ProductImages);
                //Get Features
                List<ProductFeatures> productFeatures = new List<ProductFeatures>();
                foreach (var item in request.Featurese)
                {
                    productFeatures.Add(new ProductFeatures()
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "محصول با موفقیت به فروشگاه اضافه شد"
                };
            }
            catch
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "خطا رخ داد"
                };
            }
        }
        //   ProductImages\
        //private UploadDto UploadFile(IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        string folder = $@"images\ProductImages\";
        //        var UploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

        //        if (!Directory.Exists(UploadsRootFolder))//if dont have this folder
        //        {
        //            Directory.CreateDirectory(UploadsRootFolder);//then Create folder
        //        }
        //        if (file == null || file.Length==0 )
        //        {
        //            return new UploadDto()
        //            {
        //                Status=false,
        //                FileNameAddress="",
        //            };
        //        }

        //        string FileName = DateTime.Now.Ticks.ToString() + file.FileName;
        //        var filePath = Path.Combine(UploadsRootFolder, FileName);
        //        using (var FileSteam = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(FileSteam);
        //        }

        //        return new UploadDto()
        //        {
        //            FileNameAddress=folder+FileName,
        //            Status=true,
        //        };
        //    }
        //    return null;
        //}

    }

}