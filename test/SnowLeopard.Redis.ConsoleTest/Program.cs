using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.DependencyInjection;
using StackExchange.Redis;

namespace SnowLeopard.Redis.ConsoleTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            var ss = ConfigurationOptions.Parse("10.9.101.72:6379,name=SnowLeopard.Redis.ConsoleTest,ConnectTimeout=60000,AsyncTimeout=60000,SyncTimeout=60000,password=");

            RegisterServices();
            var redisCache = GlobalServices.GetRequiredService<IRedisCache>();

            var _contentRootPath = Directory.GetCurrentDirectory();
            HashSetTest(redisCache);
            HashGetTest(redisCache);
            HashGetAllTest(redisCache);
            Debug.WriteLine(redisCache.HLen("TestHashID"));

            Debug.WriteLine(redisCache.HDel("TestHashID", "1"));
            Debug.WriteLine(redisCache.HDel("TestHashID", new string[] { "1", "2", "3", "4" }));
            Debug.WriteLine(redisCache.HExists("TestHashID", "1"));
            Debug.WriteLine(redisCache.HExists("TestHashID", "99"));

            var keys = redisCache.HKeys("TestHashID");
            foreach (var item in keys)
            {
                Debug.WriteLine(item);
            }

            var values = redisCache.HValues<string>("TestHashID");
            foreach (var item in values)
            {
                Debug.WriteLine(item);
            }


            Console.ReadKey();


            Task.Factory.StartNew(() =>
            {
                int index = 0;
                while (true)
                {
                    var sub = redisCache.GetSubscriber();
                    sub.Publish("message", "value" + index++);
                }
            });
            Task.Factory.StartNew(() =>
            {
                var sub = redisCache.GetSubscriber();
                sub.Subscribe("message", (channel, message) =>
                {
                    Console.WriteLine(message);
                    // Thread.Sleep(1000);
                });
            });
            Task.Factory.StartNew(() =>
            {
                var sub = redisCache.GetSubscriber();
                sub.Subscribe("message", (channel, message) =>
                {
                    Console.WriteLine("Task111111111111111111111111111111111111111:" + message);
                    //Thread.Sleep(1000);
                });
            });
            //Task.Factory.StartNew(() =>
            //{
            //    var index = 0;
            //    while (true)
            //    {
            //        redisCache.Set("key" + index, "value" + index);
            //        index++;
            //    }
            //});
            //Task.Factory.StartNew(() =>
            //{
            //    var index = 0;
            //    while (true)
            //    {
            //        redisCache.HSet("HashId1", Guid.NewGuid().ToString(), "value" + index);
            //        index++;
            //    }
            //});


            Console.ReadKey();
        }

        public static void HashSetTest(IRedisCache redisCache)
        {
            var res = redisCache.KeyDelete("TestHashID");
            for (int i = 0; i < 100; i++)
            {
                if (i == 78)
                    redisCache.HSet("TestHashID", "78", "");
                else
                    redisCache.HSet("TestHashID", i.ToString(), i.ToString());
            }
        }

        public static void HashGetTest(IRedisCache redisCache)
        {
            var res = redisCache.HGet<string>("TestHashID", "0");
            Debug.WriteLine("HashGet:" + res);
            Debug.WriteLine("===================================");
            var resList = redisCache.HGet<string>("TestHashID", new string[] { "1", "200", "6", "78", "133", "300", "100", "56", "99" });
            foreach (var item in resList)
            {
                Debug.WriteLine(item);
            }
        }

        public static void HashGetAllTest(IRedisCache redisCache)
        {
            var resList = redisCache.HGetAll<string>("TestHashID");
            foreach (var item in resList)
            {
                Debug.WriteLine(item);
            }
        }


        static void RegisterServices()
        {
            var configurationBuilder = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                            .AddEnvironmentVariables();

            var hostingConfig = configurationBuilder.Build();

            var services = new ServiceCollection();
            services.AddSnowLeopardRedis(hostingConfig);
            services.AddSnowLeopardServices();

            var container = new ContainerBuilder();
            container.Populate(services);

            GlobalServices.SetServiceProvider(new AutofacServiceProvider(container.Build()));
        }
    }
}
