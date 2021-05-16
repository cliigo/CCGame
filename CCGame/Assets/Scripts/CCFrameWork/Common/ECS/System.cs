using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.Pool;
using UnityEditor;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace CCFrameWork.Common.ECS
{

    public interface ISystemTask
    {
    }

    public interface ITaskSystem
    {
        public bool RegisterTask(ISystemTask task);

        public bool DisRegisterTask(ISystemTask task);
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
    
    public interface IStartTask : ISystemTask
    {
        public void Start();
    }

    public interface IStartSystem : ISystem,ITaskSystem
    {
       
    }

    public class StartSystem : System, IStartSystem
    {
        public List<IStartTask> _start_list = new List<IStartTask>(10);
        

        public bool RegisterTask(ISystemTask task)
        {
            IStartTask start_task = task as IStartTask;
            if (_start_list.Contains(start_task))
            {
                return false;
            }
            _start_list.Add(start_task);
            return true;
        }

        public bool DisRegisterTask(ISystemTask task)
        {
            IStartTask start_task = task as IStartTask;
            if (_start_list.Contains(start_task))
            {
                _start_list.Remove(start_task);
                return true;
            }

            return false;
        }


        public override void OnExecute()
        {
            foreach (var start_obj in _start_list)
            {
                start_obj.Start();
            }
            _start_list.Clear();
        }
    }
    #endregion

    #region Update
    
    public interface IUpdateTask : ISystemTask
    {
        public void Update(float dt);
        
        
    }

    public interface IUpdateSystem : ISystem,ITaskSystem
    {
    }

    public class UpdateSystem : System, IUpdateSystem
    {
        public List<IUpdateTask> _update_list = new List<IUpdateTask>();
        public List<IUpdateTask> _remove_list = new List<IUpdateTask>(10);

        public bool RegisterTask(ISystemTask task)
        {
            IUpdateTask update_task = task as IUpdateTask;;
            if (_update_list.Contains(update_task))
            {
                return false;
            }
            _update_list.Add(update_task);
            return true;
        }

        public bool DisRegisterTask(ISystemTask task)
        {
            IUpdateTask update_task = task as IUpdateTask;;
            if (!_remove_list.Contains(update_task))
            {
                _remove_list.Add(update_task);
                return true;
            }
            return false;
        }
        
        public override void OnExecute()
        {
            // 删除
            foreach (var remove_update_obj in _remove_list)
            {
                _update_list.Remove(remove_update_obj);
            }
            // 循环便利 防止循环中删除对象
            foreach (var update_obj in _update_list)
            {
                update_obj.Update(Time.deltaTime);
            }
        }
    }
    
    #endregion

    #region LateUpdate

    

    
    public interface ILateUpdateTask : ISystemTask
    {
        public void LateUpdate();
    }
    
    public interface ILateUpdateSystem : ISystem,ITaskSystem
    {
      
    }
    

    public class LateUpdateSystem : System, ILateUpdateSystem
    {
        public List<ILateUpdateTask> _late_update_list = new List<ILateUpdateTask>();
        
        public bool RegisterTask(ISystemTask task)
        {
            ILateUpdateTask late_update_task = task as ILateUpdateTask;
            if (_late_update_list.Contains(late_update_task))
            {
                return false;
            }
            _late_update_list.Add(late_update_task);
            return true;
        }

        public bool DisRegisterTask(ISystemTask task)
        {
            ILateUpdateTask late_update_task = task as ILateUpdateTask;
            if (_late_update_list.Contains(late_update_task))
            {
                _late_update_list.Remove(late_update_task);
                return true;
            }
            return false;
        }

        public override void OnExecute()
        {
            foreach (var late_update in _late_update_list)
            {
                late_update.LateUpdate();
            }
        }
    }
    #endregion
}