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
    
    public partial class V_GasMonthData
    {
        public int ID { get; set; }
        public string GasPointCode { get; set; }
        public string GasItemCode { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<decimal> MaxValue { get; set; }
        public Nullable<decimal> AvgValue { get; set; }
        public Nullable<decimal> MinValue { get; set; }
        public Nullable<decimal> UpdateValue { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> IsStatus { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string GasPointName { get; set; }
        public string GasItemName { get; set; }
    }
}