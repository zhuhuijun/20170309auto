//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace zzbj.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Bas_MaterialPurchase
    {
        public string PurchaseMaterialID { get; set; }
        public string MaterialID { get; set; }
        public string MaterialModelID { get; set; }
        public string AppllyPerson { get; set; }
        public string ApplyDept { get; set; }
        public string Application { get; set; }
        public string TechnologyRequire { get; set; }
        public string CheckRequire { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> ApplyDate { get; set; }
        public Nullable<System.DateTime> HopeDate { get; set; }
        public Nullable<System.DateTime> ReallyDate { get; set; }
        public Nullable<double> ApplyNumber { get; set; }
        public Nullable<double> ReallyNumber { get; set; }
        public string Unit { get; set; }
        public string ApplyFileName { get; set; }
        public Nullable<System.Guid> ExamineUserId { get; set; }
    }
}
