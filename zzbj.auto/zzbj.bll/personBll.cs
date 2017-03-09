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
    public class personBll : IpersonBll
    {
        private readonly IRepository<person> _repository;
        public personBll(IRepository<person> repository)
        {
            _repository = repository;
        }

        public void Insert(person p)
        {
            _repository.Insert(p);
        }

        public void Update(person p)
        {
            _repository.Update(p);
        }

        public void Delete(person p)
        {
            _repository.Delete(p);
        }
    }
}
