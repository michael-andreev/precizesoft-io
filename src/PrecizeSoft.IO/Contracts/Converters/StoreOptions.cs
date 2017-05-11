using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public class StoreOptions
    {
        public TimeSpan FileLifeTime { get; set; } = TimeSpan.FromHours(1);
    }
}
