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
    
    public partial class T_Bas_DeviceExamineNotes
    {
        public int ExamineNotesId { get; set; }
        public string ApplyServiceCode { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.Guid> DEPTID { get; set; }
        public string Content { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> ExamineTime { get; set; }
        public Nullable<System.DateTime> NextExamineTime { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> IsStatus { get; set; }
    }
}
