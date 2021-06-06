using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;

namespace CLFramework.ECS
{
    public class ComponentGroup : CLObject
    {
        public Dictionary<Type, List<IComponent>> _comp_group = new Dictionary<Type, List<IComponent>>();

        public void AddComponent(IComponent comp)
        {
            Type comp_type = comp.GetType();
            if (!_comp_group.TryGetValue(comp_type, out  List<IComponent> comp_list) )
            {
                comp_list = new List<IComponent>();
                _comp_group[comp_type] = comp_list;
            }
        }

        public void RemoveComponent(IComponent comp)
        {
            Type comp_type = comp.GetType();
            _comp_group[comp_type].Remove(comp);
        }

        public List<IComponent> GetComponents<T>() where  T : IComponent
        {
            Type comp_type = typeof(T);

            if (_comp_group.TryGetValue(comp_type, out List<IComponent> comp_list))
            {
                return comp_list;
            }

            return null;
        }
    }
}