using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.ibll;
using zzbj.models;
using zzbj.repository;

namespace zzbj.bll
{
    public class customBll : IcustomBll
    {
        private readonly IRepository<custom> _repository;

        public customBll(IRepository<custom> repository)
        {
            _repository = repository;
        }

        public void Insert(custom c)
        {
            _repository.Insert(c);
        }

        public void Update(custom c)
        {
            _repository.Update(c);
        }

        public void Delete(custom c)
        {
            _repository.Delete(c);
        }
    }
}
