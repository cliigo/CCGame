using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCFrameWork.Common.Pool
{
    public interface IPoolObject
    {
        public void OnFetch();

        public void OnRecycle();

        public bool is_in_pool { get; set; }
    }

    
}
