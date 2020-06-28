using System;
using System.Collections.Generic;
using System.Text;
using Apple.Data.Interface;

namespace Apple.Data.Sql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppleDbContext _db;

        public UnitOfWork(AppleDbContext db)
        {
            _db = db;
            Category = new CategorySql(_db);
            ProductType = new ProductTypeSql(_db);
            Catalog = new CatalogSql(_db);
            ApplicationUser = new ApplicationUserSql(_db);
            ShoppingCart = new ShoppingCartSql(_db);
            OrderHeader = new OrderHeaderSql(_db);
            OrderDetails = new OrderDetailsSql(_db);
        }

        public ICategory Category { get; private set; }
        public IProductType ProductType { get; private set; }
        public ICatalog Catalog { get; private set; }
        public IApplicationUser ApplicationUser { get; private set; }
        public IShoppingCart ShoppingCart { get; private set; }

        public IOrderHeader OrderHeader { get; private set; }

		public IOrderDetails OrderDetails { get; private set; }

		public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
