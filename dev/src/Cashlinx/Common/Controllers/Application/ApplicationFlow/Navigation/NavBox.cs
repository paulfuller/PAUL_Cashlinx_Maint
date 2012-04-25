namespace Common.Controllers.Application.ApplicationFlow.Navigation
{
    public class NavBox
    {
        public delegate void NavBoxActionFired(object sender, object data);
        public enum NavAction
        {
            NONE,
            RETRY,
            CANCEL,
            SUBMIT,
            BACK,
            BACKANDSUBMIT,
            MSGBOX,
            HIDE,
            HIDEANDSHOW
        }

        //public bool Called { get; set; }
        public object Owner { get; set; }
        private NavAction action;
        public NavAction Action
        {
            get
            {
                return (this.action);
            }
            set
            {
                this.action = value;
                this.fireNavAction();
            }
        }
        public bool IsCustom { get; set; }
        public string CustomDetail
        {
            get;
            set;
        }
        public NavBoxActionFired OnActionFire { get; set; }

        private void fireNavAction()
        {
            //this.Called = true;
            this.OnActionFire.Invoke(this, this.Owner);
        }

        public NavBox()
        {
            //this.Called = false;
            this.Owner = null;
            this.IsCustom = false;
            this.CustomDetail = string.Empty;
            this.action = NavAction.NONE;
        }
    }
}
