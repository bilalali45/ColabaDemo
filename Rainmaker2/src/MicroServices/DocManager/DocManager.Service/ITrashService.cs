﻿using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface ITrashService
    {
        Task<List<WorkbenchFile>> GetTrashFiles(int loanApplicationId, int tenantId);
        Task<bool> MoveFromTrashToWorkBench(MoveFromTrashToWorkBench moveFromTrashToWorkBench, int tenantId);
        Task<bool> SaveTrashAnnotations(SaveTrashAnnotations  saveTrashAnnotations, int tenantId);
        
        
    }
}
