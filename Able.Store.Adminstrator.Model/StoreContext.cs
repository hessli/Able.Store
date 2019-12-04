using Able.Store.Adminstrator.Model.AdvertDomain;
using Able.Store.Adminstrator.Model.BasketsDomain;
using Able.Store.Adminstrator.Model.CategoriesDomain;
using Able.Store.Adminstrator.Model.OrdersDomain;
using Able.Store.Adminstrator.Model.ProductsDomain;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Adminstrator.Model.UsersDomain;
using MySql.Data.Entity;
using System.Data.Entity;
namespace Able.Store.Adminstrator.Model
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StoreContext : DbContext
    {
        public StoreContext() : base("store")
        {
           var q=  base.Database.Connection.ConnectionString;
        }
        static StoreContext()
        { 
            //从不创建数据库
            //可以提升效率，不用判断表库是否存在
            Database.SetInitializer<StoreContext>(null);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<SkuSale> SkuSales { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuStock> SkuStocks { get; set; }
        public DbSet<SkuAttribute> SkuAttributes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<BasketSku> BasketSkus { get; set; }
        public DbSet<BasketSkuAttribute> BasketSkuAttributes { get; set; }
         public DbSet<Order> Orders { get; set; }
         public DbSet<OrderItem> OrderItems { get; set; }
         public DbSet<OrderItemSku> OrderItemSkus { get; set; }
         public DbSet<OrderAddress> OrderAddresses { get; set; }
         public DbSet<OrderReceiver> OrderReceivers { get; set; }
         public DbSet<OrderPayment> OrderPayments { get; set; }

        //public DbSet<OrderShipping> OrderShippings { get; set; }
        //public DbSet<OrderShippingLocus> OrderShippingLocus { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMoney>().HasRequired(s => s.User)
                .WithMany(s => s.UserMoneys).WillCascadeOnDelete(true);

           
            base.OnModelCreating(modelBuilder);

        }
        public void FixEfProviderServicesProblem()
        {
        }
    }
}
