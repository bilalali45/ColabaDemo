﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 8/17/2020 9:13:55 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

namespace LosIntegration.Entity.Models
{
    public partial class ByteDocTypeMapping : URF.Core.EF.Trackable.Entity
    {

        public ByteDocTypeMapping()
        {
            OnCreated();
        }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual string RmDocTypeName
        {
            get;
            set;
        }

        public virtual string ByteDoctypeName
        {
            get;
            set;
        }

        public virtual int ByteDocCategoryId
        {
            get;
            set;
        }

        public virtual ByteDocCategoryMapping ByteDocCategoryMapping
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}