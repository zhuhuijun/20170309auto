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
    
    public partial class T_Bas_CalibrateParameter
    {
        public string CalibrateParamID { get; set; }
        public string ParameterName { get; set; }
        public string Unit { get; set; }
        public string CONVENTIONID { get; set; }
        public Nullable<decimal> SignificantDigit { get; set; }
        public Nullable<decimal> DecimalDigit { get; set; }
        public string Expression { get; set; }
        public string IsX { get; set; }
        public string IsY { get; set; }
        public Nullable<decimal> OrderID { get; set; }
    }
}
