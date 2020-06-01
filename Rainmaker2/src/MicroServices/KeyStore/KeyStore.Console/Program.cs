using KeyStore.Service;
using System;

namespace KeyStore.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Enter 1 to encrypt, 2 to decrypt, any other key to quit");
                System.Console.WriteLine();
                string value = System.Console.ReadLine();
                string key;
                switch (value)
                {
                    case "1":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Encrypt(key));
                        break;
                    case "2":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Decrypt(key));
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
