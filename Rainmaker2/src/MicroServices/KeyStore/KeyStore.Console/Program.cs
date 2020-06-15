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
                System.Console.WriteLine("Enter 1 to encrypt key, 2 to decrypt key, 3 to encrypt data, 4 to decrypt data, any other key to quit");
                System.Console.WriteLine();
                string value = System.Console.ReadLine();
                string key,data;
                switch (value)
                {
                    case "1":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Encrypt(key, AESCryptography.AesKey256));
                        break;
                    case "2":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Decrypt(key, AESCryptography.AesKey256));
                        break;
                    case "3":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine("Enter data");
                        data = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Encrypt(data, key));
                        break;
                    case "4":
                        System.Console.WriteLine("Enter key");
                        key = System.Console.ReadLine();
                        System.Console.WriteLine("Enter data");
                        data = System.Console.ReadLine();
                        System.Console.WriteLine(AESCryptography.Decrypt(data, key));
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
