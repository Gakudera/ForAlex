using Guschin.GraduateProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guschin.GraduateProject.ViewModels
{
    public class ProductWindowViewModel:ViewModelBase
    {
        private Product _product;
        private List<ProductType> _productTypes;
        private List<Product> _products;
        private List<Product> _displayingProducts;
        private string _searchValue;

        private bool _isNew;

        public Product Product
        {
            get => _product;
            set => Set(ref _product, value, nameof(Product));
        }

        public List<ProductType> ProductTypes
        {
            get => _productTypes;
            set => Set(ref _productTypes, value, nameof(ProductTypes));
        }

        public List<Product> DisplayingProducts 
        { 
            get => _displayingProducts; 
            set => Set(ref _displayingProducts, value, nameof(DisplayingProducts));
        }

        public string SearchValue
        {
            get => _searchValue;
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayProducts();
            }
        }

        public ProductWindowViewModel(Guid? productId)
        {
            using(ApplicationDbContext context= new())
            {
                 ProductTypes=context.ProductTypes.ToList();
                _products = context.Products.ToList();

            }

            if(productId == null)
            {
                _isNew = true;
                _product = new Product();
                return;

            }

            Product = GetProduct((Guid)productId);
            DisplayProducts();
        }

        public Product GetProduct(Guid  productId)
        {
            using (ApplicationDbContext context = new())
            {
                return context.Products
                    .Include(p => p.ProductType)
                    .Include(p => p.Chat)
                    .Single(p => p.Id == productId);
            }
        }

        private void DisplayProducts()
        {
            DisplayingProducts = Search(_products); 
        }

        public List<Product> Search(List<Product> products)
        {
            if (SearchValue == null || SearchValue == string.Empty)
                return products;
            else
                return products.Where(p => p.ProductName.ToLower().Contains(SearchValue.ToLower())).ToList();
        }
    }
}
