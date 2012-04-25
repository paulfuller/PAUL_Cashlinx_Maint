using System.Resources;
using Common.Controllers.Application.ApplicationFlow;

namespace Pawn.Logic
{
    public class ButtonResourceManagerHelper : IButtonResourceManagerHelper
    {
        #region IButtonResourceManagerHelper Members

        public ResourceManager GetResourceManager()
        {
            return Pawn.ButtonResources.ResourceManager;
        }

        #endregion
    }
}
