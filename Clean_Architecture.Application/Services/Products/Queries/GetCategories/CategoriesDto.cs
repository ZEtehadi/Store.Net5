namespace Clean_Architecture.Application.Services.Products.Queries.GetCategories
{
    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }
        public bool IsActive { get; set; }
    }
}
