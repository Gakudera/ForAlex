using Guschin.GraduateProject.Commands;
using Guschin.GraduateProject.Entities;
using Guschin.GraduateProject.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Guschin.GraduateProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region locals

        private Product _selectedProduct;
        private List<Product> _products;
        private List<Product> _displayingProducts;
        private string _selectedSort;
        private string _selectedFilter;
        private string _searchValue;


        /* 
        private list _
        public list __ {get; set => set(_, vlaue, nameof(__));}
         
         */

        public List<Product> DisplayingProducts
        {
            get => _displayingProducts;
            set => Set(ref _displayingProducts, value, nameof(DisplayingProducts));
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value, nameof(SelectedProduct));
        }

        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                if (Set(ref _selectedSort, value, nameof(SelectedSort)))
                    DisplayProducts();
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (Set(ref _selectedFilter, value, nameof(SelectedFilter)))
                    DisplayProducts();
            }
        }

        public string SearchValue
        {
            get => _searchValue;
            set
            {
                if (Set(ref _searchValue, value, nameof(Search)))
                    DisplayProducts();
            }
        }

        public List<string> SortList { get; } = new List<string>
        {
            "Без сортировки",
            "Наименование(возр)",
            "Наименование(уб)",
            "Цена(возр)", 
            "Цена(уб)",
            "Категория (возр)",
            "Категория (уб)"
        };
        public List<string> FilterList { get; } = new List<string>
        {
            "Все категории"
        };

        #endregion

        #region Paging

        public record Page(int pageNum);
        private const int PageSize = 10;
        private List<Page> _pages;
        private Page _selectedPage;

        public List<Page> Pages
        {
            get => _pages;
            set => Set(ref _pages, value, nameof(Pages));
        }
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (Set(ref _selectedPage, value, nameof(SelectedPage)))
                    DisplayProducts();
            }
        }

        private List<Page> GetPages(int itemCount)
        {
            double pageCount = Math.Ceiling((double)itemCount / PageSize);
            var list = new List<Page>((int)pageCount);
            list.Add(new Page(1));
            for (int i = 1; i < pageCount; i++)
                list.Add(new Page(i + 1));
            return list;
        }

        #endregion

        public MainWindowViewModel()
        {
            using (ApplicationDbContext context = new())
            {
                //FilterList.AddRange(context.ProductTypes.Select(p => p.Name));

                _products = context.Products.AsNoTracking()
                    .Include(pt => pt.ProductType)
                    .OrderBy(p => p.Id)
                    .ToList();
            }

            _displayingProducts = new List<Product>(_products);
            _selectedFilter = FilterList[0];
            _selectedSort = SortList[0];
            _pages = GetPages(_displayingProducts.Count);
            _selectedPage = _pages[0];
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            var list = Sort(Search(Filter(_products))).ToList();

            Pages = GetPages(list.Count);

            var pageNum = SelectedPage == null
                ? 1
                : SelectedPage.pageNum;

            DisplayingProducts = Paging(list, pageNum, PageSize).ToList();

            SelectedPage ??= Pages[0];
        }

        private IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            var sort = SelectedSort;
            
            if (sort == SortList[1])
                return products.OrderBy(p => p.ProductName);
            else if (sort == SortList[2])
                return products.OrderByDescending(p => p.ProductName);
            else if (sort == SortList[3])
                return products.OrderBy(p => p.Price);
            else if (sort == SortList[4])
                return products.OrderByDescending(p => p.Price);
            else if (sort == SortList[5])
                return products.OrderBy(p => p.ProductType.Name);
            else if (sort == SortList[6])
                return products.OrderByDescending(a => a.ProductType.Name);
            else
                return products;
        }


        private IEnumerable<Product> Filter(IEnumerable<Product> products)
        {
            var filter = SelectedFilter;

            if (filter == FilterList[0])
                return products;
            else
                return products.Where(p => p.ProductType.Name == filter);
        }

        private IEnumerable<Product> Search(IEnumerable<Product> products)
        {
            var search = SearchValue == null
                ? string.Empty
                : SearchValue.ToLower();

            return (search == string.Empty) || (search == null)
                ? products
                : products.Where(p => p.ProductName.ToLower().Contains(search.ToLower()) || p.ProductType.Name.ToLower().Contains(search.ToLower()));
        }

        private IEnumerable<Product> Paging(IEnumerable<Product> products, int pageNum, int pageSize)
        {
            if (pageNum > 0)
                products = products.Skip((pageNum - 1) * pageSize);

            return products.Take(pageSize);
        }
    }
}
