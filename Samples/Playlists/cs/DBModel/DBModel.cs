using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MasterDetailApp.ViewModel;
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
            optionsBuilder.UseSqlite("Data Source=Retailers2.db");
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
        public WholeSellerOrder() { }
    }
    public class WholeSellerOrderProduct
    {
        public Guid WholeSellerOrderProductId { get; set; }
        public int QuantityPurchased { get; set; }
        public float PurchasePrice { get; set; }


        [Required]
        public Nullable<Guid> OrderId;
        public WholeSellerOrder WholeSellerOrder;

        [Required]
        public Nullable<Guid> ProductId;
        public Product Product;
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

        
        public Product()
        {

        }
        public Product(Guid productID, string name, string barCode, bool isInventoryItem, Int32 threshold,Int32 refillTime,
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
            p.RefillTime =0;
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
        public float BillAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public float WalletSnapShot { get; set; }
        public float PaidAmount { get; set; }//RENAME IT TO DiscountedBillAmount
        
        // PayingNow= DiscountedBillAmount + AddingMoneyToWallet - UsingWalletAmount
        public bool IsPaidNow { get; set; }
        public float PayingNow { get; set; }
        public bool AddingMoneyToWallet { get; set; }

        public bool IsUseWallet { get; set; }
        public bool UsingWalletAmount { get; set; }

        // DiscountedBillAmt=PartiallyPaid + PayingLater
        public float PartiallyPaid { get; set; }
        public float PayingLater { get; set; }

        [Required]
        public Nullable<Guid> CustomerId;
        public Customer Customer;

        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }

        public CustomerOrder(Guid customerId, float billAmount, float discountedBillAmount, float walletSnapShot)
        {
            this.CustomerOrderId = Guid.NewGuid();
            this.OrderDate = DateTime.Now;
            this.CustomerId = customerId;
            this.BillAmount = billAmount;
            this.PaidAmount = discountedBillAmount;
            this.WalletSnapShot = walletSnapShot;

        }
        public CustomerOrder() { }
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

