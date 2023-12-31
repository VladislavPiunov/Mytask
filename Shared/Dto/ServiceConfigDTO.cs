﻿using Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class ServiceConfigDTO
    {
        public DescriptionConfiguration DescConf { get; init; }

        public ConnectionsConfiguration EnvireConf { get; init; }

        public ServiceConfigDTO(DescriptionConfiguration descConf, ConnectionsConfiguration envireConf)
        {
            DescConf = descConf;
            EnvireConf = envireConf;
        }
    }
}
