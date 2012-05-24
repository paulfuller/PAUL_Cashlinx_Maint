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
    public partial class EditBusinessRuleCompWindow : Window
    {

        public EditBusinessRuleCompWindow()
        {
            InitializeComponent();
            spMainContent.Children.Add(new EditBusinessRuleComponent(null, false));
        }

        public EditBusinessRuleCompWindow(BusinessRuleComponentVO comp, bool isReadOnly)            
        {
            InitializeComponent();
            spMainContent.Children.Add(new EditBusinessRuleComponent(comp, isReadOnly));
        }

        public EditBusinessRuleCompWindow(BusinessRuleComponentVO comp, BusinessRuleComponentVO parent, bool isReadOnly)
        {
            InitializeComponent();
            spMainContent.Children.Add(new EditBusinessRuleComponent(comp, parent, isReadOnly));
        }

        public EditBusinessRuleCompWindow(BusinessRuleComponentVO comp, BusinessRuleNodeVO parent, bool isReadOnly)
        {
            InitializeComponent();
            spMainContent.Children.Add(new EditBusinessRuleComponent(comp, parent, isReadOnly));
        }

    }
}
