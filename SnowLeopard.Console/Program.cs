using SnowLeopard.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnowLeopard.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var dal = new GroupDAL("Server=localhost;Port=7002;Database=ZGD;User=root;Password=abcd-1234;");
            dal.TT();

            System.Console.WriteLine("Hello World!");
        }
    }

    public class GroupDAL : BaseDAL<Group>
    {
        public GroupDAL(string connStr) : base(connStr)
        {
        }

        public void TT()
        {
            var sql = "SELECT * FROM groups ORDER BY GroupStatus ASC,MemberCount DESC,EntryDeadline ASC;";

            var res = Query(sql);
        }
    }

    /// <summary>
    /// 组队表
    /// </summary>
    [Table("Groups")]
    public class Group
    {
        [Key]
        public Guid Id { get; set; }

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
        /// 活动结束时间
        /// </summary>
        [Display(Name = "活动结束时间")]
        public DateTime ActivityDeadline { get; set; }

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
    }

}
