using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrecizeSoft.IO.Contracts.ConversionStatistics;

namespace PrecizeSoft.IO.WebApi.Controllers.ConversionStatistics.V1
{
    public abstract class ConversionStatisticsV1ControllerBase : Controller
    {
        protected abstract ISummaryStat GetSummaryStatInternal();

        protected abstract IEnumerable<IStatByFileCategory> GetStatByFileCategoriesInternal();

        protected abstract IEnumerable<IStatByHour> GetDailyStatInternal(DateTimeOffset dateWithTimeZone);

        /// <summary>
        /// Get summary statistics
        /// </summary>
        /// <returns>Summary statistics</returns>
        [HttpGet("summary")]
        [Produces("application/json")]
        public virtual ISummaryStat GetSummaryStat()
        {
            return this.GetSummaryStatInternal();
        }

        /// <summary>
        /// Get summary statistics by file categories
        /// </summary>
        /// <returns>List of file categories</returns>
        [HttpGet("fileCategories")]
        [Produces("application/json")]
        public virtual IEnumerable<IStatByFileCategory> GetStatByFileCategories()
        {
            return this.GetStatByFileCategoriesInternal();
        }

        /// <summary>
        /// Get hourly statistics for one day
        /// </summary>
        /// <param name="dateWithTimeZone">Format: YYYY-MM-DD+ZH:ZM (see [RFC 3339])</param>
        /// <returns>List of hours</returns>
        [HttpGet("hours/getByDate")]
        [Produces("application/json")]
        public virtual IEnumerable<IStatByHour> GetDailyStat([FromQuery] DateTimeOffset dateWithTimeZone)
        {
            return this.GetDailyStatInternal(dateWithTimeZone);
        }
    }
}
