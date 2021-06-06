using CLFramework.Pool;

namespace CLFramework.ECS
{
    public interface IComponent : IPoolObject
    {
        public Entity entity { get; set; }
        
    }
}