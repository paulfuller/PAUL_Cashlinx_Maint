using System.Resources;
using Common.Controllers.Application.ApplicationFlow;

namespace Audit.Logic
{
    public class ButtonResourceManagerHelper : IButtonResourceManagerHelper
    {
        #region IButtonResourceManagerHelper Members

        public ResourceManager GetResourceManager()
        {
            return Audit.ButtonResources.ResourceManager;
        }

        #endregion
    }
}
