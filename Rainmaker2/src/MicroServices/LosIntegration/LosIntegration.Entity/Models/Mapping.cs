﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 8/17/2020 9:13:55 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;

namespace LosIntegration.Entity.Models
{
    public partial class Mapping : URF.Core.EF.Trackable.Entity {

        public Mapping()
        {
            OnCreated();
        }

        public virtual long Id
        {
            get;
            set;
        }

        public virtual string RMEntityName
        {
            get;
            set;
        }

        public virtual string RMEnittyId
        {
            get;
            set;
        }

        public virtual string ExtOriginatorEntityName
        {
            get;
            set;
        }

        public virtual string ExtOriginatorEntityId
        {
            get;
            set;
        }

        public virtual short ExtOriginatorId
        {
            get;
            set;
        }

        public virtual ExternalOriginator ExternalOriginator
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
