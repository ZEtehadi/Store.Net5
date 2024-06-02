namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin
{
    public class ProductForAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int  Inventory { get; set; }
        public int  Price { get; set; }
        public bool Displayed { get; set; }
    }
}
