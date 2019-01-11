using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.DependencyInjection;

namespace SnowLeopard.Redis.ConsoleTest
{
    class Program
    {
        static void Run()
        {
            var redisCache = GlobalServices.GetRequiredService<IRedisCache>();
            var key = "123";
            var value = "123";
            var count = 0;
            var finish = 0;
            var taskCount = 5;
            var obj = new object();
            for (int i = 0; i < taskCount; i++)
            {
                Task.Run(() =>
                {
                    if (redisCache.Lock(key))
                    {
                        redisCache.Set(key, value);
                        count++;
                        redisCache.UnLock(key);
                    }
                    Thread.Sleep(20);
                    Console.WriteLine($"Value:{redisCache.Get<string>(key)}");
                    lock (obj)
                    {
                        finish++;
                    }
                });
            }
            //for (int i = 0; i < taskCount; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        redisCache.Set(key, value);
            //        count++;
            //        finish++;
            //        Console.WriteLine($"Value:{redisCache.Get<string>(key)}");
            //    });
            //}

            while (true)
            {
                if (finish == taskCount)
                    break;
                Thread.Sleep(10);
            }
            Console.WriteLine($"Count:{count}");
            Console.ReadLine();

            //redisCache.KeyDelete("Q_Flag_SignIn1");
            //redisCache.KeyDelete("Q_Flag_SignIn2");
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //int count = 0;
            //for (int j = 0; j < 50; j++)
            //{
            //    Task.Run(async () =>
            //    {
            //        for (int i = 0; i < 5000; i++)
            //        {
            //            //await redisCache.HSetAsync("Q_Flag_SignIn2", Guid.NewGuid().ToString(), new
            //            //{
            //            //    userCode = "1850410605",
            //            //    Longitude = 117.111464,
            //            //    Latitude = 39.070614,
            //            //    Place = "压测",
            //            //    SignInTime = "2018-11-07 16:12:13"
            //            //});
            //            await redisCache.LPushAsync("Q_Flag_SignIn1", new
            //            {
            //                userCode = "1850410605",
            //                Longitude = 117.111464,
            //                Latitude = 39.070614,
            //                Place = "压测",
            //                SignInTime = "2018-11-07 16:12:13"
            //            });
            //        }
            //        count++;
            //        if (count == 50)
            //        {
            //            stopwatch.Stop();
            //            Console.WriteLine($"耗时：{stopwatch.ElapsedMilliseconds}");
            //        }
            //    });
            //}
        }

        public static void Main(string[] args)
        {
            RegisterServices();
            //var redisCache = GlobalServices.GetRequiredService<IRedisCache>();
            Run();

            Console.ReadKey();
            //HashSetTest(redisCache);
            //HashGetTest(redisCache);
            //HashGetAllTest(redisCache);
            //Debug.WriteLine(redisCache.HLen("TestHashID"));

            //Debug.WriteLine(redisCache.HDel("TestHashID", "1"));
            //Debug.WriteLine(redisCache.HDel("TestHashID", new string[] { "1", "2", "3", "4" }));
            //Debug.WriteLine(redisCache.HExists("TestHashID", "1"));
            //Debug.WriteLine(redisCache.HExists("TestHashID", "99"));

            //var keys = redisCache.HKeys("TestHashID");
            //foreach (var item in keys)
            //{
            //    Debug.WriteLine(item);
            //}

            //var values = redisCache.HValues<string>("TestHashID");
            //foreach (var item in values)
            //{
            //    Debug.WriteLine(item);
            //}


            //Console.ReadKey();


            //Task.Factory.StartNew(() =>
            //{
            //    int index = 0;
            //    while (true)
            //    {
            //        var sub = redisCache.GetSubscriber();
            //        sub.Publish("message", "value" + index++);
            //    }
            //});
            //Task.Factory.StartNew(() =>
            //{
            //    var sub = redisCache.GetSubscriber();
            //    sub.Subscribe("message", (channel, message) =>
            //    {
            //        Console.WriteLine(message);
            //        // Thread.Sleep(1000);
            //    });
            //});
            //Task.Factory.StartNew(() =>
            //{
            //    var sub = redisCache.GetSubscriber();
            //    sub.Subscribe("message", (channel, message) =>
            //    {
            //        Console.WriteLine("Task111111111111111111111111111111111111111:" + message);
            //        //Thread.Sleep(1000);
            //    });
            //});
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
