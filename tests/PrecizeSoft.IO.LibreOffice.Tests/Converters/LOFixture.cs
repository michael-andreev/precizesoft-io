using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Tests.Converters
{
    public class LOFixture: IDisposable
    {
        public LOFixture()
        {
            LOEnvironment.ConfigureFromRegistry();
        }

        public void Dispose()
        {
        }
    }
}
