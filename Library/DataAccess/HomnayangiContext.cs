using System;
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
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogReaction> BlogReactions { get; set; }
        public virtual DbSet<BlogSubCate> BlogSubCates { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerReward> CustomerRewards { get; set; }
        public virtual DbSet<CustomerVoucher> CustomerVouchers { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCookedDetail> OrderCookedDetails { get; set; }
        public virtual DbSet<OrderIngredientDetail> OrderIngredientDetails { get; set; }
        public virtual DbSet<OrderPackageDetail> OrderPackageDetails { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeDetail> RecipeDetails { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
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

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.VideoUrl).HasColumnName("videoURL");

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

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.BlogId)
                    .ValueGeneratedNever()
                    .HasColumnName("blogId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.BlogStatus).HasColumnName("blogStatus");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Finished).HasColumnName("finished");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Preparation).HasColumnName("preparation");

                entity.Property(e => e.Processing).HasColumnName("processing");

                entity.Property(e => e.Reaction).HasColumnName("reaction");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.VideoUrl).HasColumnName("videoURL");

                entity.Property(e => e.View).HasColumnName("view");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Blog_User");
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

            modelBuilder.Entity<BlogSubCate>(entity =>
            {
                entity.HasKey(e => new { e.BlogId, e.SubCateId });

                entity.ToTable("BlogSubCate");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.SubCateId).HasColumnName("subCateId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

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

            modelBuilder.Entity<CustomerReward>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RewardId });

                entity.ToTable("CustomerReward");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.RewardId).HasColumnName("rewardId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerRewards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerReward_Customer");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.CustomerRewards)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerReward_Reward");
            });

            modelBuilder.Entity<CustomerVoucher>(entity =>
            {
                entity.HasKey(e => new { e.VoucherId, e.CustomerId });

                entity.ToTable("CustomerVoucher");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

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

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantitative).HasColumnName("quantitative");

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

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");

                entity.Property(e => e.ShippedAddress).HasColumnName("shippedAddress");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippedDate");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("totalPrice");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.OrderNavigation)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Order>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Transaction");
            });

            modelBuilder.Entity<OrderCookedDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.RecipeId });

                entity.ToTable("OrderCookedDetail");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.RecipeId).HasColumnName("recipeId");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Session).HasColumnName("session");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCookedDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCookedDetail_Order");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.OrderCookedDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCookedDetail_Recipe");
            });

            modelBuilder.Entity<OrderIngredientDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.IngredientId });

                entity.ToTable("OrderIngredientDetail");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientId");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.OrderIngredientDetails)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderIngredientDetail_Ingredient");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderIngredientDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderIngredientDetail_Order");
            });

            modelBuilder.Entity<OrderPackageDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.RecipeId });

                entity.ToTable("OrderPackageDetail");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.RecipeId).HasColumnName("recipeId");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPackageDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPackageDetail_Order");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.OrderPackageDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPackageDetail_Recipe");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.RecipeId)
                    .ValueGeneratedNever()
                    .HasColumnName("recipeId");

                entity.Property(e => e.CookedPrice)
                    .HasColumnType("money")
                    .HasColumnName("cookedPrice");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.PackagePrice)
                    .HasColumnType("money")
                    .HasColumnName("packagePrice");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.RecipeNavigation)
                    .WithOne(p => p.Recipe)
                    .HasForeignKey<Recipe>(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Blog");
            });

            modelBuilder.Entity<RecipeDetail>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId });

                entity.ToTable("RecipeDetail");

                entity.Property(e => e.RecipeId).HasColumnName("recipeId");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeDetails)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeDetail_Ingredient");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeDetail_Recipe");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Reward");

                entity.Property(e => e.RewardId)
                    .ValueGeneratedNever()
                    .HasColumnName("rewardId");

                entity.Property(e => e.ConditionType).HasColumnName("conditionType");

                entity.Property(e => e.ConditionValue).HasColumnName("conditionValue");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
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
                    .HasConstraintName("FK_Tag_Category");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("transactionId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("totalAmount");

                entity.Property(e => e.TransactionStatus).HasColumnName("transactionStatus");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Transaction_Customer");
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

                entity.Property(e => e.MaximumOrder)
                    .HasColumnType("money")
                    .HasColumnName("maximumOrder");

                entity.Property(e => e.MinimumOrder)
                    .HasColumnType("money")
                    .HasColumnName("minimumOrder");

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
