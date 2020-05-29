using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyStoreController : Controller
    {
        private readonly IKeyStore keyStore;
        public KeyStoreController(IKeyStore keyStore)
        {
            this.keyStore = keyStore;
        }
        [HttpGet]
        public IActionResult Get(string key)
        {
            return Ok(new { key = keyStore.Get(key) });
        }
    }
}