using System.Collections.Generic;
using CLFramework.Log;
using Unity.VisualScripting;
using NotImplementedException = System.NotImplementedException;

namespace CLFramework.ECS
{
    /// <summary>
    ///  一个系统的任务
    /// </summary>
    public interface ISystemTask
    {
        
    };
    
    public interface ISystem
    {
        
        public void Update();

        public void AddTask(ISystemTask task);

        public void RemoveTask(ISystemTask task);

    }

    public class System : ISystem
    {

        public bool enable = true;
        protected List<ISystem> _system_list = new List<ISystem>();

        public void AddTask(ISystemTask task)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(ISystemTask task)
        {
            throw new NotImplementedException();
        }

        public void AddSubSystem(ISystem sub_system)
        {

            if (!_system_list.Contains(sub_system))
            {
                _system_list.Add(sub_system);
            }
        }

        public void RemoveSubSystem(ISystem sub_system)
        {
            if (_system_list.Contains(sub_system))
            {
                _system_list.Remove(sub_system);
            }
        }

        public void Update()
        {
            if (!enable) return;
            Execute();
            foreach (var sub_system in _system_list)
            {
                sub_system.Update();
            }
        }

        protected virtual void Execute()
        {
            
        }
    }
}