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
    
    public partial class T_Bas_Module
    {
        public int MouduleID { get; set; }
        public Nullable<int> ApplicationID { get; set; }
        public string MouduleName { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> IsUse { get; set; }
        public Nullable<int> Path { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string IcoPath { get; set; }
        public string MenuUrl { get; set; }
        public Nullable<int> IsFuntion { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> MouduleType { get; set; }
    }
}