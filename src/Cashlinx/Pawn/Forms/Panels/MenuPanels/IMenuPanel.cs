using Common.Controllers.Application.ApplicationFlow;

namespace Pawn.Forms.Panels.MenuPanels
{
    interface IMenuPanel
    {
        ImageButtonControllerGroup ButtonControllers { get;}
        MenuLevelController MenuController { get; }
    }
}
