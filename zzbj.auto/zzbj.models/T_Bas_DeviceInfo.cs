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
    
    public partial class T_Bas_DeviceInfo
    {
        public string deviceID { get; set; }
        public string deviceNum { get; set; }
        public string deviceName { get; set; }
        public string specType { get; set; }
        public string leaveFactoryNum { get; set; }
        public string measureArea { get; set; }
        public string nicety { get; set; }
        public Nullable<System.DateTime> buyDate { get; set; }
        public string manufacturer { get; set; }
        public string temperatureAsk { get; set; }
        public string humidityAsk { get; set; }
        public Nullable<int> DeviceStatus { get; set; }
        public Nullable<int> isTab { get; set; }
        public string deviceDoc { get; set; }
        public string deviceDocType { get; set; }
        public string depositPlace { get; set; }
        public string calibrateUnit { get; set; }
        public Nullable<System.DateTime> checkDate { get; set; }
        public Nullable<System.DateTime> disuseDate { get; set; }
        public string deviceTypeID { get; set; }
        public string DEPTID { get; set; }
        public Nullable<decimal> CalibrateCycle { get; set; }
        public string Custodian { get; set; }
        public string CalibrateCycleName { get; set; }
        public Nullable<decimal> CalibrateRemindDays { get; set; }
        public Nullable<int> MaintainCycle { get; set; }
        public string MaintainCycleName { get; set; }
        public Nullable<decimal> MaintainRemindDays { get; set; }
        public string remark { get; set; }
        public string datafilepath { get; set; }
        public string datafilesavepath { get; set; }
        public string savedfilepath { get; set; }
        public string capturemethodid { get; set; }
        public Nullable<int> IsMaintain { get; set; }
        public Nullable<decimal> BuyPrice { get; set; }
        public Nullable<int> LinkID { get; set; }
        public Nullable<int> MonitorPK_ID { get; set; }
    }
}
