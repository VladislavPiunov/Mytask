using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mytask.API.Extensions.Options
{
    public class ConnectionsConfiguration
    {
        public string ConnectionName { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Mongo { get; set; }
    }
}
