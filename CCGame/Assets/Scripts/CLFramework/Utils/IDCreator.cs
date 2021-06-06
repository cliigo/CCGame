namespace CLFramework.Utils
{
    public class IDCreator
    {
        private int _id = 0;

        public int GetId()
        {
            return ++_id;
        }

        public void Reset()
        {
            _id = 0;
        }
    }
}