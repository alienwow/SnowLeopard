using Microsoft.AspNetCore.Mvc;
using SnowLeopard.Infrastructure.Filters;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// MvcOptions Extension
    /// </summary>
    public static class MvcOptionsExtension
    {
        /// <summary>
        /// AddSnowLeopardFilters
        /// </summary>
        /// <param name="self"></param>
        public static void AddSnowLeopardFilters(this MvcOptions self)
        {
            self.Filters.Add(typeof(GlobalExceptionFilter));
            self.Filters.Add(typeof(ModelStateFilter));
            self.Filters.Add(typeof(ResultFilter));
        }

    }
}
