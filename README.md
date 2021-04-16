
    1.查询表数据：
写法1：

 var sql = new CreateSql<Users>().Select();
 var rdata = DapperExt.QueryList(sql);
写法2：
 
 var lsit= new CreateSql<Users>().Select().QueryList();

写法3：可以重载指定表名，或指定查询的字段如：

   var sql = new CreateSql<Users>("Users").Select(new string[] {"Id","UserName" });
   var rdata = DapperExt.QueryList(sql);
 或者
 
var list= new CreateSql<Users>("Users").Select(new string[] {"Id","UserName" }).
QueryList();

2.查询单个对象

var sql = new CreateSql<Users>().Select().Where("Id='843DB63F-252E-436F-B666-F4F8755793B8'");
var rdata = DapperExt.QueryList(sql);
 
或者

Var model=CreateSql<Users>().Select() 
.Where("Id='843DB63F-252E-436F-B666-F4F8755793B8'")
.QueryFirst();

3.非锁表查询（常用于事务中查询）
var sql = new CreateSql<Users>().SelectNoLock().Where("Id='843DB63F-252E-436F-B666-F4F8755793B8'"); 
var rdata = DapperExt.QueryList(sql);
或者
 
 var data= new CreateSql<Users>().SelectNoLock().Where("Id='843DB63F-252E-436F-B666-F4F8755793B8'").QueryFirst();

4.新增操作

var model = new Users() { 
Id = Guid.NewGuid(),
 Age = 20,
 Sex = "女",
 UserName = "test",
 addr = "",
 CreateTime = DateTime.Now };
   var sql = new CreateSql<Users>().Insert();
   var rdata = DapperExt.Add(sql, model);
或者
 var model = new Users() { Id = Guid.NewGuid(), Age = 20, Sex = "女", UserName = "test", addr = "", CreateTime = DateTime.Now };
 var sql = new CreateSql<Users>().Insert().Add(model);
5.修改操作

 var model = new Users() { Id = Guid.NewGuid(), Age = 20, Sex = "女", UserName = "test", addr = "", CreateTime = DateTime.Now };
var sql = new CreateSql<Users>().Updata().Where($"Id='843DB63F-252E-436F-B666-F4F8755793B8'");
var rdata = DapperExt.Modify(sql, model);
 
或者

 var model = new Users() { Id = Guid.NewGuid(), Age = 20, Sex = "女", UserName = "test", addr = "", CreateTime = DateTime.Now };
 var sql = new CreateSql<Users>().Updata().Where($"Id='843DB63F-252E-436F-B666-F4F8755793B8'").Modify(model);

6.删除操作

  var sql = new CreateSql<Users>().Delete().Where($"Id='{data.Id}'");
 
  var rdata = DapperExt.Remove(sql);
 
或者

  var sql = new CreateSql<Users>().Delete().Where($"Id=@Id");
   var rdata = DapperExt.Remove(sql,new { Id=Guid.Parse("F91BEE4E-67BE-4080-9C11-98C1A610F5C5") });


7.事务操作

说明：事务封装类：DapperTransaction，构造参数可以传入数据库连接字符串：
var dt = new DapperTransaction(DbConfig.SqlConnectString)
1.事务处理操作：

  var model = new Users() { 
Id = Guid.NewGuid(),
 Age = 20, Sex = "女",
 UserName = "test", 
addr = "", 
CreateTime = DateTime.Now };

  using (var dt = new DapperTransaction(DbConfig.SqlConnectString))
  
  {
  
var data = new CreateSql<Users>().Insert()
 
.Add(dt, model);

    model.UserName = "69855";
    
var data2 = new CreateSql<Users>().SelectNoLock()
 
.Where($"Id='{model.Id}'").QueryFirst();

    data2.UserName = "mystest";
    
 var data3 = new CreateSql<Users>().Updata().Where($"Id='{data2.Id}'")
 
.Modify(dt, data2);

   dt.Commit(); 

   }
    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------
2.0 版本更新内容：
           1.新增拉母表达式sql解析，例如：
           
           //select
           
            var sql = new CreateSql<Users>().Select(s => new { s.Id, s.UserName }).Where(w=>w.Sex=="男").QueryList();


            var user = new Users();
            
            //insert
            new CreateSql<Users>().Insert().Add(user);

            //update
            
            new CreateSql<Users>().Updata().Where(w=>w.Id==Guid.Empty).Modify(user);

            new CreateSql<Users>().Updata(u => u.UserName).Where(w => w.Id == Guid.Empty).Modify(user);

            //delete
            
            new CreateSql<Users>().Delete().Where(w=>w.Id==Guid.Empty).Remove();
