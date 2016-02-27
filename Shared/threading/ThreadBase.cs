namespace Shared.threading
{
    public class ThreadBase
    {
        protected volatile bool _shouldStop;

        public virtual void DoWork() { }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}