using MySql.Data.Entity;
using System.Data.Entity;

namespace Able.Store.InfrsturctureProvider.Domain
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StoreProviderContext : DbContext
    {
        public StoreProviderContext() : base("store")
        {
            
        }
        static StoreProviderContext()
        {
           
            Database.SetInitializer<StoreProviderContext>(null);
        }
    }
}
