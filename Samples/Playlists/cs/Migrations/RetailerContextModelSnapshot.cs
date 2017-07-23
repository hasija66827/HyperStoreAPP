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

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<float>("WalletBalance");

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

                    b.Property<float>("AddingMoneyToWallet");

                    b.Property<Guid?>("CustomerId")
                        .IsRequired();

                    b.Property<long>("CustomerOrderNo");

                    b.Property<float>("DiscountedAmount");

                    b.Property<bool>("IsPaidNow");

                    b.Property<bool>("IsUseWallet");

                    b.Property<DateTime>("OrderDate");

                    b.Property<float>("PartiallyPaid");

                    b.Property<float>("PayingLater");

                    b.Property<float>("PayingNow");

                    b.Property<float>("TotalBillAmount");

                    b.Property<float>("UsingWalletAmount");

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

                    b.Property<float>("DiscountPerSnapShot");

                    b.Property<float>("DisplayCostSnapShot");

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

                    b.Property<float>("DiscountPer");

                    b.Property<float>("DisplayPrice");

                    b.Property<bool>("IsInventoryItem");

                    b.Property<string>("Name");

                    b.Property<int>("RefillTime");

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

            modelBuilder.Entity("DatabaseModel.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("CreditAmount");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<long>("TransactionNo");

                    b.Property<float>("WalletSnapshot");

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

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<float>("WalletBalance");

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

                    b.Property<float>("BillAmount");

                    b.Property<DateTime>("DueDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<float>("PaidAmount");

                    b.Property<Guid?>("WholeSellerId")
                        .IsRequired();

                    b.Property<long>("WholeSellerOrderNo");

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

                    b.Property<float>("PurchasePrice");

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

                    b.Property<float>("PaidAmount");

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
