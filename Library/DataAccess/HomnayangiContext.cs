﻿using System;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Type = Library.Models.Type;

#nullable disable

namespace Library.DataAccess
{
    public partial class HomnayangiContext : DbContext
    {
        public HomnayangiContext()
        {
        }

        public HomnayangiContext(DbContextOptions<HomnayangiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accomplishment> Accomplishments { get; set; }
        public virtual DbSet<AccomplishmentReaction> AccomplishmentReactions { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<BadgeCondition> BadgeConditions { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogReaction> BlogReactions { get; set; }
        public virtual DbSet<BlogReference> BlogReferences { get; set; }
        public virtual DbSet<BlogSubCate> BlogSubCates { get; set; }
        public virtual DbSet<CaloReference> CaloReferences { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CookingMethod> CookingMethods { get; set; }
        public virtual DbSet<CronJobTimeConfig> CronJobTimeConfigs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBadge> CustomerBadges { get; set; }
        public virtual DbSet<CustomerVoucher> CustomerVouchers { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageDetail> PackageDetails { get; set; }
        public virtual DbSet<PriceNote> PriceNotes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SeasonReference> SeasonReferences { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI\\MONKINAM;Uid=sa;Pwd=Monki123;Database=Homnayangi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Accomplishment>(entity =>
            {
                entity.ToTable("Accomplishment");

                entity.Property(e => e.AccomplishmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("accomplishmentId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.ConfirmBy).HasColumnName("confirmBy");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.ListImageUrl).HasColumnName("listImageUrl");

                entity.Property(e => e.ListVideoUrl).HasColumnName("listVideoUrl");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Accomplishments)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Accomplishment_Customer");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Accomplishments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Accomplishment_Blog");

                entity.HasOne(d => d.ConfirmByNavigation)
                    .WithMany(p => p.Accomplishments)
                    .HasForeignKey(d => d.ConfirmBy)
                    .HasConstraintName("FK_Accomplishment_User");
            });

            modelBuilder.Entity<AccomplishmentReaction>(entity =>
            {
                entity.HasKey(e => new { e.AccomplishmentId, e.CustomerId });

                entity.ToTable("AccomplishmentReaction");

                entity.Property(e => e.AccomplishmentId).HasColumnName("accomplishmentId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Accomplishment)
                    .WithMany(p => p.AccomplishmentReactions)
                    .HasForeignKey(d => d.AccomplishmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccomplishmentReaction_Accomplishment");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AccomplishmentReactions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccomplishmentReaction_Customer");
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge");

                entity.Property(e => e.BadgeId)
                    .ValueGeneratedNever()
                    .HasColumnName("badgeId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Badges)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK_Badge_Voucher");
            });

            modelBuilder.Entity<BadgeCondition>(entity =>
            {
                entity.ToTable("BadgeCondition");

                entity.Property(e => e.BadgeConditionId)
                    .ValueGeneratedNever()
                    .HasColumnName("badgeConditionId");

                entity.Property(e => e.Accomplishments).HasColumnName("accomplishments");

                entity.Property(e => e.BadgeId).HasColumnName("badgeId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Orders).HasColumnName("orders");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.BadgeConditions)
                    .HasForeignKey(d => d.BadgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BadgeCondition_Badge");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.BlogId)
                    .ValueGeneratedNever()
                    .HasColumnName("blogId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.BlogStatus).HasColumnName("blogStatus");

                entity.Property(e => e.CookingMethodId).HasColumnName("cookingMethodId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.EventExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("eventExpiredDate");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.IsEvent).HasColumnName("isEvent");

                entity.Property(e => e.MaxSize).HasColumnName("maxSize");

                entity.Property(e => e.MinSize).HasColumnName("minSize");

                entity.Property(e => e.MinutesToCook).HasColumnName("minutesToCook");

                entity.Property(e => e.Reaction).HasColumnName("reaction");

                entity.Property(e => e.RegionId).HasColumnName("regionId");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.TotalKcal).HasColumnName("totalKcal");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.VideoUrl).HasColumnName("videoURL");

                entity.Property(e => e.View).HasColumnName("view");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Blog_User");

                entity.HasOne(d => d.CookingMethod)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.CookingMethodId)
                    .HasConstraintName("FK_Blog_CookingMethod");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Blog_Region");
            });

            modelBuilder.Entity<BlogReaction>(entity =>
            {
                entity.HasKey(e => new { e.BlogId, e.CustomerId });

                entity.ToTable("BlogReaction");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogReactions)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogReaction_Blog");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BlogReactions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogReaction_Customer");
            });

            modelBuilder.Entity<BlogReference>(entity =>
            {
                entity.ToTable("BlogReference");

                entity.Property(e => e.BlogReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("blogReferenceId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.Html).HasColumnName("html");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogReferences)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_BlogReference_Blog");
            });

            modelBuilder.Entity<BlogSubCate>(entity =>
            {
                entity.HasKey(e => new { e.BlogId, e.SubCateId });

                entity.ToTable("BlogSubCate");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.SubCateId).HasColumnName("subCateId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogSubCates)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogSubCate_Blog");

                entity.HasOne(d => d.SubCate)
                    .WithMany(p => p.BlogSubCates)
                    .HasForeignKey(d => d.SubCateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogSubCate_SubCategory");
            });

            modelBuilder.Entity<CaloReference>(entity =>
            {
                entity.ToTable("CaloReference");

                entity.Property(e => e.CaloReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("caloReferenceId");

                entity.Property(e => e.Calo).HasColumnName("calo");

                entity.Property(e => e.FromAge).HasColumnName("fromAge");

                entity.Property(e => e.IsMale).HasColumnName("isMale");

                entity.Property(e => e.ToAge).HasColumnName("toAge");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("commentId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.ByStaff).HasColumnName("byStaff");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Comment_Blog");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_Comment");
            });

            modelBuilder.Entity<CookingMethod>(entity =>
            {
                entity.ToTable("CookingMethod");

                entity.Property(e => e.CookingMethodId)
                    .ValueGeneratedNever()
                    .HasColumnName("cookingMethodId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<CronJobTimeConfig>(entity =>
            {
                entity.ToTable("CronJobTimeConfig");

                entity.Property(e => e.CronJobTimeConfigId)
                    .ValueGeneratedNever()
                    .HasColumnName("cronJobTimeConfigId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Hour).HasColumnName("hour");

                entity.Property(e => e.Minute).HasColumnName("minute");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.TargetObject).HasColumnName("targetObject");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("customerId");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsGoogle).HasColumnName("isGoogle");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<CustomerBadge>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.BadgeId })
                    .HasName("PK_CustomerReward");

                entity.ToTable("CustomerBadge");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.BadgeId).HasColumnName("badgeId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.CustomerBadges)
                    .HasForeignKey(d => d.BadgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBadge_Badge");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBadges)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBadge_Customer");
            });

            modelBuilder.Entity<CustomerVoucher>(entity =>
            {
                entity.ToTable("CustomerVoucher");

                entity.Property(e => e.CustomerVoucherId)
                    .ValueGeneratedNever()
                    .HasColumnName("customerVoucherId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerVouchers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerVoucher_Customer");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.CustomerVouchers)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerVoucher_Voucher");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId)
                    .ValueGeneratedNever()
                    .HasColumnName("ingredientId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.ListImage).HasColumnName("listImage");

                entity.Property(e => e.ListImagePosition).HasColumnName("listImagePosition");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TypeId).HasColumnName("typeId");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Ingredient_Type");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.NotificationId)
                    .ValueGeneratedNever()
                    .HasColumnName("notificationId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("orderId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.CustomerVoucherId).HasColumnName("customerVoucherId");

                entity.Property(e => e.IsCooked).HasColumnName("isCooked");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");

                entity.Property(e => e.PaymentMethod).HasColumnName("paymentMethod");

                entity.Property(e => e.PaypalUrl).HasColumnName("paypalUrl");

                entity.Property(e => e.ShippedAddress).HasColumnName("shippedAddress");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippedDate");

                entity.Property(e => e.ShippingCost)
                    .HasColumnType("money")
                    .HasColumnName("shippingCost");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("totalPrice");

                entity.Property(e => e.TransactionStatus).HasColumnName("transactionStatus");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.CustomerVoucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerVoucherId)
                    .HasConstraintName("FK_Order_CustomerVoucher");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("orderDetailId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PackageId).HasColumnName("packageId");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_OrderDetail_Package");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.Property(e => e.PackageId)
                    .ValueGeneratedNever()
                    .HasColumnName("packageId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.IsCooked).HasColumnName("isCooked");

                entity.Property(e => e.PackagePrice)
                    .HasColumnType("money")
                    .HasColumnName("packagePrice");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Package_Blog");
            });

            modelBuilder.Entity<PackageDetail>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.IngredientId })
                    .HasName("PK_RecipeDetail");

                entity.ToTable("PackageDetail");

                entity.Property(e => e.PackageId).HasColumnName("packageId");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.PackageDetails)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageDetail_Ingredient");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageDetails)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageDetail_Package");
            });

            modelBuilder.Entity<PriceNote>(entity =>
            {
                entity.ToTable("PriceNote");

                entity.Property(e => e.PriceNoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("priceNoteId");

                entity.Property(e => e.DateApply)
                    .HasColumnType("datetime")
                    .HasColumnName("dateApply");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientId");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionId)
                    .ValueGeneratedNever()
                    .HasColumnName("regionId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(100)
                    .HasColumnName("regionName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<SeasonReference>(entity =>
            {
                entity.ToTable("SeasonReference");

                entity.Property(e => e.SeasonReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("seasonReferenceId");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.Property(e => e.SubCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("subCategoryId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("typeId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("unitName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsGoogle).HasColumnName("isGoogle");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.VoucherId)
                    .ValueGeneratedNever()
                    .HasColumnName("voucherId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.MaximumOrderPrice)
                    .HasColumnType("money")
                    .HasColumnName("maximumOrderPrice");

                entity.Property(e => e.MinimumOrderPrice)
                    .HasColumnType("money")
                    .HasColumnName("minimumOrderPrice");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("datetime")
                    .HasColumnName("validFrom");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("datetime")
                    .HasColumnName("validTo");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
