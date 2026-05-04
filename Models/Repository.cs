namespace FormApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _product = new();
        private static readonly List<Category> _category = new();

        static Repository()
        {
            _category.Add(new Category {CategoryId = 1, CategoryName = "Telefon"});
            _category.Add(new Category {CategoryId = 2, CategoryName = "Bilgisayar"});

            _product.Add(new Product {ProductId = 1, Name = "Samsung Galaxys s25 Ultra", Price = 80000, Image = "samsung_s25_u.jpg", CategoryId = 1, IsActive = true});
            _product.Add(new Product {ProductId = 2, Name = "Iphone 16e", Price = 40000, Image = "iphone_16e.jpg", CategoryId = 1, IsActive = true});
            _product.Add(new Product {ProductId = 3, Name = "Iphone 17e", Price = 60000, Image = "iphone_17e.jpg", CategoryId = 1, IsActive = true});
            _product.Add(new Product {ProductId = 4, Name = "Macbook Air M1 512GB 8GB", Price = 45000, Image = "air.jpg", CategoryId = 2, IsActive = true});
            _product.Add(new Product {ProductId = 5, Name = "Macbook Pro M1 Pro 1TB 32GB", Price = 120000, Image = "pro.jpg", CategoryId = 2, IsActive = true});
        }
        public static List<Product> GetProducts
        {
            get
            {
                return _product;
            }
        }

        public static List<Category> GetCategories
        {
            get
            {
                return _category;
            }
        }

        public static void CreateProduct(Product entity)
        {
            _product.Add(entity);
        }
    }
}