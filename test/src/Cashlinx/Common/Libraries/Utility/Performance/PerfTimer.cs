using System;

namespace Common.Libraries.Utility.Performance
{
    public class PerfTimer
    {
        #region Private Fields
        private string context;
        private double durationMillis;
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan deltaTime;
        private bool running;
        #endregion

        #region Private Methods
        private void initialize(string ctx)
        {
            lock (this)
            {
                this.context = ctx ?? @"NoContext";
                this.running = true;
                this.startTime = DateTime.Now;
            }
        }
        #endregion


        public PerfTimer(string ctx)
        {
            this.running = false;
            this.initialize(ctx);
        }

        public PerfTimer(object refObj, string methName)
        {
            this.running = false;
            var refName = @"Static";
            if (refObj != null)
            {
                refName = refObj.GetType().Name;
            }
            var methodName = methName ?? @"UnknownMethod";
            this.initialize(refName + "::" + methodName);
        }

        public void Stop()
        {
            if (!this.running) return;
            lock (this)
            {
                this.running = false;
                this.endTime = DateTime.Now;
                this.deltaTime = this.endTime - this.startTime;
                this.durationMillis = (endTime - startTime).TotalMilliseconds;
            }
        }

        public override string ToString()
        {
            if (this.running)
                this.Stop();
            return ("(" + this.context + "): " + this.durationMillis + " ms - TimeSpan" + this.deltaTime.ToString());
        }
    }
}