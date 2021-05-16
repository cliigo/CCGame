using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.Pool;
using CCFrameWork.Common.Utils;

namespace CCFrameWork.Common.ECS
{
    public interface IEntity : IPoolObject, ITaskSystem
    {
        public int id { get; set; }

        public bool is_vailed { get;}

        public T AddComponent<T>() where T : Component;

        public bool RemoveComponent(Component comp);
        public bool RemoveComponent<T>() where T : Component;

        public void ClearAllComponent();
        public T GetComponent<T>() where T : Component;

        

    }

    public class Entity : IEntity
    {
        private  static IDCreator _id_creator = new IDCreator();

        public int id { get; set; }

        public bool is_in_pool { get; set; }

        public bool is_vailed
        {
            get { return !is_in_pool; }
        }

        /// <summary>
        ///  任务 系统方便注册
        /// </summary>
        public ITaskSystem task_system { get; set; }


        public Dictionary<Type, Component> _component_dict = new Dictionary<Type, Component>();
        
        public Entity()
        {
            id = _id_creator.ID;
        }

        public T AddComponent<T>() where  T : Component
        {
            if (!is_vailed)
            {
                return default(T);
            }

            T comp = Game.pool.Create<T>();
            _component_dict[typeof(T)] = comp;
            comp.entity = this;
            RegisterTask(comp);
            return comp;
        }

        public T GetComponent<T>() where  T : Component
        {
            if (!is_vailed)
            {
                return default(T);
            }
            Type comp_type = typeof(T);
            if (_component_dict.TryGetValue(comp_type, out  Component comp))
            {
                return (T) comp;
            }
            return null;
        }

        public bool RemoveComponent(Component comp)
        {
            if (!is_vailed)
            {
                return false;
            }
            Type comp_type = comp.GetType();
            if (_component_dict.ContainsKey(comp_type))
            {
                Game.pool.Recycle(comp);
                _component_dict.Remove(comp_type);
                return true;
            }
            return false;
        }

        public bool RemoveComponent<T>() where  T : Component
        {
            if (!is_vailed)
            {
                return false;
            }
            Type comp_type = typeof(T);
            if (_component_dict.ContainsKey(comp_type))
            {
                Game.pool.Recycle(_component_dict[comp_type]);
                _component_dict.Remove(comp_type);
                return true;
            }
            return false;
        }

        public void ClearAllComponent()
        {
            if (!is_vailed)
            {
                return;
            }
            foreach (var pair in _component_dict)
            {
                Game.pool.Recycle(pair.Value);
            }
            _component_dict.Clear();
        }

        public bool RegisterTask(ISystemTask task)
        {
            if (is_vailed && task_system != null)
            {
                return task_system.RegisterTask(task);
            }

            return false;
        }

        public bool DisRegisterTask(ISystemTask task)
        {
            if (is_vailed && task_system != null)
            {
                return task_system.DisRegisterTask(task);
            }
            return false;
        }

        
       
        public virtual void OnFetch()
        {
           
        }

        public virtual void OnRecycle()
        {
            
            //  清理全部组件
            ClearAllComponent();
        }
    }
}