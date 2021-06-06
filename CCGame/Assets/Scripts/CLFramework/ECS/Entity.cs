using System;
using System.Collections.Generic;
using CLFramework.Pool;
using CLFramework.Utils;
using  CLFramework.Game;
using CLFramework.Log;
using DefaultNamespace;
using GameObject = UnityEngine.GameObject;


namespace CLFramework.ECS
{
    public class Entity : CLObject, IPoolObject
    {
        private static IDCreator _id_creator = new IDCreator();

        
        public bool isInPool { get; set; }
        
        public int id;
        public int type;

        public IWorld world;
        
        public Dictionary<Type, IComponent> _component_dict = new Dictionary<Type, IComponent>();

        public Entity()
        {
            id = _id_creator.GetId();
        }

        public bool Vailed
        {
            get{ return  !isInPool && !isDestroy; }
            
        }

        public T AddComponent<T>() where  T : IComponent
        {
            IComponent component = GameIns.pool.Create<T>();
            component.entity = this;
            Type comp_type = typeof(T);
            CLAssert.Assert(_component_dict.ContainsKey(comp_type), "type is in");
            _component_dict[typeof(T)] = component;
            world.OnAddComponent(component);
            return (T) component;
        }

        public void RemoveComponent<T>() where  T : IComponent
        {
            Type comp_type = typeof(T);
            if (_component_dict.TryGetValue(comp_type, out IComponent comp))
            {
                world.OnRemoveComponent(comp);
                GameIns.pool.Recycle(comp);
                _component_dict.Remove(comp_type);
            }
        }

        public T GetComponent<T>()  where  T : IComponent
        {
            Type comp_type = typeof(T);
            if (_component_dict.TryGetValue(comp_type, out IComponent comp))
            {
                return (T) comp;
            }

            return default(T);
        }

        /// <summary>
        ///  清理全部组件
        /// </summary>
        public void ClearComponent()
        {
            foreach (var VARIABLE in _component_dict)
            {
                world.OnRemoveComponent(VARIABLE.Value);
                GameIns.pool.Recycle(VARIABLE.Value);
            }
            _component_dict.Clear();
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            
        }
    }
}