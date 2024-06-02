using Clean_Architecture.Application.Services.Products.Commands.AddNewCategory;
using Clean_Architecture.Application.Services.Products.Commands.AddNewProduct;
using Clean_Architecture.Application.Services.Products.Commands.DeleteProductForAdmin;
using Clean_Architecture.Application.Services.Products.Commands.CategoryStatusChange;
using Clean_Architecture.Application.Services.Products.Queries.GetAllCategories;
using Clean_Architecture.Application.Services.Products.Queries.GetCategories;
using Clean_Architecture.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin;
using Clean_Architecture.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Application.Services.Products.Commands.ChangeDisplayed;
using Clean_Architecture.Application.Services.Products.Commands.EditCategory;
using Clean_Architecture.Application.Services.Products.Commands.DeleteCategory;
using Clean_Architecture.Application.Services.Products.Queries.GetCategoriesForAdmin;

using Clean_Architecture.Application.Services.Products.Queries.GetProduct_sameCatForAdminService;

namespace Clean_Architecture.Application.Interface.Facadpatter
{
    public interface IProductFacadAdmin
    {
        //Category
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        GetAllCategoriesService GetAllCategoriesService { get; }
        CategoryStatusChangeService CategoryStatusChangeService { get; }
        EditCategoryService EditCategoryService { get; }
        DeleteCategoryService DeleteCategoryService { get; }
        GetCategoriesForAdmin GetCategoriesForAdmin { get; }


        //Product
        AddNewProductService AddNewProductService { get; }
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IDeleteProductForAdminService DeleteProductForAdminService { get; }
        ChangeDisplayedService ChangeDisplayedService { get; }
        GetProduct_sameCatForAdminService GetProduct_SameCatForAdminService { get; }
    }
}