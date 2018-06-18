using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SnowLeopard.Extensions;
using SnowLeopard.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Threading.Tasks;

namespace SnowLeopard.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "Server=localhost;Port=7002;Database=ZGD;User=root;Password=abcd-1234;";
            IServiceCollection services = new ServiceCollection();
            services.AddMySqlDbConnection((x) =>
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<GroupDAL>().As<GroupDAL>();

            var container = builder.Build();

            var dal = container.Resolve<GroupDAL>();
            //dal.InsertAsync().Wait();

            dal.BatchInsertAsync().Wait();

            //var dal = new GroupDAL("Server=localhost;Port=7002;Database=ZGD;User=root;Password=abcd-1234;");

            //dal.TT();
            System.Console.ReadKey();
            System.Console.WriteLine("Hello World!");
        }
    }

    public class GroupDAL : BaseDAL<Group>
    {
        private IDbConnection _dbConnection;
        public GroupDAL(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <returns></returns>
        public async Task BatchInsertAsync()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            IDbTransaction tran = _dbConnection.BeginTransaction();

            try
            {
                var batchInsertSql = @"INSERT INTO `groups` 
(`CoverUrl`, `CreateTime`, `Describe`, `EntryDeadline`, `GenderRequirements`, `GroupStatus`, `IsTimeNo`, `MemberCount`, `MemberLimit`, `PayType`, `TeamLeaderId`)
VALUES (@CoverUrl,@CreateTime,@Describe,@EntryDeadline,@GenderRequirements,@GroupStatus,@IsTimeNo,@MemberCount,@MemberLimit,@PayType,@TeamLeaderId)";

                var objs = new Group[10];
                for (int i = 0; i < 10; i++)
                {
                    objs[i] = new Group
                    {
                        CoverUrl = i.ToString(),
                        EntryDeadline = DateTime.Now,
                        GroupStatus = 1,
                        MemberCount = i,
                        MemberLimit = i,
                        PayType = i,
                        TeamLeaderId = i,
                        Describe = i.ToString(),
                        IsTimeNo = true,
                        GenderRequirements = i,
                        CreateTime = DateTime.Now
                    };
                }

                await ExecuteAsync(batchInsertSql, tran, objs);

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                System.Console.WriteLine(ex);
            }
        }

        public async Task InsertAsync()
        {
            _dbConnection.Open();
            var model = new Group()
            {
                CoverUrl = "",
                EntryDeadline = DateTime.Now,
                GroupStatus = 1,
                MemberCount = 1,
                MemberLimit = 1,
                PayType = 1,
                TeamLeaderId = 2,
                Describe = "",
                IsTimeNo = true,
                GenderRequirements = 2,
                CreateTime = DateTime.Now
            };
            var res = await InsertAsync(model);
        }
    }

    /// <summary>
    /// 组队表
    /// </summary>
    [Table("Groups")]
    public class Group : TopBasePoco
    {
        /// <summary>
        /// 队长
        /// </summary>
        [Display(Name = "队长")]
        public long TeamLeaderId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [Display(Name = "描述")]
        public string Describe { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [Required]
        [Display(Name = "封面")]
        public string CoverUrl { get; set; }

        /// <summary>
        /// 人数上限
        /// </summary>
        [Display(Name = "人数上限")]
        public int MemberLimit { get; set; }

        /// <summary>
        /// 成员数
        /// </summary>
        [Display(Name = "成员数")]
        public int MemberCount { get; set; }

        /// <summary>
        /// 报名截止时间
        /// </summary>
        [Display(Name = "报名截止时间")]
        public DateTime EntryDeadline { get; set; }

        /// <summary>
        /// 性别要求
        /// </summary>
        [Display(Name = "性别要求")]
        public int GenderRequirements { get; set; }

        /// <summary>
        /// 买单类型
        /// </summary>
        [Display(Name = "买单类型")]
        public int PayType { get; set; }

        /// <summary>
        /// 组队时间
        /// </summary>
        [Display(Name = "组队时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 组队状态
        /// </summary>
        [Display(Name = "组队状态")]
        public int GroupStatus { get; set; }

        public bool IsTimeNo { get; set; }
    }

}
