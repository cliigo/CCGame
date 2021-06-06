using System;
using System.Collections.Generic;
using System.Reflection;
using CLFramework.Log;
using DefaultNamespace;


namespace CLFramework.Pool
{

    public interface IPoolObject
    {
        public void OnFetch();

        public void OnRecycle();

        public bool isInPool { get; set; }
    }

    /// <summary>
    ///  缓存池 千万别放值类型
    /// </summary>
    public class CLPool : CLObject
    {
        private readonly Queue<IPoolObject> _pool_object_queue = new Queue<IPoolObject>();

        private Type _create_type;

        public CLPool(Type create_type)
        {
            _create_type = create_type;
        }

        public T Create<T>()
        {
            T create_obj;
            if (_pool_object_queue.Count > 0)
            {
                create_obj =  (T) _pool_object_queue.Dequeue();
            }
            else
            {
                create_obj = Activator.CreateInstance<T>();
            }
            IPoolObject pool_obj = create_obj as IPoolObject;
            pool_obj.isInPool = false;
            pool_obj.OnFetch();
            return create_obj;
        }

        public void Recycle(IPoolObject pool_object)
        {
            if (!pool_object.isInPool)
            {
                pool_object.isInPool = true;
                pool_object.OnRecycle();
                _pool_object_queue.Enqueue(pool_object);
            }
        }
    }

    /// <summary>
    ///  缓存池管理器
    /// </summary>
    public class CLPoolMgr : CLObject
    {
        
        private readonly Dictionary<Type, CLPool> _pool_dict = new Dictionary<Type, CLPool>();

        public T Create<T>()
        {
            Type type = typeof(T);
            if (!_pool_dict.TryGetValue(type, out CLPool pool))
            {
                pool = new CLPool(type);
                _pool_dict[type] = pool;
            }
            return pool.Create<T>();
        }

        public void Recycle(IPoolObject pool_object)
        {
            Type type = pool_object.GetType();
            if (_pool_dict.TryGetValue(type, out CLPool pool))
            {
                pool.Recycle(pool_object);
            }
            else
            {
                CLAssert.Assert(true, "object is not in poll" + type);
            }
        }
    }
}