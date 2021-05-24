using System.Collections.Generic;
using CCFrameWork.Common.ECS;

namespace CCFrameWork.Common.Scene
{
    public class EntityGroup
    {
        public  static int DEFAULT_GROUP = 0;
        
        public  Dictionary<int, HashSet<IEntity>> _entity_dict = new Dictionary<int, HashSet<IEntity>>();

        public void AddEntity(IEntity entity)
        {
            AddEntity(entity.group, entity);
        }

        public void AddEntity(int entity_group, IEntity entity)
        {
            HashSet<IEntity> entity_set;
            entity.group = entity_group;
            if (!_entity_dict.TryGetValue(entity_group, out entity_set))
            {
                entity_set = new HashSet<IEntity>();
                _entity_dict[entity_group] = entity_set;
            }
            entity_set.Add(entity);
        }

        public bool RemoveEntity(IEntity entity)
        {
            return RemoveEntity(entity.group, entity);
        }

        public bool RemoveEntity(int entity_group, IEntity entity)
        {
            if (_entity_dict.TryGetValue(entity_group, out HashSet<IEntity> entity_set))
            {
                entity_set.Remove(entity);
            }
        }


    }
}