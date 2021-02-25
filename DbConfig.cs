using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSql
{
   public class DbConfig
    {
        public static string SqlConnectString = ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
    }
}
