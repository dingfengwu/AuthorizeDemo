using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication3;
using WebApplication3.Db;
using WebApplication3.Db.Model;
using Xunit;
using Z.EntityFramework.Extensions;

namespace WebDemoTest
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TestEntityFramework
    {
        TestServer _server;
        Startup _start;
        IApplicationBuilder _builder;
        IConfigurationRoot _config;

        public object SqlBulkCopy { get; private set; }

        public TestEntityFramework()
        {
            CreateServer();
        }

        [Fact]
        public void TestBulkInsert()
        {
            using (var dbcontext = new ApplicationDbContext(_config["Data:Default"]))
            {
                var list = new List<User>();
                for (var i = 0; i < 10000; i++)
                {
                    list.Add(new User
                    {
                        Address = "DONGGUANG",
                        Id = i.ToString(),
                        Name = "Tom" + i
                    });
                }


                //dbcontext.AddRange(list);

                var start = DateTime.Now;
                //dbcontext.SaveChanges();
                dbcontext.BulkInsert(list);
                var end = DateTime.Now;
                Console.WriteLine($"total second is {(end - start).TotalSeconds}");
            }
        }

        [Fact]
        public async void CustomBulkInsert()
        {
            using (var dbcontext = new ApplicationDbContext(_config["Data:Default"]))
            {
                try
                {
                    var result=Parallel.For(1, 10001, async i =>
                     {
                         var size = 10000 * 10000 / 10000;
                         var list = new List<User>(size);
                         for (var j = (i-1) * size; j <= i * size; j++)
                         {
                             list.Add(new User
                             {
                                 Address = "DONGGUANG",
                                 Id = j.ToString(),
                                 Name = "Tom" + j
                             });
                         }
                         await BulkInsert(list);
                     });
                

                
                //dbcontext.AddRange(list);
                var start = DateTime.Now;
                //dbcontext.SaveChanges();
                //dbcontext.BulkInsert(list);
                
                var end = DateTime.Now;
                Console.WriteLine($"total second is {(end - start).TotalSeconds}");
                }
                catch (AggregateException ex)
                {

                }
            }
        }

        private async Task BulkInsert<TModel>(List<TModel> list) where TModel:class,new()
        {
            DataTable table = new DataTable();
            DataRow tmpDr = null;
            PropertyInfo propInfo;
            var type = typeof(TModel);
            var connectionString = _config["Data:Default"];
            SqlBulkCopy bulk = new SqlBulkCopy(connectionString);
            foreach(var prop in typeof(TModel).GetProperties())
            {
                bulk.ColumnMappings.Add(new SqlBulkCopyColumnMapping
                {
                    SourceColumn=prop.Name,
                    DestinationColumn=prop.Name
                });

                table.Columns.Add(prop.Name);
            }
            list.ForEach(item =>
            {
                tmpDr =table.NewRow();
                foreach (SqlBulkCopyColumnMapping col in bulk.ColumnMappings)
                {
                    propInfo = type.GetProperty(col.SourceColumn);
                    tmpDr[col.DestinationColumn] = propInfo.GetValue(item);
                }
                table.Rows.Add(tmpDr);
            });
            bulk.BatchSize = 10000;
            bulk.BulkCopyTimeout = 5 * 60;
            bulk.DestinationTableName = "[" + typeof(TModel).Name + "]";
            await bulk.WriteToServerAsync(table);
        }

        private void CreateServer()
        {
            _start = new Startup(null);
            _server = TestServer.Create((IApplicationBuilder builder) =>
            {
                _builder = builder;
                _config = _builder.ApplicationServices.GetService(typeof(IConfigurationRoot)) as IConfigurationRoot;
                var env = builder.ApplicationServices.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
                var logger = builder.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
                _start.Configure(builder, env, logger);
            }, 
            service =>
            {
                _start.ConfigureServices(service);
            });


            //WebHostBuilder builder = new WebHostBuilder()..Build().Start();
        }

    }
}
