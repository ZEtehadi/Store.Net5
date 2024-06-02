using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin
{
    public class ProductForAdminDto
   {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ProductForAdminList_Dto> Products { get; set; }
    }
}
