using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Identity.Models
{
    //public class RedisHost
    //{
    //    public string Host { get; set; }
    //    public int Port { get; set; }
    //}
    public class RedisParameters
    {
        public string Password { get; set; }
        public RedisHost[] Hosts { get; set; }
    }
}
