using System;
using Microsoft.SPOT;
using NetMf.CommonExtensions.Threading;

namespace Test.NetMf.CommonExtensions
{
    public class PrintAlive : ActiveObject
    {

        protected override void Run()
        {
            lock (this.Locker)
            {
                while (this.Running)
                {
                    Debug.Print("Thread Alive at: " + DateTime.UtcNow);

                    MonitorMicro.Wait(this.Locker, 1000);
                }
            }
        }
    }
}
