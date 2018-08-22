using System.ComponentModel.DataAnnotations;

namespace SnowLeopard.Lynx.Enums
{
    /// <summary>
    /// DayOfWeek
    /// </summary>
    public enum DayOfWeekEnum
    {
        /// <summary>
        /// Monday
        /// </summary>
        [Display(Name = "周一")]
        Monday = 1,
        /// <summary>
        /// Tuesday
        /// </summary>
        [Display(Name = "周二")]
        Tuesday = 2,
        /// <summary>
        /// Wednesday
        /// </summary>
        [Display(Name = "周三")]
        Wednesday = 3,
        /// <summary>
        /// Thursday
        /// </summary>
        [Display(Name = "周四")]
        Thursday = 4,
        /// <summary>
        /// Friday
        /// </summary>
        [Display(Name = "周五")]
        Friday = 5,
        /// <summary>
        /// Saturday
        /// </summary>
        [Display(Name = "周六")]
        Saturday = 6,
        /// <summary>
        /// Sunday
        /// </summary>
        [Display(Name = "周日")]
        Sunday = 7
    }
}
