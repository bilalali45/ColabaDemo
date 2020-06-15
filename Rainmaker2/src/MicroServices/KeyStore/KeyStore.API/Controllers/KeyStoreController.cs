using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyStore.API.Controllers
{
    [Route("api/Keystore/[controller]")]
    [ApiController]
    public class KeyStoreController : Controller
    {
        private readonly IKeyStore keyStore;
        public KeyStoreController(IKeyStore keyStore)
        {
            this.keyStore = keyStore;
        }
        [HttpGet]
        public string Get(string key)
        {
            var keyData = keyStore.Get(key);
            if (string.IsNullOrEmpty(keyData))
                throw new Exception("unable to get key");
            return keyData;
        }
    }
}