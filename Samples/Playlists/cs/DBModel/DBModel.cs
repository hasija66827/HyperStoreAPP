using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SDKTemp.ViewModel;
using SDKTemplate;

namespace DatabaseModel
{
    public class RetailerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderProduct> CustomerOrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WholeSeller> WholeSellers { get; set; }
        public DbSet<WholeSellerOrder> WholeSellersOrders { get; set; }
        public DbSet<WholeSellerOrderProduct> WholeSellersOrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Retailers7.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.MobileNo)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<WholeSeller>()
                .HasIndex(w => w.MobileNo);
            modelBuilder.Entity<WholeSeller>()
                .HasIndex(w => w.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.BarCode)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.UserDefinedCode)
                .IsUnique();
        }
    }

    public class WholeSeller
    {
        public Guid WholeSellerId { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public bool IsVerifiedWholeSeller { get; set; }
        public string Address { get; set; }
        public float WalletBalance { get; set; }
        public List<WholeSellerOrder> WholeSellerOrders { get; set; }

        public WholeSeller(string name, string mobileNo)
        {
            this.WholeSellerId = Guid.NewGuid();
            this.Address = "";
            this.IsVerifiedWholeSeller = false;
            this.MobileNo = mobileNo;
            this.Name = name;
        }
        public WholeSeller() { }

        public static explicit operator WholeSeller(WholeSellerViewModel v)
        {
            WholeSeller w = new WholeSeller();
            w.WholeSellerId = v.WholeSellerId;
            w.MobileNo = v.MobileNo;
            w.Name = v.Name;
            w.IsVerifiedWholeSeller = v.IsVerifiedWholeSeller;
            w.Address = v.Address;
            w.WalletBalance = v.WalletBalance;
            return w;
        }
    }
    public class WholeSellerOrder
    {
        public Guid WholeSellerOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public float BillAmount { get; set; }
        public float PaidAmount { get; set; }

        [Required]
        public Nullable<Guid> WholeSellerId;
        public WholeSeller WholeSeller;

        public List<WholeSellerOrderProduct> WholeSellerOrderProducts { get; set; }

        public WholeSellerOrder(Guid wholeSellerId)
        {
            this.WholeSellerOrderId = Guid.NewGuid();
            this.OrderDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.BillAmount = 0;
            this.PaidAmount = 0;
            this.WholeSellerId = wholeSellerId;
        }
        public WholeSellerOrder(WholeSellerPurchaseNavigationParameter wholeSellerPurchaseNavigationParameter)
        {
            this.WholeSellerOrderId = Guid.NewGuid();
            this.OrderDate = DateTime.Now;
            this.DueDate = wholeSellerPurchaseNavigationParameter.WholeSellerPurchaseCheckoutViewModel.DueDate;
            this.BillAmount = wholeSellerPurchaseNavigationParameter.WholeSellerBillingViewModel.BillAmount;
            this.PaidAmount = wholeSellerPurchaseNavigationParameter.WholeSellerPurchaseCheckoutViewModel.PaidAmount;
            this.WholeSellerId = wholeSellerPurchaseNavigationParameter.WholeSellerViewModel.WholeSellerId;
        }
        public WholeSellerOrder() { }
    }
    public class WholeSellerOrderProduct
    {
        public Guid WholeSellerOrderProductId { get; set; }
        public int QuantityPurchased { get; set; }
        public float PurchasePrice { get; set; }


        [Required]
        public Nullable<Guid> WholeSellerOrderId;
        public WholeSellerOrder WholeSellerOrder;

        [Required]
        public Nullable<Guid> ProductId;
        public Product Product;
        public WholeSellerOrderProduct() { }
        public WholeSellerOrderProduct(Guid productId, Guid wholeSellerOrderId, int quantityPurchased, float purchasePrice)
        {
            this.WholeSellerOrderProductId = Guid.NewGuid();
            this.ProductId = productId;
            this.WholeSellerOrderId = wholeSellerOrderId;
            this.QuantityPurchased = quantityPurchased;
            this.PurchasePrice = purchasePrice;
        }
    }
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string UserDefinedCode { get; set; }
        public bool IsInventoryItem { get; set; }
        public Int32 Threshold { get; set; }
        public Int32 RefillTime { get; set; }
        public float DisplayPrice { get; set; }
        public float DiscountPer { get; set; }
        public Int32 TotalQuantity { get; set; }
        public List<WholeSellerOrderProduct> WholeSellerOrderPorducts { get; set; }
        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }


        public Product() {   }
        public Product(Guid productID, string name, string barCode, bool isInventoryItem, Int32 threshold, Int32 refillTime,
            float displayPrice, float discountPer, Int32 totalQuantity)
        {
            ProductId = productID;
            Name = name;
            BarCode = barCode;
            IsInventoryItem = isInventoryItem;
            Threshold = threshold;
            RefillTime = refillTime;
            DisplayPrice = displayPrice;
            DiscountPer = discountPer;
            TotalQuantity = totalQuantity;
        }

        public static explicit operator Product(ProductViewModel v)
        {
            Product p = new Product();
            p.ProductId = v.ProductId;
            p.Name = v.Name;
            p.BarCode = v.BarCode;
            p.Threshold = v.Threshold;
            p.DisplayPrice = v.DisplayPrice;
            p.DiscountPer = v.DiscountPer;
            p.TotalQuantity = v.TotalQuantity;

            //TODO: add below propery in product view model 
            p.IsInventoryItem = false;
            p.RefillTime = 0;
            return p;
        }
    }

    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public bool IsVerifiedCustomer { get; set; }
        public string Address { get; set; }
        public float WalletBalance { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }

        public Customer(string name, string mobileNo)
        {
            this.CustomerId = Guid.NewGuid();
            this.Address = "";
            this.IsVerifiedCustomer = false;
            this.MobileNo = mobileNo;
            this.Name = name;
            this.WalletBalance = 0;
        }
        public Customer()
        { }
        public static explicit operator Customer(CustomerViewModel v)
        {
            Customer c = new Customer();
            c.CustomerId = v.CustomerId;
            c.MobileNo = v.MobileNo;
            c.Name = v.Name;
            c.IsVerifiedCustomer = v.IsVerifiedCustomer;
            c.Address = v.Address;
            c.WalletBalance = v.WalletBalance;
            return c;
        }
    }

    public class CustomerOrder
    {
        public Guid CustomerOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalBillAmount { get; set; }

        public float DiscountedAmount { get; set; }

        // PayingNow = DiscountedBillAmount + AddingMoneyToWallet - UsingWalletAmount
        public bool IsPaidNow { get; set; }
        public float PayingNow { get; set; }
        public float AddingMoneyToWallet { get; set; }

        public bool IsUseWallet { get; set; }
        public float UsingWalletAmount { get; set; }

        // DiscountedBillAmt = PartiallyPaid + PayingLater
        public float PartiallyPaid { get; set; }
        public float PayingLater { get; set; }
        public CustomerOrder() { }
        public CustomerOrder(PageNavigationParameter pageNavigationParameter)
        {
            this.CustomerOrderId = Guid.NewGuid();
            this.OrderDate = DateTime.Now;
            this.TotalBillAmount = pageNavigationParameter.BillingViewModel.TotalBillAmount;
            this.DiscountedAmount = pageNavigationParameter.BillingViewModel.DiscountedBillAmount;

            this.IsPaidNow = pageNavigationParameter.IsPaidNow;
            this.PayingNow = pageNavigationParameter.OverPaid;
            this.AddingMoneyToWallet = pageNavigationParameter.WalletAmountToBeAddedNow;

            this.IsUseWallet = pageNavigationParameter.UseWallet.Value;
            this.UsingWalletAmount = pageNavigationParameter.CustomerViewModel.WalletBalance;

            this.PartiallyPaid = pageNavigationParameter.PartiallyPaid;
            this.PayingLater = pageNavigationParameter.WalletAmountToBePaidLater;

            this.CustomerId = pageNavigationParameter.CustomerViewModel.CustomerId;
        }
        [Required]
        public Nullable<Guid> CustomerId;
        public Customer Customer;
        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }
        
    }

    public class CustomerOrderProduct
    {
        public Guid CustomerOrderProductId { get; set; }
        public float DiscountPerSnapShot { get; set; }
        public float DisplayCostSnapShot { get; set; }
        public int QuantityPurchased { get; set; }

        public CustomerOrderProduct(Guid customerOrderId, Guid productId,
            float discountPerSnapshot, float displayCostSnapshot, int quantityPurchased)
        {
            this.CustomerOrderProductId = Guid.NewGuid();
            this.CustomerOrderId = customerOrderId;
            this.ProductId = productId;
            this.DiscountPerSnapShot = discountPerSnapshot;
            this.DisplayCostSnapShot = displayCostSnapshot;
            this.QuantityPurchased = quantityPurchased;
        }

        public CustomerOrderProduct() { }
        [Required]
        public Nullable<Guid> CustomerOrderId;
        public CustomerOrder CustomerOrder;

        [Required]
        public Nullable<Guid> ProductId;
        public Product Product;
    }
}

