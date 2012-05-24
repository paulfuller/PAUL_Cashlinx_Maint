using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System.Windows;
using Common.Libraries.Objects.Rules.Structure;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for EditBusinessRuleWindow.xaml
    /// </summary>
    public partial class EditBusinessRuleWindow : Window
    {
        public EditBusinessRuleWindow(BusinessRuleNodeVO rule)
        {
            InitializeComponent();

            spMainContent.Children.Add(new EditBusinessRule(rule, false));
        }
    }
}
