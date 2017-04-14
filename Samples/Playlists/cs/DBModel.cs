using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            optionsBuilder.UseSqlite("Data Source=Retailers.db");
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
        public string BarCode { get; set; }
        public string UserDefinedCode { get; set; }
        public bool IsInventoryItem { get; set; }
        public Int32 Threshold { get; set; }
        public Int32 RefillTime { get; set; }
        public float DisplayPrice { get; set; }
        public float DiscountPer { get; set; }

        public List<WholeSellerOrderProduct> WholeSellerOrderPorducts { get; set; }
        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }

        public Product()
        {
            this.ProductId = Guid.NewGuid();
            this.Threshold = 0;
            this.RefillTime = 0;
            this.DisplayPrice = 0;
            this.DiscountPer = 0;
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
            this.IsVerifiedCustomer = false;
            this.MobileNo = mobileNo;
            this.Name = name;
            this.Address = "";
        }
        public Customer() { }
    }

    public class CustomerOrder
    {
        public Guid CustomerOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float BillAmount { get; set; }
        public float PaidAmount { get; set; }
        public float WalletSnapShot { get; set; }

        [Required]
        public Nullable<Guid> CustomerId;
        public Customer Customer;

        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }

        public CustomerOrder(Guid customerId)
        {
            this.CustomerOrderId = Guid.NewGuid();
            this.OrderDate = DateTime.Now;
            this.CustomerId = customerId;
        }
        public CustomerOrder() { }
    }

    public class CustomerOrderProduct
    {
        public Guid CustomerOrderProductId { get; set; }
        public int QuantityPurchased { get; set; }
        public float DisplayCostSnapShot { get; set; }
        public float DiscountPerSnapShot { get; set; }


        [Required]
        public Nullable<Guid> OrderId;
        public CustomerOrder Order;

        [Required]
        public Nullable<Guid> ProductId;
        public Product Product;
    }
}

