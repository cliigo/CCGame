using System;
using System.Collections.Generic;

namespace CCFrameWork.Common.Pool
{
    public class PoolMgr
    {
        public Dictionary<Type, Pool> _pool_dict = new Dictionary<Type, Pool>();

        public T Create<T>() where  T : IPoolObject
        {
            Type object_type = typeof(T);
            if (!_pool_dict.TryGetValue(object_type, out Pool pool))
            {
                pool = new Pool(object_type);
                _pool_dict[object_type] = pool;
            }
            return pool.Create<T>();
        }

        public void Recycle(IPoolObject pool_objct)
        {
            Type object_type = pool_objct.GetType();
            if (!_pool_dict.TryGetValue(object_type, out Pool pool))
            {
                pool =  new Pool(object_type);
                _pool_dict[object_type] = pool;
            }
            pool.Recycle(pool_objct);
        }
    }
}