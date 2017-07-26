using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SDKTemp.ViewModel;
using SDKTemplate;
using SDKTemplate.View_Models;
namespace DatabaseModel
{
    public class RetailerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderProduct> CustomerOrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WholeSeller> WholeSellers { get; set; }
        public DbSet<WholeSellerOrder> WholeSellersOrders { get; set; }
        public DbSet<WholeSellerOrderProduct> WholeSellersOrderProducts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<WholeSellerOrderTransaction> WholeSellerOrderTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Retailers25.db");
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
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string GSTIN { get; set; }
        public float WalletBalance { get; set; }
        public List<WholeSellerOrder> WholeSellerOrders { get; set; }
        //This is used by Retailer to mark the product to be prurchased from Wholeseller.
        public List<Product> Products { get; set; }
        public List<Transaction> Transactions { get; set; }

        public WholeSeller(string address, string mobileNo, string name, float walletBalance)
        {
            this.WholeSellerId = Guid.NewGuid();
            this.Address = address;
            this.MobileNo = mobileNo;
            this.Name = name;
            this.WalletBalance = walletBalance;
        }
        public WholeSeller() { }

        public static explicit operator WholeSeller(WholeSellerViewModel v)
        {
            WholeSeller w = new WholeSeller();
            w.WholeSellerId = v.WholeSellerId;
            w.MobileNo = v.MobileNo;
            w.Name = v.Name;
            w.Address = v.Address;
            w.WalletBalance = v.WalletBalance;
            return w;
        }
    }

    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public string TransactionNo { get; set; }
        public float CreditAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public float WalletSnapshot { get; set; }

        [Required]
        public Nullable<Guid> WholeSellerId;
        public WholeSeller WholeSeller;

        public List<WholeSellerOrderTransaction> WholeSellerOrderTransactions { get; set; }

        public Transaction() { }
        public Transaction(TransactionViewModel transactionViewModel)
        {
            this.TransactionId = transactionViewModel.TransactionId;
            this.TransactionNo = Utility.GenerateWholeSellerTransactionNo();
            this.CreditAmount = transactionViewModel.CreditAmount;
            this.TransactionDate = transactionViewModel.TransactionDate;
            this.WholeSellerId = transactionViewModel.WholeSellerId;
            this.WalletSnapshot = transactionViewModel.WalletSnapshot;
        }
    }

    public class WholeSellerOrderTransaction
    {
        public Guid WholeSellerOrderTransactionId { get; set; }
        public float PaidAmount { get; set; }//#remove it.
        public bool IsPaymentComplete { get; set; }// # remove it.
        public WholeSellerOrderTransaction() { }
        public WholeSellerOrderTransaction(Guid transactionId, Guid wholeSellerOrderId, float paidAmount, bool isPaymentComplete)
        {
            this.WholeSellerOrderTransactionId = Guid.NewGuid();
            this.TransactionId = transactionId;
            this.WholeSellerOrderId = wholeSellerOrderId;
            this.PaidAmount = paidAmount;
            this.IsPaymentComplete = isPaymentComplete;
        }
        [Required]
        public Nullable<Guid> TransactionId;
        public Transaction Transaction;

        [Required]
        public Nullable<Guid> WholeSellerOrderId;
        public WholeSellerOrder WholeSellerOrder;
    }

    public class WholeSellerOrder
    {
        public Guid WholeSellerOrderId { get; set; }
        public string WholeSellerOrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public float BillAmount { get; set; }
        public float PaidAmount { get; set; }

        [Required]
        public Nullable<Guid> WholeSellerId;
        public WholeSeller WholeSeller;

        public List<WholeSellerOrderProduct> WholeSellerOrderProducts { get; set; }
        public List<WholeSellerOrderTransaction> WholeSellerOrderTransactions { get; set; }

        public WholeSellerOrder(Guid wholeSellerId)
        {
            this.WholeSellerOrderId = Guid.NewGuid();
            this.WholeSellerOrderNo = Utility.GenerateWholeSellerOrderNo();
            this.OrderDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.BillAmount = 0;
            this.PaidAmount = 0;
            this.WholeSellerId = wholeSellerId;
        }

        public WholeSellerOrder(WholeSellerCheckoutNavigationParameter wholeSellerPurchaseNavigationParameter)
        {
            this.WholeSellerOrderId = Guid.NewGuid();
            this.WholeSellerOrderNo = Utility.GenerateWholeSellerOrderNo();
            this.OrderDate = DateTime.Now;
            this.DueDate = wholeSellerPurchaseNavigationParameter.WholeSellerCheckoutViewModel.DueDate;
            this.BillAmount = wholeSellerPurchaseNavigationParameter.WholeSellerBillingSummaryViewModel.BillAmount;
            this.PaidAmount = wholeSellerPurchaseNavigationParameter.WholeSellerCheckoutViewModel.PaidAmount;
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
        public virtual Product Product { get; set; }
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

    public class Tag
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public List<ProductTag> ProductTags { get; set; }

        public Tag()
        { }
        public Tag(TagViewModel tag)
        {
            this.TagId = new Guid();
            this.TagName = tag.TagName;
        }
    }

    public class ProductTag
    {
        public Guid ProductTagId { get; set; }

        [Required]
        public Nullable<Guid> ProductId { get; set; }
        public Product Product;

        [Required]
        public Nullable<Guid> TagId { get; set; }
        public Tag Tag;
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
        public float SGSTPer { get; set; }
        public float CGSTPer { get; set; }

        public List<WholeSellerOrderProduct> WholeSellerOrderProducts { get; set; }
        public List<CustomerOrderProduct> CustomerOrderProducts { get; set; }
        public List<ProductTag> ProductTags { get; set; }

        //This is used by Retailer to mark the product to be prurchased from Wholeseller.
        [Required]
        public Guid? WholeSellerId;
        public WholeSeller WholeSeller;

        public Product() { }

        public Product(ProductViewModelBase productViewModel)
        {
            ProductId = productViewModel.ProductId;
            Name = productViewModel.Name;
            BarCode = productViewModel.BarCode;
            CGSTPer = productViewModel.CGSTPer;
            DisplayPrice = productViewModel.DisplayPrice;
            DiscountPer = productViewModel.DiscountPer;
            //IsInventoryItem = productViewModel.IsInventoryItem;
            RefillTime = productViewModel.RefillTime;
            SGSTPer = productViewModel.SGSTPer;
            Threshold = productViewModel.Threshold;
            TotalQuantity = productViewModel.TotalQuantity;
            WholeSellerId = null;
        }

        public static explicit operator Product(CustomerProductViewModel customerProductViewModel)
        {
            Product p = new Product();
            p.ProductId = customerProductViewModel.ProductId;
            p.Name = customerProductViewModel.Name;
            p.BarCode = customerProductViewModel.BarCode;
            p.CGSTPer = customerProductViewModel.CGSTPer;
            p.Threshold = customerProductViewModel.Threshold;
            p.DisplayPrice = customerProductViewModel.DisplayPrice;
            p.DiscountPer = customerProductViewModel.DiscountPer;
            p.SGSTPer = customerProductViewModel.SGSTPer;
            p.TotalQuantity = customerProductViewModel.TotalQuantity;

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
        public string Address { get; set; }
        public string GSTIN;
        public float WalletBalance { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }

        public Customer(string name, string mobileNo)
        {
            this.CustomerId = Guid.NewGuid();
            this.Address = "";
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
            c.Address = v.Address;
            c.WalletBalance = v.WalletBalance;
            return c;
        }
    }

    public class CustomerOrder
    {
        public Guid CustomerOrderId { get; set; }
        public string CustomerOrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public float BillAmount { get; set; }
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
            this.CustomerOrderNo = Utility.GenerateCustomerOrderNo();
            this.OrderDate = DateTime.Now;
            this.BillAmount = pageNavigationParameter.BillingSummaryViewModel.TotalBillAmount;
            this.DiscountedAmount = pageNavigationParameter.BillingSummaryViewModel.DiscountedBillAmount;

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

