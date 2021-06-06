using  System.Collections.Generic;

using UnityEditor.Search;
using NotImplementedException = System.NotImplementedException;

namespace CLFramework.ECS
{

    public interface IStartTask
    {
        public void Start();
    }

    public class StartSystem : ISystem
    {
        
        private List<IStartTask> _task_list = new List<IStartTask>();
        
        public void Update()
        {
            throw new NotImplementedException();
        }

        public void AddTask(ISystemTask task)
        {
            _task_list.Add((IStartTask)task);
        }

        public void RemoveTask(ISystemTask task)
        {
            IStartTask start_task = task as IStartTask;
            int index = _task_list.IndexOf(start_task);
            if (index != -1)
            {
                _task_list.RemoveAt(index);
            }
        }
    }
}