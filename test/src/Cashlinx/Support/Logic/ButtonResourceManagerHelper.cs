using System.Resources;
using Common.Controllers.Application.ApplicationFlow;

namespace Support.Logic
{
    public class ButtonResourceManagerHelper : IButtonResourceManagerHelper
    {
        #region IButtonResourceManagerHelper Members

        public ResourceManager GetResourceManager()
        {
            return Support.MenuButtonResources.ResourceManager;
                
        }

        #endregion
    }
}
