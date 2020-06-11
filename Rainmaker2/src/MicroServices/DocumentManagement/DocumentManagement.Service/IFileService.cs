using DocumentManagement.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IFileService
    {
     
        Task<bool> Done(DoneModel model);
        Task<bool> Rename(FileRenameModel model);

        Task Order(FileOrderModel model);

        Task<bool> Submit(string id,string requestId,string docId , string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm);

    }
}
