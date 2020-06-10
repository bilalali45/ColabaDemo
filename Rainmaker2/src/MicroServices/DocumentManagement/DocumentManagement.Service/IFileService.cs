using DocumentManagement.Model;
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

    }
}
