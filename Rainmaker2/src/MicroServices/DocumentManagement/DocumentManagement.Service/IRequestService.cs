﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;

namespace DocumentManagement.Service
{
    public interface IRequestService
    {
        Task<bool> SaveDraft(LoanApplication loanApplication);
    }
}
