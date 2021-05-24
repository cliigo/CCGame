using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;
using UnityEditor;

namespace CCFrameWork.Common.Scene
{
    public class Scene : ISystemTask
    {

        public ISystemStart system_start;

        public ISystemUpdate system_update;

        public ISystemLateUpdate system_late_update;

        public EntityGroup _entity_group = new EntityGroup();
        
        public Scene InitSystem(
            ISystemStart system_start = null,
            ISystemUpdate system_update = null,
            ISystemLateUpdate system_late_update = null
        )
        {
            if(system_start == null) system_start = new SystemStart();
            if(system_update == null) system_update = new SystemUpdate();
            if(system_late_update == null) system_late_update = new SystemLateUpdate();
            this.system_start = system_start;
            this.system_update = system_update;
            this.system_late_update = system_late_update;
            return this;
        }

        /// <summary>
        ///  从场景中创建一个实例
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            return CreateEntity(EntityGroup.DEFAULT_GROUP);
        }

        public Entity CreateEntity(int entity_group)
        {
            Entity entity = Game.pool.Create<Entity>();
            entity.task_system = this;
            _entity_group.AddEntity(entity_group,entity);
            return entity;
        }

        /// <summary>
        ///  注册一个系统任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool RegisterTask(ITask task)
        {
            if (task is ITaskStart)
            {
                if (!system_start.RegisterTask((ITaskStart) task)) return false;
            }

            if (task is ITaskUpdate)
            {
                if (!system_update.RegisterTask((ITaskUpdate) task)) return false;
            }
            
            if (task is ITaskLateUpdate)
            {
                if (!system_late_update.RegisterTask((ITaskLateUpdate) task)) return false;
            }

            return true;
        }

        public bool DisRegisterTask(ITask task)
        {
            if (task is ITaskStart)
            {
                if(!system_start.DisRegisterTask((ITaskStart)task)) return  false;
            }

            if (task is ITaskUpdate)
            {
                if(!system_update.DisRegisterTask((ITaskUpdate)task)) return  false;
            }

            if (task is ITaskLateUpdate)
            {
                if(!system_late_update.DisRegisterTask((ITaskLateUpdate) task)) return  false;
            }

            return true;
        }

        public void Update()
        {
            system_start.Execute();
            system_update.Execute();
        }

        public void LateUpdate()
        {
            system_late_update.Execute();
        }
    }
    
}