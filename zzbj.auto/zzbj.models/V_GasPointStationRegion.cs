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
    
    public partial class V_GasPointStationRegion
    {
        public Nullable<System.Guid> Pk_id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string ParentNode { get; set; }
        public string StaionCode { get; set; }
        public string StationName { get; set; }
        public string GasPointCode { get; set; }
        public string GasPointName { get; set; }
        public Nullable<int> LinkID { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<int> QualityLevelID { get; set; }
        public Nullable<decimal> High { get; set; }
        public Nullable<double> PeopleNum { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> IsStatus { get; set; }
        public string LinkName { get; set; }
    }
}
