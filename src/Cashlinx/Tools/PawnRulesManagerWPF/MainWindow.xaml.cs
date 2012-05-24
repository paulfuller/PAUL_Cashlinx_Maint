using System;
using System.Windows;
using Common.Controllers.Rules.Data;
using PawnRulesManagerWPF.Views;

namespace PawnRulesManagerWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RulesHelper.WorkFromTempFile = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RulesHelper.CreateBackup();
            App.RulesTreeView = new RulesTreeView();
            this.spMainContent.Children.Add(App.RulesTreeView);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (App.RulesTreeView != null)
            {
                App.RulesTreeView.PromptChangesNotSaved();
            }

            RulesHelper.DeleteWorkingTempFile();
            App.Current.Shutdown();
        }
    }
}
