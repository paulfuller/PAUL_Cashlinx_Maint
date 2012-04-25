using System;
using System.Windows;
using System.Windows.Controls;
using Common.Controllers.Database.DataAccessLayer;

namespace DSTRViewer
{
    /// <summary>
    /// Interaction logic for DSTRViewerWindow.xaml
    /// </summary>
    public partial class DSTRViewerWindow : Window
    {
        public string Environment { get; set; }
        protected DataAccessTools dataTools;

        public DSTRViewerWindow()
        {
            InitializeComponent();            
        }

        private void storeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DSTRViewerWindowForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Environment))
            {
                MessageBox.Show("Invalid environment specified");
            }

            var propFile = string.Empty;
            if (string.Equals(Environment, "CLXD3"))
            {
                
            }
            //DSTRViewer.Properties.Resources.ResourceManager
        }

        private void DSTRViewerWindowForm_Initialized(object sender, EventArgs e)
        {

        }
    }
}
