using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;
using UnityEditor;

namespace CCFrameWork.Common.Scene
{
    public class Scene : ITaskSystem
    {

        public IStartSystem _start_system;

        public IUpdateSystem _update_system;

        public ILateUpdateSystem _late_update_system;
        
        public Scene InitSystem(
            IStartSystem start_system = null,
            IUpdateSystem update_system = null,
            ILateUpdateSystem late_update_system = null
        )
        {
            if(start_system == null) start_system = new StartSystem();
            if(update_system == null) update_system = new UpdateSystem();
            if(late_update_system == null) late_update_system = new LateUpdateSystem();
            _start_system = start_system;
            _update_system = update_system;
            _late_update_system = late_update_system;
            return this;
        }

        /// <summary>
        ///  从场景中创建一个实例
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            Entity entity = Game.pool.Create<Entity>();
            entity.task_system = this;

            return entity;
        }

        /// <summary>
        ///  注册一个系统任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool RegisterTask(ISystemTask task)
        {
            if (task is IStartTask)
            {
                if (!_start_system.RegisterTask((IStartTask) task)) return false;
            }

            if (task is IUpdateTask)
            {
                if (!_update_system.RegisterTask((IUpdateTask) task)) return false;
            }
            
            if (task is ILateUpdateTask)
            {
                if (!_late_update_system.RegisterTask((ILateUpdateTask) task)) return false;
            }

            return true;
        }

        public bool DisRegisterTask(ISystemTask task)
        {
            if (task is IStartTask)
            {
                if(!_start_system.DisRegisterTask((IStartTask)task)) return  false;
            }

            if (task is IUpdateTask)
            {
                if(!_update_system.DisRegisterTask((IUpdateTask)task)) return  false;
            }

            if (task is ILateUpdateTask)
            {
                if(!_late_update_system.DisRegisterTask((ILateUpdateTask) task)) return  false;
            }

            return true;
        }

        public void Update()
        {
            _start_system.Execute();
            _update_system.Execute();
        }

        public void LateUpdate()
        {
            _late_update_system.Execute();
        }
    }
    
}