using System.Collections.Generic;

namespace CCFrameWork.Common.ECS
{
    public interface ITaskStart : ITask
    {
        public void Start();
    }

    public interface ISystemStart : ISystem,ISystemTask
    {
       
    }

    public class SystemStart : System, ISystemStart
    {
        public List<ITaskStart> _start_list = new List<ITaskStart>(10);
        

        public bool RegisterTask(ITask task)
        {
            ITaskStart taskStart = task as ITaskStart;
            if (_start_list.Contains(taskStart))
            {
                return false;
            }
            _start_list.Add(taskStart);
            return true;
        }

        public bool DisRegisterTask(ITask task)
        {
            ITaskStart taskStart = task as ITaskStart;
            if (_start_list.Contains(taskStart))
            {
                _start_list.Remove(taskStart);
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
}