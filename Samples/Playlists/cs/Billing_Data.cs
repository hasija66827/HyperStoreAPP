using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal SellingPrice { get { return this.CostPrice - this.Discount; }}
        public Int32 Quantity { get; set; }
        public decimal NetValue { get { return this.SellingPrice * this.Quantity; } }
        //TODO: #feature: consider weight parameter for non inventory items
        public Product()
        {
            this.Id = "";
            this.Name = "";
            this.CostPrice = 0;
            this.Discount = 0;
            this.DiscountPercentage = 0;
            this.Quantity = 0;
        }
    }

    public class Recording
    {
        public string ArtistName { get; set; }
        public string CompositionName { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public Recording()
        {
            this.ArtistName = "Wolfgang Amadeus Mozart";
            this.CompositionName = "Andante in C for Piano";
            this.ReleaseDateTime = new DateTime(1761, 1, 1);
        }
        public string OneLineSummary
        {
            get
            {
                return $"{this.CompositionName} by {this.ArtistName}, released: "
                    + this.ReleaseDateTime.ToString("d");
            }
        }
    }
    public class RecordingViewModel
    {
        private Recording defaultRecording = new Recording();
        public Recording DefaultRecording { get { return this.defaultRecording; } }
        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();
        public ObservableCollection<Recording> Recordings { get { return this.recordings; } }
        public RecordingViewModel()
        {
            this.recordings.Add(new Recording()
            {
                ArtistName = "Johann Sebastian Bach",
                CompositionName = "Mass in B minor",
                ReleaseDateTime = new DateTime(1748, 7, 8)
            });
            this.recordings.Add(new Recording()
            {
                ArtistName = "Ludwig van Beethoven",
                CompositionName = "Third Symphony",
                ReleaseDateTime = new DateTime(1805, 2, 11)
            });
            this.recordings.Add(new Recording()
            {
                ArtistName = "George Frideric Handel",
                CompositionName = "Serse",
                ReleaseDateTime = new DateTime(1737, 12, 3)
            });
        }
    }
    public class ProductViewModel
    {
        private Product _product = new Product();
        public Product Product { get { return this._product; } }
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get { return this._products; } }
        public ProductViewModel()
        {
            this._products.Add(new Product()
            {
                Id = "4444",
                Name = "Maggi",
                Quantity = 1,
                CostPrice = 12,
                Discount = 2,
                DiscountPercentage = (2 / 12 * 100),
            });
            this._products.Add(new Product()
            {
                Id = "55555",
                Name = "Patanjali cow ghee 1kg",
                CostPrice=200,
                Discount = 10,
                DiscountPercentage = (10 / 200 * 100),
            });
            this._products.Add(new Product()
            {
                Id = "666666",
                Name = "Patanjali shampoo asdfaswfasgsafgsfagsfag 1kg",
                CostPrice = 200,
                Discount = 10,
                DiscountPercentage = (10 / 200 * 100),
            });
        }
    }
}
