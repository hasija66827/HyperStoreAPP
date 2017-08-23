using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DatabaseModel;

namespace SDKTemplate.Migrations
{
    [DbContext(typeof(RetailerContext))]
    partial class RetailerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("DatabaseModel.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("GSTIN");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<decimal>("WalletBalance");

                    b.HasKey("CustomerId");

                    b.HasIndex("MobileNo")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DatabaseModel.CustomerOrder", b =>
                {
                    b.Property<Guid>("CustomerOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AddingMoneyToWallet");

                    b.Property<decimal>("BillAmount");

                    b.Property<Guid?>("CustomerId")
                        .IsRequired();

                    b.Property<string>("CustomerOrderNo");

                    b.Property<decimal>("DiscountedAmount");

                    b.Property<bool>("IsPaidNow");

                    b.Property<bool>("IsUseWallet");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("PartiallyPaid");

                    b.Property<decimal>("PayingLater");

                    b.Property<decimal>("PayingNow");

                    b.Property<decimal>("UsingWalletAmount");

                    b.HasKey("CustomerOrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerOrders");
                });

            modelBuilder.Entity("DatabaseModel.CustomerOrderProduct", b =>
                {
                    b.Property<Guid>("CustomerOrderProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CustomerOrderId")
                        .IsRequired();

                    b.Property<decimal>("DiscountPerSnapShot");

                    b.Property<decimal>("DisplayCostSnapShot");

                    b.Property<Guid?>("ProductId")
                        .IsRequired();

                    b.Property<int>("QuantityPurchased");

                    b.HasKey("CustomerOrderProductId");

                    b.HasIndex("CustomerOrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CustomerOrderProducts");
                });

            modelBuilder.Entity("DatabaseModel.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarCode");

                    b.Property<decimal>("CGSTPer");

                    b.Property<decimal>("DiscountPer");

                    b.Property<decimal>("DisplayPrice");

                    b.Property<bool>("IsInventoryItem");

                    b.Property<string>("Name");

                    b.Property<int>("RefillTime");

                    b.Property<decimal>("SGSTPer");

                    b.Property<int>("Threshold");

                    b.Property<int>("TotalQuantity");

                    b.Property<string>("UserDefinedCode");

                    b.Property<Guid?>("WholeSellerId")
                        .IsRequired();

                    b.HasKey("ProductId");

                    b.HasIndex("BarCode")
                        .IsUnique();

                    b.HasIndex("UserDefinedCode")
                        .IsUnique();

                    b.HasIndex("WholeSellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DatabaseModel.ProductTag", b =>
                {
                    b.Property<Guid>("ProductTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProductId")
                        .IsRequired();

                    b.Property<Guid?>("TagId")
                        .IsRequired();

                    b.HasKey("ProductTagId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("DatabaseModel.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DatabaseModel.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CreditAmount");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<string>("TransactionNo");

                    b.Property<decimal>("WalletSnapshot");

                    b.Property<Guid?>("WholeSellerId")
                        .IsRequired();

                    b.HasKey("TransactionId");

                    b.HasIndex("WholeSellerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DatabaseModel.WholeSeller", b =>
                {
                    b.Property<Guid>("WholeSellerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("GSTIN");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<decimal>("WalletBalance");

                    b.HasKey("WholeSellerId");

                    b.HasIndex("MobileNo");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("WholeSellers");
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrder", b =>
                {
                    b.Property<Guid>("WholeSellerOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BillAmount");

                    b.Property<DateTime>("DueDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("PaidAmount");

                    b.Property<Guid?>("WholeSellerId")
                        .IsRequired();

                    b.Property<string>("WholeSellerOrderNo");

                    b.HasKey("WholeSellerOrderId");

                    b.HasIndex("WholeSellerId");

                    b.ToTable("WholeSellersOrders");
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrderProduct", b =>
                {
                    b.Property<Guid>("WholeSellerOrderProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProductId")
                        .IsRequired();

                    b.Property<decimal>("PurchasePrice");

                    b.Property<int>("QuantityPurchased");

                    b.Property<Guid?>("WholeSellerOrderId")
                        .IsRequired();

                    b.HasKey("WholeSellerOrderProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WholeSellerOrderId");

                    b.ToTable("WholeSellersOrderProducts");
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrderTransaction", b =>
                {
                    b.Property<Guid>("WholeSellerOrderTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPaymentComplete");

                    b.Property<decimal>("PaidAmount");

                    b.Property<Guid?>("TransactionId")
                        .IsRequired();

                    b.Property<Guid?>("WholeSellerOrderId")
                        .IsRequired();

                    b.HasKey("WholeSellerOrderTransactionId");

                    b.HasIndex("TransactionId");

                    b.HasIndex("WholeSellerOrderId");

                    b.ToTable("WholeSellerOrderTransactions");
                });

            modelBuilder.Entity("DatabaseModel.CustomerOrder", b =>
                {
                    b.HasOne("DatabaseModel.Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.CustomerOrderProduct", b =>
                {
                    b.HasOne("DatabaseModel.CustomerOrder")
                        .WithMany("CustomerOrderProducts")
                        .HasForeignKey("CustomerOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseModel.Product")
                        .WithMany("CustomerOrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.Product", b =>
                {
                    b.HasOne("DatabaseModel.WholeSeller")
                        .WithMany("Products")
                        .HasForeignKey("WholeSellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.ProductTag", b =>
                {
                    b.HasOne("DatabaseModel.Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseModel.Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.Transaction", b =>
                {
                    b.HasOne("DatabaseModel.WholeSeller")
                        .WithMany("Transactions")
                        .HasForeignKey("WholeSellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrder", b =>
                {
                    b.HasOne("DatabaseModel.WholeSeller")
                        .WithMany("WholeSellerOrders")
                        .HasForeignKey("WholeSellerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrderProduct", b =>
                {
                    b.HasOne("DatabaseModel.Product", "Product")
                        .WithMany("WholeSellerOrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseModel.WholeSellerOrder")
                        .WithMany("WholeSellerOrderProducts")
                        .HasForeignKey("WholeSellerOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseModel.WholeSellerOrderTransaction", b =>
                {
                    b.HasOne("DatabaseModel.Transaction")
                        .WithMany("WholeSellerOrderTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseModel.WholeSellerOrder")
                        .WithMany("WholeSellerOrderTransactions")
                        .HasForeignKey("WholeSellerOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
