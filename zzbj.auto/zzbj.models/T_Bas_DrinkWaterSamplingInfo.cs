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
    
    public partial class T_Bas_DrinkWaterSamplingInfo
    {
        public int DrinkWaterSamplingCode { get; set; }
        public string DSampleCode { get; set; }
        public string DrinkWaterSamplingName { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> IsStatus { get; set; }
    }
}
