namespace CCFrameWork.Common.Utils
{
    public class IDCreator
    {
        public int id;

        public  int ID 
        {
            get
            {
                return ++id;
            }
        }

        public void Reset()
        {
            id = 0;
        }
    }
}