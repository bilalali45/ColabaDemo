﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface IRedisService
    {
        Task Run();
    }
}
