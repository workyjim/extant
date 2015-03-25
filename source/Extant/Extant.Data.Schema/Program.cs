using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;

namespace Extant.Data.Schema
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtantSessionFactory.GetConfig("Data Source=localhost;Initial Catalog=Inbank_Extant; Integrated Security=SSPI")
                                    .ExposeConfiguration(cfg => new SchemaExport(cfg).SetOutputFile(args[0]).Create(true, false))
                                    .BuildConfiguration();
        }
    }
}
