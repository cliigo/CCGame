using System.Collections.Generic;
using CLFramework.Game;
using DefaultNamespace;

namespace CLFramework.ECS
{

    public interface IWorld
    {
        public Entity CreateEntity(int entity_type = 0);
        public Entity GetEntity(int id);
        public void RemoveEntity(Entity entity);

        public List<IComponent> GetComponentList<T>() where T : IComponent;
        
        public void OnAddComponent(IComponent comp);
        public void OnRemoveComponent(IComponent comp);


        public System system { get; }

        public string name { get; set; }
    }

    public class World : CLObject, IWorld
    {
      
        private  readonly EntityGroup entity_group = new EntityGroup();
        private  readonly  ComponentGroup comp_group = new ComponentGroup();
        
        private readonly  Dictionary<int,Entity> _entity_dict = new Dictionary<int, Entity>();

        private  readonly  System _system = new System();
    
        
        /// <summary>
        ///  世界的名字
        /// </summary>
        public string name { get; set; }
        
        public System system
        {
            get { return _system; }
        }
        
        /// <summary>
        ///  创建一个实例到世界中
        /// </summary>
        /// <param name="entity_type"></param>
        /// <returns></returns>
        public Entity CreateEntity(int entity_type = 0)
        {
            Entity entity = GameIns.pool.Create<Entity>();
            entity.type = entity_type;
            entity_group.AddEntity(entity);
            _entity_dict[entity.id] = entity;
            entity.world = this;
            return entity;
        }

        /// <summary>
        ///  从世界中获得一个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity GetEntity(int id)
        {
            if (_entity_dict.TryGetValue(id, out  Entity entity))
            {
                return entity;
            }

            return null;
        }
        
        /// <summary>
        ///  将实例从世界中移除
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            if (_entity_dict.ContainsKey(entity.id))
            {
                entity_group.RemoveEntity(entity);
                _entity_dict.Remove(entity.id);
                // 清除全部组件
                entity.ClearComponent();
            }
        }

        public void OnAddComponent(IComponent comp)
        {
            comp_group.AddComponent(comp);
        }

        public List<IComponent> GetComponentList<T>() where T : IComponent
        {
            return comp_group.GetComponents<T>();
        }

        public void OnRemoveComponent(IComponent comp)
        {
            comp_group.RemoveComponent(comp);
        }


        public void Update()
        {
            _system.Update();
        }

    }
}