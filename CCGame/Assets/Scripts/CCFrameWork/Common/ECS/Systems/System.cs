using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.Pool;
using UnityEditor;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace CCFrameWork.Common.ECS
{

    public interface ITask
    {
    }

    public interface ISystemTask
    {
        public bool RegisterTask(ITask task);

        public bool DisRegisterTask(ITask task);
    }

    public interface ISystem : IPoolObject
    {
        public  void Execute();

        public T AddSubSystem<T>() where T : ISystem, new();

        public bool RemoveSubSystem<T>() where T : ISystem;

        public T GetSubSystem<T>() where T : ISystem;

    }

    public class System : ISystem
    {
        // 一个系统的子系统应该不会很多 如果很多那就是设计有问题
        public List<ISystem> _sub_system_list = new List<ISystem>(10);
        public bool is_in_pool { get; set; }
        
        public virtual void Execute()
        {
            for (int idx = 0; idx < _sub_system_list.Count; idx++) 
            {
                _sub_system_list[idx].Execute();
            }
            OnExecute();
        }

        public T GetSubSystem<T>() where T : ISystem
        {
            foreach (var sub_system in _sub_system_list)
            {
                if (sub_system is T)
                {
                    return (T)sub_system;
                }
            }

            return default(T);
        }

        public T AddSubSystem<T>() where T : ISystem, new()
        {
            ISystem system = Game.pool.Create<T>();
            _sub_system_list.Add(system);
            return (T)system;
        }

        public bool RemoveSubSystem<T>() where T : ISystem
        {
            foreach (var sub_system in _sub_system_list)
            {
                if (sub_system is T)
                {
                    Game.pool.Recycle(sub_system);
                    _sub_system_list.Remove(sub_system);
                    return true;
                }
            }

            return false;
        }


        public void OnFetch()
        {
            OnInit();
        }

        public void OnRecycle()
        {
            foreach (var sub_system in _sub_system_list)
            {
                Game.pool.Recycle(sub_system);
            }
            _sub_system_list.Clear();
        }

        public virtual void OnInit() { }

        public virtual void OnExecute() { }
    }

    #region Start
    
   
    #endregion

    #region Update
    
   
    
    #endregion

    #region LateUpdate

    

    
   
    #endregion
}