using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
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
            optionsBuilder.UseSqlite("Data Source=Retailers27.db");
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
        public Guid? WholeSellerId { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public decimal WalletBalance { get; set; }
        public List<WholeSellerOrder> WholeSellerOrders { get; set; }
        //This is used by Retailer to mark the product to be prurchased from Wholeseller.
        public List<Product> Products { get; set; }
        public List<Transaction> Transactions { get; set; }

        public WholeSeller() { }

        public static explicit operator WholeSeller(WholeSellerViewModel v)
        {
            WholeSeller w = new WholeSeller();
            w.WholeSellerId = v.SupplierId;
            w.Address = v.Address;
            w.GSTIN = v.GSTIN;
            w.MobileNo = v.MobileNo;
            w.Name = v.Name;
            w.WalletBalance = v.WalletBalance;
            return w;
        }
    }

    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public string TransactionNo { get; set; }
        public decimal CreditAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal WalletSnapshot { get; set; }

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
        public decimal PaidAmount { get; set; }//#remove it.
        public bool IsPaymentComplete { get; set; }// # remove it.
        public WholeSellerOrderTransaction() { }
        public WholeSellerOrderTransaction(Guid transactionId, Guid wholeSellerOrderId, decimal paidAmount, bool isPaymentComplete)
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
        public decimal BillAmount { get; set; }
        public decimal PaidAmount { get; set; }

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
            this.WholeSellerId = wholeSellerPurchaseNavigationParameter.WholeSellerViewModel.SupplierId;
        }

        public WholeSellerOrder() { }
    }
    public class WholeSellerOrderProduct
    {
        public Guid WholeSellerOrderProductId { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal PurchasePrice { get; set; }

        [Required]
        public Nullable<Guid> WholeSellerOrderId;
        public WholeSellerOrder WholeSellerOrder;

        [Required]
        public Nullable<Guid> ProductId;
        public virtual Product Product { get; set; }
        public WholeSellerOrderProduct() { }
        public WholeSellerOrderProduct(Guid productId, Guid wholeSellerOrderId, int quantityPurchased, decimal purchasePrice)
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
            this.TagId = tag.TagId ?? Guid.NewGuid();
            this.TagName = tag.TagName;
        }
    }

    public class ProductTag
    {
        public Guid ProductTagId { get; set; }

        public ProductTag(Guid productId, Guid? tagId)
        {
            this.ProductTagId = Guid.NewGuid();
            this.ProductId = productId;
            this.TagId = tagId;
        }

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
        public decimal DisplayPrice { get; set; }
        public decimal DiscountPer { get; set; }
        public Int32 TotalQuantity { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal CGSTPer { get; set; }

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
        public string Address { get; set; }
        public string GSTIN { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public decimal WalletBalance { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }

        public Customer()
        { }
        public static explicit operator Customer(CustomerViewModel v)
        {
            Customer c = new Customer();
            c.CustomerId = v.CustomerId;
            c.Address = v.Address;
            c.GSTIN = v.GSTIN;
            c.MobileNo = v.MobileNo;
            c.Name = v.Name;
            c.WalletBalance = v.WalletBalance;
            return c;
        }
    }

    public class CustomerOrder
    {
        public Guid CustomerOrderId { get; set; }
        public string CustomerOrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal BillAmount { get; set; }
        public decimal DiscountedAmount { get; set; }

        // PayingNow = DiscountedBillAmount + AddingMoneyToWallet - UsingWalletAmount
        public bool IsPaidNow { get; set; }
        public decimal PayingNow { get; set; }
        public decimal AddingMoneyToWallet { get; set; }

        public bool IsUseWallet { get; set; }
        public decimal UsingWalletAmount { get; set; }

        // DiscountedBillAmt = PartiallyPaid + PayingLater
        public decimal PartiallyPaid { get; set; }
        public decimal PayingLater { get; set; }
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
            this.UsingWalletAmount = (decimal)pageNavigationParameter.CustomerViewModel.WalletBalance;

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
        public decimal DiscountPerSnapShot { get; set; }
        public decimal DisplayCostSnapShot { get; set; }
        public int QuantityPurchased { get; set; }

        public CustomerOrderProduct(Guid customerOrderId, Guid productId,
            decimal discountPerSnapshot, decimal displayCostSnapshot, int quantityPurchased)
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

    public class CustomerPurchaseTrend
    {
        public int TotalQuantityPurchased { get; set; }
        public Product Product { get; set; }
        public decimal NetValue { get; set; }
        public CustomerPurchaseTrend() { }
    }
    public class ProductTrend
    {
        public DayOfWeek Day { get; set; }
        public float Quantity { get; set; }
        public ProductTrend(DayOfWeek day, float quantity)
        {
            this.Day = day;
            this.Quantity = quantity;
        }
    }

}

