using System.Collections.Generic;

namespace CCFrameWork.Common.ECS
{
    public interface ITaskLateUpdate : ITask
    {
        public void LateUpdate();
    }
    
    public interface ISystemLateUpdate : ISystem,ISystemTask
    {
      
    }
    

    public class SystemLateUpdate : System, ISystemLateUpdate
    {
        public List<ITaskLateUpdate> _late_update_list = new List<ITaskLateUpdate>();
        
        public bool RegisterTask(ITask task)
        {
            ITaskLateUpdate taskLateUpdate = task as ITaskLateUpdate;
            if (_late_update_list.Contains(taskLateUpdate))
            {
                return false;
            }
            _late_update_list.Add(taskLateUpdate);
            return true;
        }

        public bool DisRegisterTask(ITask task)
        {
            ITaskLateUpdate taskLateUpdate = task as ITaskLateUpdate;
            if (_late_update_list.Contains(taskLateUpdate))
            {
                _late_update_list.Remove(taskLateUpdate);
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
}