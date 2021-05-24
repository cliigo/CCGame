using CCFrameWork.Common.Pool;
using NotImplementedException = System.NotImplementedException;

namespace CCFrameWork.Common.ECS
{
    public class Component : IPoolObject, ITaskStart
    {
        public IEntity entity { get; set; }
        
        public bool is_in_pool { get; set; }
        
        public bool is_vailed
        {
            get { return !is_in_pool; }
        }
        

        public virtual void Awake()
        {
            
        }

        public virtual void Start()
        {
            
        }

        public virtual void OnFetch()
        {
            Awake();
        }

        public virtual void OnRecycle()
        {
            entity = null;
        }

        public void Remove()
        {
            if (is_vailed && entity != null)
            {
                entity.RemoveComponent(this);
            }
        }

        public T AddComponent<T>() where T : Component
        {
            if (is_vailed && entity != null)
            {
                return entity.AddComponent<T>();
            }

            return default(T);
        }

        public bool RemoveComponent(Component comp)
        {
            if (is_vailed && entity != null)
            {
                return entity.RemoveComponent(comp);
            }
            return false;
        }

        public bool RemoveComponent<T>() where T : Component
        {
            if (is_vailed && entity != null)
            {
                return entity.RemoveComponent<T>();
            }
            return false;
        }

        public void ClearAllComponent()
        {
            if (is_vailed && entity != null)
            {
                entity.ClearAllComponent();
            }
        }

        public T GetComponent<T>() where T : Component
        {
            if (is_vailed && entity != null)
            {
                return entity.GetComponent<T>();
            }

            return default(T);
        }



    }
}