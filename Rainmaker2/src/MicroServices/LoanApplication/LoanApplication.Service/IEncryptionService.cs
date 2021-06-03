using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Service
{
    public interface IEncryptionService
    {
        (string,string,string) Encrypt(string input);
         string Decrypt(string input,string key,string iv);
    }
}
