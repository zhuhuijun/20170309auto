﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dapper_testEntities : DbContext
    {
        public dapper_testEntities()
            : base("name=dapper_testEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_Bas_ApplyService> T_Bas_ApplyService { get; set; }
        public virtual DbSet<T_Bas_CalibrateParameter> T_Bas_CalibrateParameter { get; set; }
        public virtual DbSet<T_Bas_City> T_Bas_City { get; set; }
        public virtual DbSet<T_Bas_DEPARTMENTINFO> T_Bas_DEPARTMENTINFO { get; set; }
        public virtual DbSet<T_Bas_Device> T_Bas_Device { get; set; }
        public virtual DbSet<T_Bas_DeviceCalibrateCurve> T_Bas_DeviceCalibrateCurve { get; set; }
        public virtual DbSet<T_Bas_DeviceCalibratePoint> T_Bas_DeviceCalibratePoint { get; set; }
        public virtual DbSet<T_Bas_DeviceExamine> T_Bas_DeviceExamine { get; set; }
        public virtual DbSet<T_Bas_DeviceExamineNotes> T_Bas_DeviceExamineNotes { get; set; }
        public virtual DbSet<T_Bas_DeviceInfo> T_Bas_DeviceInfo { get; set; }
        public virtual DbSet<T_Bas_DringkingWaterSourceInfo> T_Bas_DringkingWaterSourceInfo { get; set; }
        public virtual DbSet<T_Bas_DrinkingWaterFactoryInfo> T_Bas_DrinkingWaterFactoryInfo { get; set; }
        public virtual DbSet<T_Bas_DrinkWaterSamplingInfo> T_Bas_DrinkWaterSamplingInfo { get; set; }
        public virtual DbSet<T_Bas_Examine> T_Bas_Examine { get; set; }
        public virtual DbSet<T_Bas_FuncitonNoisePointInfo> T_Bas_FuncitonNoisePointInfo { get; set; }
        public virtual DbSet<T_Bas_GasItem> T_Bas_GasItem { get; set; }
        public virtual DbSet<T_Bas_GasPointInfo> T_Bas_GasPointInfo { get; set; }
        public virtual DbSet<T_Bas_GridInfo> T_Bas_GridInfo { get; set; }
        public virtual DbSet<T_Bas_LabMonitorItem> T_Bas_LabMonitorItem { get; set; }
        public virtual DbSet<T_Bas_LakeInfo> T_Bas_LakeInfo { get; set; }
        public virtual DbSet<T_Bas_LakeVerticaPointlInfo> T_Bas_LakeVerticaPointlInfo { get; set; }
        public virtual DbSet<T_Bas_Link> T_Bas_Link { get; set; }
        public virtual DbSet<T_Bas_MaintainRecord> T_Bas_MaintainRecord { get; set; }
        public virtual DbSet<T_Bas_MaterialInventory> T_Bas_MaterialInventory { get; set; }
        public virtual DbSet<T_Bas_MaterialPurchase> T_Bas_MaterialPurchase { get; set; }
        public virtual DbSet<T_Bas_MaterialPurchaseRecord> T_Bas_MaterialPurchaseRecord { get; set; }
        public virtual DbSet<T_Bas_Materials> T_Bas_Materials { get; set; }
        public virtual DbSet<T_Bas_MaterialUseRecord> T_Bas_MaterialUseRecord { get; set; }
        public virtual DbSet<T_Bas_Module> T_Bas_Module { get; set; }
        public virtual DbSet<T_Bas_RainfallItem> T_Bas_RainfallItem { get; set; }
        public virtual DbSet<T_Bas_RainfallPointInfo> T_Bas_RainfallPointInfo { get; set; }
        public virtual DbSet<T_Bas_RiversInfo> T_Bas_RiversInfo { get; set; }
        public virtual DbSet<T_Bas_Road> T_Bas_Road { get; set; }
        public virtual DbSet<T_Bas_SeaWaterItem> T_Bas_SeaWaterItem { get; set; }
        public virtual DbSet<T_Bas_SeaWaterPointInfo> T_Bas_SeaWaterPointInfo { get; set; }
        public virtual DbSet<T_Bas_SectionPointInfo> T_Bas_SectionPointInfo { get; set; }
        public virtual DbSet<T_Bas_Station> T_Bas_Station { get; set; }
        public virtual DbSet<T_Bas_TrafficNoisePointInfo> T_Bas_TrafficNoisePointInfo { get; set; }
        public virtual DbSet<T_Bas_Warehouse> T_Bas_Warehouse { get; set; }
        public virtual DbSet<T_Bas_WaterItem> T_Bas_WaterItem { get; set; }
        public virtual DbSet<T_Bas_WaterSampling> T_Bas_WaterSampling { get; set; }
        public virtual DbSet<T_Cod_CalibrateRunType> T_Cod_CalibrateRunType { get; set; }
        public virtual DbSet<T_Cod_CONVENTIONRULEINFO> T_Cod_CONVENTIONRULEINFO { get; set; }
        public virtual DbSet<T_Cod_DeviceType> T_Cod_DeviceType { get; set; }
        public virtual DbSet<T_Cod_DrinkingWaterType> T_Cod_DrinkingWaterType { get; set; }
        public virtual DbSet<T_Cod_GasOriginStandard> T_Cod_GasOriginStandard { get; set; }
        public virtual DbSet<T_Cod_LakeOriginStandard> T_Cod_LakeOriginStandard { get; set; }
        public virtual DbSet<T_Cod_MaterialModel> T_Cod_MaterialModel { get; set; }
        public virtual DbSet<T_Cod_MaterialsType> T_Cod_MaterialsType { get; set; }
        public virtual DbSet<T_Cod_NoiseNetWokManageLevel> T_Cod_NoiseNetWokManageLevel { get; set; }
        public virtual DbSet<T_Cod_Region> T_Cod_Region { get; set; }
        public virtual DbSet<T_Cod_RiverOriginStandard> T_Cod_RiverOriginStandard { get; set; }
        public virtual DbSet<T_Cod_SeaWaterOriginStandard> T_Cod_SeaWaterOriginStandard { get; set; }
        public virtual DbSet<T_Cod_SoundSource> T_Cod_SoundSource { get; set; }
        public virtual DbSet<T_Cod_StandardToLimit> T_Cod_StandardToLimit { get; set; }
        public virtual DbSet<T_Code_CheckType> T_Code_CheckType { get; set; }
        public virtual DbSet<T_Code_ControlLevel> T_Code_ControlLevel { get; set; }
        public virtual DbSet<T_Code_Function> T_Code_Function { get; set; }
        public virtual DbSet<T_Code_GasItemType> T_Code_GasItemType { get; set; }
        public virtual DbSet<T_Code_GasItemTypeRelevance> T_Code_GasItemTypeRelevance { get; set; }
        public virtual DbSet<T_Code_GasStandardLimit> T_Code_GasStandardLimit { get; set; }
        public virtual DbSet<T_Code_GasTimeTypes> T_Code_GasTimeTypes { get; set; }
        public virtual DbSet<T_Code_GradeType> T_Code_GradeType { get; set; }
        public virtual DbSet<T_Code_LakeQualityStandard> T_Code_LakeQualityStandard { get; set; }
        public virtual DbSet<T_Code_LakeSampling> T_Code_LakeSampling { get; set; }
        public virtual DbSet<T_Code_LinkRelevance> T_Code_LinkRelevance { get; set; }
        public virtual DbSet<T_Code_NoiseFunction> T_Code_NoiseFunction { get; set; }
        public virtual DbSet<T_Code_NoiseSource> T_Code_NoiseSource { get; set; }
        public virtual DbSet<T_Code_PresentSituation> T_Code_PresentSituation { get; set; }
        public virtual DbSet<T_Code_QualityLevel> T_Code_QualityLevel { get; set; }
        public virtual DbSet<T_Code_RiverQualityStandard> T_Code_RiverQualityStandard { get; set; }
        public virtual DbSet<T_Code_SeaWaterFactorLimit> T_Code_SeaWaterFactorLimit { get; set; }
        public virtual DbSet<T_Code_SeaWaterFunction> T_Code_SeaWaterFunction { get; set; }
        public virtual DbSet<T_Code_WaterItemLimit> T_Code_WaterItemLimit { get; set; }
        public virtual DbSet<T_Code_WaterQuality> T_Code_WaterQuality { get; set; }
        public virtual DbSet<T_Code_WaterSourceType> T_Code_WaterSourceType { get; set; }
        public virtual DbSet<T_Code_WaterStandardLimit> T_Code_WaterStandardLimit { get; set; }
        public virtual DbSet<T_Code_WaterSystem> T_Code_WaterSystem { get; set; }
        public virtual DbSet<T_Sys_DEPARTMENTINFO> T_Sys_DEPARTMENTINFO { get; set; }
        public virtual DbSet<T_Sys_ModuleOperation> T_Sys_ModuleOperation { get; set; }
        public virtual DbSet<T_Sys_OperationId> T_Sys_OperationId { get; set; }
        public virtual DbSet<T_Sys_Role> T_Sys_Role { get; set; }
        public virtual DbSet<T_Sys_RoleMoudule> T_Sys_RoleMoudule { get; set; }
        public virtual DbSet<T_Sys_RoleOperation> T_Sys_RoleOperation { get; set; }
        public virtual DbSet<T_Sys_UserDept> T_Sys_UserDept { get; set; }
        public virtual DbSet<T_Sys_UserRole> T_Sys_UserRole { get; set; }
        public virtual DbSet<T_Cod_NoiseDayAndNightInfo> T_Cod_NoiseDayAndNightInfo { get; set; }
        public virtual DbSet<V_AQIDaySinglePoint> V_AQIDaySinglePoint { get; set; }
        public virtual DbSet<V_AQIReportData> V_AQIReportData { get; set; }
        public virtual DbSet<V_DrinkingWaterFactoryInfo> V_DrinkingWaterFactoryInfo { get; set; }
        public virtual DbSet<V_DrinkingWaterSamplingInfo> V_DrinkingWaterSamplingInfo { get; set; }
        public virtual DbSet<V_DrinkingWaterSourceInfo> V_DrinkingWaterSourceInfo { get; set; }
        public virtual DbSet<V_FunctionNoisePointLinkRegion> V_FunctionNoisePointLinkRegion { get; set; }
        public virtual DbSet<V_FunctionNoisePointRegion> V_FunctionNoisePointRegion { get; set; }
        public virtual DbSet<V_GasDayData> V_GasDayData { get; set; }
        public virtual DbSet<V_GasHourData> V_GasHourData { get; set; }
        public virtual DbSet<V_GasItemTypeAndRealTimeData> V_GasItemTypeAndRealTimeData { get; set; }
        public virtual DbSet<V_GasItemTypeRelevance> V_GasItemTypeRelevance { get; set; }
        public virtual DbSet<V_GasMonitorYearResult> V_GasMonitorYearResult { get; set; }
        public virtual DbSet<V_GasMonthData> V_GasMonthData { get; set; }
        public virtual DbSet<V_GasPointLinkStation> V_GasPointLinkStation { get; set; }
        public virtual DbSet<V_GasPointRealTimeData> V_GasPointRealTimeData { get; set; }
        public virtual DbSet<V_GasPointStationRegion> V_GasPointStationRegion { get; set; }
        public virtual DbSet<V_GridNoisePointRegion> V_GridNoisePointRegion { get; set; }
        public virtual DbSet<V_ItemRealData> V_ItemRealData { get; set; }
        public virtual DbSet<V_LabAblumMessage> V_LabAblumMessage { get; set; }
        public virtual DbSet<V_LabEquipmentApproval> V_LabEquipmentApproval { get; set; }
        public virtual DbSet<V_LabEquipmentConfirm> V_LabEquipmentConfirm { get; set; }
        public virtual DbSet<V_LabEquipmentCycles> V_LabEquipmentCycles { get; set; }
        public virtual DbSet<V_LabEquipmentExamineInfo> V_LabEquipmentExamineInfo { get; set; }
        public virtual DbSet<V_LabEquipmentInfo> V_LabEquipmentInfo { get; set; }
        public virtual DbSet<V_LabEquipmentProject> V_LabEquipmentProject { get; set; }
        public virtual DbSet<V_LabEquipmentRepairRequest> V_LabEquipmentRepairRequest { get; set; }
        public virtual DbSet<V_LabMaterialApproval> V_LabMaterialApproval { get; set; }
        public virtual DbSet<V_LabMaterialApprovalShow> V_LabMaterialApprovalShow { get; set; }
        public virtual DbSet<V_LabMaterialPurchase> V_LabMaterialPurchase { get; set; }
        public virtual DbSet<V_LabMaterialPurchaseRecord> V_LabMaterialPurchaseRecord { get; set; }
        public virtual DbSet<V_LabMaterialStock> V_LabMaterialStock { get; set; }
        public virtual DbSet<V_LabMaterialUseRecord> V_LabMaterialUseRecord { get; set; }
        public virtual DbSet<V_LabMonitorItemInfo> V_LabMonitorItemInfo { get; set; }
        public virtual DbSet<V_LakeSamplingPoint> V_LakeSamplingPoint { get; set; }
        public virtual DbSet<V_LakeVerticaPointlInfo> V_LakeVerticaPointlInfo { get; set; }
        public virtual DbSet<V_LakeVerticaPointRegion> V_LakeVerticaPointRegion { get; set; }
        public virtual DbSet<V_MaintainRecordInfo> V_MaintainRecordInfo { get; set; }
        public virtual DbSet<V_MaterialInfo> V_MaterialInfo { get; set; }
        public virtual DbSet<V_NoiseCityAndRoadInfo> V_NoiseCityAndRoadInfo { get; set; }
        public virtual DbSet<V_NoiseCityInfo> V_NoiseCityInfo { get; set; }
        public virtual DbSet<V_NoiseCityRangCountResult> V_NoiseCityRangCountResult { get; set; }
        public virtual DbSet<V_NoiseFunctionPointInfo> V_NoiseFunctionPointInfo { get; set; }
        public virtual DbSet<V_NoiseGridInfo> V_NoiseGridInfo { get; set; }
        public virtual DbSet<V_NoiseRoadInfo> V_NoiseRoadInfo { get; set; }
        public virtual DbSet<V_RainFallPointRegion> V_RainFallPointRegion { get; set; }
        public virtual DbSet<V_RainPointInfo> V_RainPointInfo { get; set; }
        public virtual DbSet<V_RiverSamplingInfo> V_RiverSamplingInfo { get; set; }
        public virtual DbSet<V_RiverSectionPointRegion> V_RiverSectionPointRegion { get; set; }
        public virtual DbSet<V_RiversInfo> V_RiversInfo { get; set; }
        public virtual DbSet<V_SeaWaterFunctionAreaInfo> V_SeaWaterFunctionAreaInfo { get; set; }
        public virtual DbSet<V_SeaWaterPointLinkStation> V_SeaWaterPointLinkStation { get; set; }
        public virtual DbSet<V_SelectAQIReportContains24HourData> V_SelectAQIReportContains24HourData { get; set; }
        public virtual DbSet<V_StationLinkRegion> V_StationLinkRegion { get; set; }
        public virtual DbSet<V_TrafficNoisePointInfo> V_TrafficNoisePointInfo { get; set; }
        public virtual DbSet<V_TrafficNoisePointRegion> V_TrafficNoisePointRegion { get; set; }
        public virtual DbSet<V_UserDeptRole> V_UserDeptRole { get; set; }
        public virtual DbSet<V_UserRightsView> V_UserRightsView { get; set; }
        public virtual DbSet<V_WaterSectionInfo> V_WaterSectionInfo { get; set; }
        public virtual DbSet<sys_action> sys_action { get; set; }
        public virtual DbSet<sys_role> sys_role { get; set; }
        public virtual DbSet<rel_menuactions> rel_menuactions { get; set; }
        public virtual DbSet<rel_rolemenus> rel_rolemenus { get; set; }
        public virtual DbSet<T_Sys_Users> T_Sys_Users { get; set; }
    }
}
