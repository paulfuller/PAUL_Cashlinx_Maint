using System;

namespace Cashlinx.Build.Tasks
{
    public class Singleton<T> where T : class
    {
        private static T type;
        private static readonly object padlock = new object();

        public static T GetInstance()
        {
            lock (padlock)
            {
                if (type == null)
                {
                    type = Activator.CreateInstance(typeof(T)) as T;
                }
                return type;
            }
        }
    }
}
