using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;

namespace CLFramework.ECS
{
    public class EntityGroup : CLObject
    {
        private  readonly Dictionary<int, List<Entity>> _entity_dict = new Dictionary<int, List<Entity>>();

        public void AddEntity(Entity entity)
        {
            int type = entity.type;
            if (!_entity_dict.TryGetValue(type, out  List<Entity> entity_list))
            {
                entity_list = new List<Entity>();
                _entity_dict[type] = entity_list;
            }
            entity_list.Add(entity);;
        }

        public void RemoveEntity( Entity entity)
        {
            int type = entity.type;
            if (_entity_dict.TryGetValue(type, out  List<Entity> entity_list))
            {
                entity_list.Remove(entity);
            }
        }

        public List<Entity> GetEntities(int type)
        {

            if (_entity_dict.TryGetValue(type, out  List<Entity> entity_list))
            {
                return entity_list;
            }

            return null;
        }
    }
}