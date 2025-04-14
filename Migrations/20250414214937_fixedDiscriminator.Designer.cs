﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimelessTapes.Data;

#nullable disable

namespace TimelessTapes.Migrations
{
    [DbContext(typeof(DBHandler))]
    [Migration("20250414214937_fixedDiscriminator")]
    partial class fixedDiscriminator
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TimelessTapes.Models.AdminLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("action");

                    b.Property<DateTime>("ActionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("actionDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.HasKey("LogId");

                    b.HasIndex("AdminId");

                    b.ToTable("AdminLogs", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.CustomerReview", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("reviewText");

                    b.Property<string>("TitleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TitleId");

                    b.ToTable("CustomerReviews", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.Latefee", b =>
                {
                    b.Property<int>("LatefeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LatefeeId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float")
                        .HasColumnName("amount");

                    b.Property<DateTime>("FeeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("feeDate");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit")
                        .HasColumnName("paid");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("LatefeeId");

                    b.HasIndex("TransactionId");

                    b.ToTable("LateFees", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("amount");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("paymentDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.Refund", b =>
                {
                    b.Property<int>("RefundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefundId"));

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<decimal>("RefundAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("RefundDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("refundDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RefundId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("UserId");

                    b.ToTable("Refunds", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.Title", b =>
                {
                    b.Property<string>("TitleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cast")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Copies")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PrimaryTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("TitleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TitleId");

                    b.ToTable("Titles", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("price");

                    b.Property<DateTime>("RentalDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("rentalDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("date")
                        .HasColumnName("returnDate");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("TitleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransactionId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TitleId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("TimelessTapes.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("AccessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("accessType");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("passwordHash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("accessType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("UserId");

                    b.ToTable("Users", null, t =>
                        {
                            t.Property("accessType")
                                .HasColumnName("accessType1");
                        });

                    b.HasDiscriminator<string>("accessType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TimelessTapes.Models.Customer", b =>
                {
                    b.HasBaseType("TimelessTapes.Models.User");

                    b.ToTable("Users", t =>
                        {
                            t.Property("accessType")
                                .HasColumnName("accessType1");
                        });

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("TimelessTapes.Models.AdminLog", b =>
                {
                    b.HasOne("TimelessTapes.Models.User", "User")
                        .WithMany("AdminLog")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimelessTapes.Models.CustomerReview", b =>
                {
                    b.HasOne("TimelessTapes.Models.User", "User")
                        .WithMany("CustomerReviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TimelessTapes.Models.Title", "Title")
                        .WithMany("CustomerReviews")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Title");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimelessTapes.Models.Latefee", b =>
                {
                    b.HasOne("TimelessTapes.Models.Transaction", "Transaction")
                        .WithMany("Latefees")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TimelessTapes.Models.Payment", b =>
                {
                    b.HasOne("TimelessTapes.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TimelessTapes.Models.Transaction", "Transaction")
                        .WithMany("Payments")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimelessTapes.Models.Refund", b =>
                {
                    b.HasOne("TimelessTapes.Models.Payment", "Payment")
                        .WithMany("Refund")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TimelessTapes.Models.User", "User")
                        .WithMany("Refunds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimelessTapes.Models.Transaction", b =>
                {
                    b.HasOne("TimelessTapes.Models.User", "Customer")
                        .WithMany("Transaction")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TimelessTapes.Models.Title", "Title")
                        .WithMany("Transaction")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("TimelessTapes.Models.Payment", b =>
                {
                    b.Navigation("Refund");
                });

            modelBuilder.Entity("TimelessTapes.Models.Title", b =>
                {
                    b.Navigation("CustomerReviews");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TimelessTapes.Models.Transaction", b =>
                {
                    b.Navigation("Latefees");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("TimelessTapes.Models.User", b =>
                {
                    b.Navigation("AdminLog");

                    b.Navigation("CustomerReviews");

                    b.Navigation("Payments");

                    b.Navigation("Refunds");

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
