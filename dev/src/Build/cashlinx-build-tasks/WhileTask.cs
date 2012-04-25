using NAnt.Core;
using NAnt.Core.Attributes;

namespace Cashlinx.Build.Tasks
{
    [TaskName("while")]
    public class WhileTask : TaskContainer
    {
        [BuildElement("do")]
        public TaskContainer ChildTask { get; set; }

        [TaskAttribute("property")]
        public string PropertyName { get; set; }

        [TaskAttribute("equals")]
        public string Equals { get; set; }

        [TaskAttribute("notequals")]
        public string NotEquals { get; set; }

        protected override void ExecuteTask()
        {
            while (this.IsTrue())
            {
                this.ChildTask.Execute();
            }
        }

        private bool IsTrue()
        {
            if (!string.IsNullOrEmpty(this.Equals))
            {
                return this.Properties[this.PropertyName] == this.Equals;
            }
            return this.Properties[this.PropertyName] != this.NotEquals;
        }
    }
}
