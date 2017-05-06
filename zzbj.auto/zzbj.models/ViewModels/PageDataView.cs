using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.models
{
    /// <summary>
    /// 分页使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDataView<T>
    {
        public PageDataView()
        {
            this._Items = new List<T>();
        }
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        private IList<T> _Items;
        public IList<T> rows
        {
            get { return _Items; }
            set { _Items = value; }
        }

    }
}
