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
            services.AddDbConnection((x) =>
            {
                return new MySqlConnection(connStr);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<GroupDAL>().As<GroupDAL>();

            var container = builder.Build();

            var dal = container.Resolve<GroupDAL>();
            dal.TT().Wait();

            //var dal = new GroupDAL("Server=localhost;Port=7002;Database=ZGD;User=root;Password=abcd-1234;");

            //dal.TT();

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

        public async Task TT()
        {
            _dbConnection.Open();
            //IDbTransaction tran = _dbConnection.BeginTransaction();
            //var sql = "SELECT * FROM groups ORDER BY GroupStatus ASC,MemberCount DESC,EntryDeadline ASC;";

            var sql1 = "INSERT INTO `groups` (`Id`, `CoverUrl`, `CreateTime`, `Describe`, `EntryDeadline`, `GenderRequirements`, `GroupStatus`, `IsTimeNo`, `MemberCount`, `MemberLimit`, `PayType`, `TeamLeaderId`) VALUES ('1', '1', '2018-6-13 12:51:36', '1', '2018-6-13 12:51:41', 1, 1, '', 1, 1, 1, '1');";
            var sql2 = "INSERT INTO `groups` (`Id`, `CoverUrl`, `CreateTime`, `Describe`, `EntryDeadline`, `GenderRequirements`, `GroupStatus`, `IsTimeNo`, `MemberCount`, `MemberLimit`, `PayType`, `TeamLeaderId`) VALUES ('1', '2', '2018-6-13 12:51:36', '2', '2018-6-13 12:51:41', 2, 2, '', 2, 2, 2, '2');";

            try
            {
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

                //await ExecuteAsync(sql1, null, tran);
                //await ExecuteAsync(sql2, null, tran);

                //tran.Commit();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                System.Console.WriteLine(ex);
            }
            //var res = Query(sql);
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
