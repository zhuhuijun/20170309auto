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
    
    public partial class T_Bas_MaintainRecord
    {
        public string PK_ID { get; set; }
        public string DeviceID { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<System.DateTime> MaintainDate { get; set; }
        public int IsMaintain { get; set; }
        public string Remarks { get; set; }
        public string MaintainPerson { get; set; }
    }
}
