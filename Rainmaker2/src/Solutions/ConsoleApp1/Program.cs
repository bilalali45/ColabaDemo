using System;
using System.ComponentModel;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using URF.Core.EF;

namespace ConsoleApp1
{
    class Program
    {
       


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
        }
    }



    class Test
    {
        private readonly IByteDocCategoryMappingService _byteDocTypeMappingService;


        public Test()
        {
            var dd = LosIntegration.Data.Context > (options => "")
            var uow = new UnitOfWork<LosIntegration.Data.Context>();
            _byteDocTypeMappingService = new ByteDocCategoryMappingService();
        }


        public void test()
        {

            _byteDocTypeMappingService.GetByteDocTypeMappingWithDetails(docType: documentType?.DocType).SingleOrDefault();
    }


    }

}
