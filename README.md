# FastSql
example:


  static string sqlcon = "Data Source = 127.0.0.1;Initial Catalog = Test;User Id = sa;Password = sa@2019;";
  
        static void Main(string[] args)
        {
            var Sql = new CreateSql<Users>().Select().Where("ID=@ID");
            
            using (var conn = new SqlConnection(sqlcon))
            
            {
            
                var rdata = conn.SqlQuery(Sql.ToSqlString(),new { ID=Guid.Parse("088941FE-075A-42C6-8937-5D1A1B795ACD") });
                
                Console.WriteLine(rdata);
            }
            

            Console.ReadKey();
        }
