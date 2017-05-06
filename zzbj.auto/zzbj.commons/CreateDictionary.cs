using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace zzbj.commons
{
    public class DataSearchModel
    {
        /// <summary>
        /// 查询数据库字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 查询数据库值
        /// </summary>
        public string Value { get; set; }
    }
    public class CreateDictionary
    {
        /// <summary>
        /// 根据查询条件json数据字符串返回查询条件字典数据
        /// </summary>
        /// <param name="searchData"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string searchData)
        {
            try
            {
                List<DataSearchModel> dataSearchList = JsonConvert.DeserializeObject<List<DataSearchModel>>(searchData);
                if (dataSearchList.Count > 0)
                {
                    Dictionary<string, string> dicResult = new Dictionary<string, string>();
                    foreach (var data in dataSearchList)
                    {
                        dicResult.Add(data.Name, data.Value);
                    }
                    return dicResult;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
