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
    
    public partial class T_Bas_DrinkingWaterFactoryInfo
    {
        public int FactoryID { get; set; }
        public Nullable<int> DrinkingSourceID { get; set; }
        public string FactoryName { get; set; }
        public string FactoryLocation { get; set; }
        public string ServiceCity { get; set; }
        public Nullable<double> DesignQSL { get; set; }
        public Nullable<double> ActualQSL { get; set; }
        public Nullable<double> UsePersonsNum { get; set; }
        public Nullable<int> IsStatue { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Remarks { get; set; }
    }
}
