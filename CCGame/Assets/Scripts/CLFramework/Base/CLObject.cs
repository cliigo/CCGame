namespace DefaultNamespace
{
    public interface IDestroy
    {
        public void Destroy();
    }

    public class CLObject : IDestroy
    {
        public bool isDestroy = false;

        public void Destroy()
        {
            if(isDestroy) return;
            isDestroy = true;
            OnDestroy();
        }

        protected virtual void OnDestroy(){ }
    }
}