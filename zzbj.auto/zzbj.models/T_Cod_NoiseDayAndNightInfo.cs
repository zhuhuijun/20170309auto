//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-05-19 18:20:46 by ding
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
namespace zzbj.models
{	
	public partial class T_Cod_NoiseDayAndNightInfo
    {
		
		/// <summary>
		/// 
		/// </summary>		
		public int DayTypeId { get; set; }
		
		/// <summary>
		/// 噪声类别
		/// </summary>		
		public int PK_NoiseType { get; set; }
		
		/// <summary>
		/// 昼夜开始时间
		/// </summary>		
		public int StartTime { get; set; }
		
		/// <summary>
		/// 昼夜结束时间
		/// </summary>		
		public int EndTime { get; set; }
		   
    }
}

