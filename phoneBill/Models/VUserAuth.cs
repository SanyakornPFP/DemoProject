﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 12/22/2023 9:22:09 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace phoneBill.Models
{
    public partial class VUserAuth {

        public VUserAuth()
        {
            OnCreated();
        }

        public virtual int ID { get; set; }

        public virtual string EMPID { get; set; }

        public virtual string FIRSTNAME { get; set; }

        public virtual string SURNAME { get; set; }

        public virtual string IDCARD { get; set; }

        public virtual string Fullname { get; set; }

        public virtual string Level { get; set; }

        public virtual string POSITION { get; set; }

        public virtual string DEPNAME { get; set; }

        public virtual string SecName { get; set; }

        public virtual string Expr1 { get; set; }

        public virtual DateTime? ENDDATE { get; set; }

        public virtual string DEPID { get; set; }

        public virtual string NAMEENG { get; set; }

        public virtual string SURNAMEENG { get; set; }

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string ImgProfile { get; set; }

        public virtual bool? DeleteStatus { get; set; }

        public virtual string DateLogin { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
