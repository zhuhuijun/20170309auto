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
    
    public partial class V_DrinkingWaterSamplingInfo
    {
        public int DrinkWaterSamplingCode { get; set; }
        public string DSampleCode { get; set; }
        public string DrinkWaterSamplingName { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public string FactoryName { get; set; }
        public Nullable<int> DrinkingSourceID { get; set; }
        public string DrinkingSourceName { get; set; }
        public string WaterSystemName { get; set; }
        public Nullable<int> WaterSystemID { get; set; }
        public Nullable<int> DrinkingSourceProperty { get; set; }
        public string CheckTypeName { get; set; }
        public string DrinkingWaterTypeName { get; set; }
        public Nullable<int> DrinkingWaterTypeID { get; set; }
        public string ControlLevelName { get; set; }
        public string DMorCX { get; set; }
        public string LakeVerticaName { get; set; }
        public Nullable<int> WaterQualityID { get; set; }
        public string WaterQualityName { get; set; }
        public string StationName { get; set; }
        public string StaionCode { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public Nullable<int> ControlLevelID { get; set; }
        public string DMControllName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> IsStatus { get; set; }
    }
}