using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Services.Products.Commands.AddNewCategory;
using Clean_Architecture.Application.Services.Products.Commands.AddNewProduct;
using Clean_Architecture.Application.Services.Products.Commands.DeleteProductForAdmin;
using Clean_Architecture.Application.Services.Products.Commands.CategoryStatusChange;
using Clean_Architecture.Application.Services.Products.Queries.GetAllCategories;
using Clean_Architecture.Application.Services.Products.Queries.GetCategories;
using Clean_Architecture.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin;
using Clean_Architecture.Common.UploadFile;
using Microsoft.AspNetCore.Hosting;
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

namespace Clean_Architecture.Application.Services.Products.FacadPatern
{
    public class ProductFacadAdmin:IProductFacadAdmin
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly UploadFilee _uploadFilee;

        public ProductFacadAdmin(IDataBaseContext context,IHostingEnvironment environment,UploadFilee uploadFilee)
        {
            _context = context;
            _environment = environment;
            _uploadFilee = uploadFilee;
        }


        /// Product

        ////Adding GetProductForAdmin
        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }


        ////Adding GetProductDetailForAdmin
        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService = _getProductDetailForAdminService ?? new GetProductDetailForAdminService(_context);
            }
        }


        ////Adding AddNewProduct Service
        private AddNewProductService _addNewProductService;
        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService = _addNewProductService ?? new AddNewProductService(_context, _uploadFilee, _environment);
            }
        }


        ////Adding ChangeDisplayed Service Product
        private ChangeDisplayedService _changeDisplayedService;
        public ChangeDisplayedService ChangeDisplayedService
        {
            get
            {
                return _changeDisplayedService = _changeDisplayedService ?? new ChangeDisplayedService(_context);
            }
        }



        ////Adding DeleteProductForAdmin
        private IDeleteProductForAdminService _deleteProductForAdminService;
        public IDeleteProductForAdminService DeleteProductForAdminService
        {
            get
            {
                return _deleteProductForAdminService = _deleteProductForAdminService ?? new DeleteProductForAdminService(_context);
            }
        } 
         
        
       
        
        private GetProduct_sameCatForAdminService _getProduct_SameCatForAdminService;
        public GetProduct_sameCatForAdminService GetProduct_SameCatForAdminService
        {
            get
            {
                return _getProduct_SameCatForAdminService = _getProduct_SameCatForAdminService ?? new GetProduct_sameCatForAdminService(_context);
            }
        } 
        
        
        
       







        //Category


        ////Adding AddNewCategory Service
        private AddNewCategoryService _addNewCategory;
        public AddNewCategoryService AddNewCategoryService
        {
            get
            {
                //if(_addNewCategory==Null) => AddNewCategorySrvice(_context)
                return _addNewCategory = _addNewCategory ?? new AddNewCategoryService(_context, _uploadFilee);
            }
        }



        ///Adding GetCategories Service
        private IGetCategoriesService _getCategoriesService; 
        public IGetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategorisService(_context);
            }
        }
    


        ////Adding GetAllCategories Service
        private GetAllCategoriesService _getAllCategoriesService;
        public GetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }




        ////Adding CategoryStatusChange Service
        private CategoryStatusChangeService _categoryStatusChangeService;
        public CategoryStatusChangeService CategoryStatusChangeService
        {
            get
            {
                return _categoryStatusChangeService = _categoryStatusChangeService ?? new CategoryStatusChangeService(_context);
            }
        }



        ////Adding EditCategory Service
        private EditCategoryService _editCategoryService;
        public EditCategoryService EditCategoryService
        {
            get
            {
                return _editCategoryService = _editCategoryService ?? new EditCategoryService(_context,_uploadFilee);
            }
        }



        ////Adding DeleteCategory Service
        private DeleteCategoryService _deleteCategoryService;
        public DeleteCategoryService DeleteCategoryService
        {
            get
            {
                return _deleteCategoryService = _deleteCategoryService ?? new DeleteCategoryService(_context);
            }
        }  
        
        
       
        private GetCategoriesForAdmin _getCategoriesForAdmin;
        public GetCategoriesForAdmin GetCategoriesForAdmin
        {
            get
            {
                return _getCategoriesForAdmin = _getCategoriesForAdmin ?? new GetCategoriesForAdmin(_context);
            }
        }




    }
}
