using System;
using System.Collections.Generic;


namespace CCFrameWork.Common.Pool
{
    public class Pool
    {
        public Type _create_type;
        
        public Queue<IPoolObject> _pool_object_queue = new Queue<IPoolObject>();

        public int _use_cnt = 0;

        public Pool(Type create_type)
        {
            _create_type = create_type;
        }

        public T Create<T>() where  T : IPoolObject
        {
            IPoolObject pool_object;
            if (_pool_object_queue.Count > 0)
            {
                pool_object =_pool_object_queue.Dequeue();
            }
            else
            {
             
                pool_object = (T)Activator.CreateInstance(_create_type);
            }

            _use_cnt++;
            pool_object.is_in_pool = false;
            pool_object.OnFetch();
            return (T) pool_object;
        }

        public void Recycle(IPoolObject pool_object)
        {
            if (pool_object.is_in_pool)
            {
                return;
            }
            pool_object.is_in_pool = true;
            pool_object.OnRecycle();
            _pool_object_queue.Enqueue(pool_object);
            _use_cnt--;
        }

    }
}