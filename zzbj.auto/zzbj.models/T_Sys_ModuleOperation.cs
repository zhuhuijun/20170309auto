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
    
    public partial class T_Sys_ModuleOperation
    {
        public int ModuleOperationId { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public Nullable<System.Guid> OperationId { get; set; }
        public Nullable<int> IsStatus { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Remarks { get; set; }
    }
}
