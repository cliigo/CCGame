using System.Collections.Generic;
using UnityEngine;

namespace CCFrameWork.Common.ECS
{
    public interface ITaskUpdate : ITask
    {
        public void Update(float dt);
        
        
    }

    public interface ISystemUpdate : ISystem,ISystemTask
    {
    }

    public class SystemUpdate : System, ISystemUpdate
    {
        public List<ITaskUpdate> _update_list = new List<ITaskUpdate>();
        public List<ITaskUpdate> _remove_list = new List<ITaskUpdate>(10);

        public bool RegisterTask(ITask task)
        {
            ITaskUpdate taskUpdate = task as ITaskUpdate;;
            if (_update_list.Contains(taskUpdate))
            {
                return false;
            }
            _update_list.Add(taskUpdate);
            return true;
        }

        public bool DisRegisterTask(ITask task)
        {
            ITaskUpdate taskUpdate = task as ITaskUpdate;;
            if (!_remove_list.Contains(taskUpdate))
            {
                _remove_list.Add(taskUpdate);
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
}