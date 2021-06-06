namespace CLFramework.ECS
{
    public class Component : IComponent
    {
        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            
        }

        public bool isInPool { get; set; }


        public Entity entity { get; set; }
    }
}