using System;
using Microsoft.SPOT;
using NetMf.CommonExtensions.Threading;

namespace Test.NetMf.CommonExtensions
{
    public class BasicLockExample
    {
        private object _locker = new MicroLock();

        public void Run()
        {
            while (true)
            {
                lock (_locker)
                {
                    if (MonitorMicro.Wait(_locker, 1000))
                    {
                        Debug.Print("Signalled OK");
                    }
                    else
                    {
                        Debug.Print("Timout waiting");
                    }


                }
            }
        }

        public void DoWork()
        {
            lock (this._locker)
            {
                MonitorMicro.Pulse(_locker);
            }
        }
    }
}
