﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 12/16/2023 4:46:26 PM
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
    public partial class VBilllist {

        public VBilllist()
        {
            OnCreated();
        }

        public virtual int IDAUTO { get; set; }

        public virtual string MemberID { get; set; }

        public virtual string ID { get; set; }

        public virtual string Dateonly { get; set; }

        public virtual string Name { get; set; }

        public virtual string MonthBill { get; set; }

        public virtual string YearBill { get; set; }

        public virtual string Phonenumber { get; set; }

        public virtual string Timeonly { get; set; }

        public virtual double? PromotionCost { get; set; }

        public virtual string Notification { get; set; }

        public virtual double? ExcessCost { get; set; }

        public virtual double? InterCallingCharge { get; set; }

        public virtual double? AdditionalServiceFee { get; set; }

        public virtual double? VAT { get; set; }

        public virtual string Promotion { get; set; }

        public virtual string Camp { get; set; }

        public virtual string Month { get; set; }

        public virtual bool? DeleteStatus { get; set; }

        public virtual string MonthID { get; set; }

        public virtual string EmpID { get; set; }

        public virtual double? TotalService { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}