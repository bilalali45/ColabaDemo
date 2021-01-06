using LOSAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteWebConnector.SDK.Models;

namespace ByteWebConnector.SDK
{
    public sealed class ByteProSession
    {
        private static SDKSession _byteSession;
        private static readonly object padlock = new object();


        public static SDKSession GetInstance(ByteProSettings settings)
        {
            lock (padlock)
            {
                if (_byteSession == null)
                {
                    _byteSession = new SDKSession();
                    try
                    {
                        _byteSession.Login(settings.ByteUserName, settings.BytePassword, settings.ByteConnectionName);
                        _byteSession.Authorize(settings.ByteCompanyCode, settings.ByteUserNo, settings.ByteAuthKey);
                    }
                    catch (Exception)
                    {
                        _byteSession = null;
                    }
                    
                }
                return _byteSession;
            }
        }


        public static void ResetSession()
        {
            lock (padlock)
            {
                if (_byteSession != null)
                {
                    _byteSession = null;
                }
            }
        }
    }
}
