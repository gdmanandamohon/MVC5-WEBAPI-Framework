using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebClient.DataAccess
{
    public class FDBContext : DbContext
    {
        public FDBContext()
            : base("name=FDBContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
    }
}