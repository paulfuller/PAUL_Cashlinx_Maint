using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using Core.AutomationElementSearch;
using Core.Factory;
using Core.UIItems;
using Core.UIItems.Finders;
using Core.UIItems.TableItems;
using Core.UIItems.WindowItems;
using PawnUtilities.Collection;
using Application=Core.Application;
using Button=Core.UIItems.Button;
using TextBox=Core.UIItems.TextBox;

namespace CashlinxDesktopLoadTester.WhiteBoxController
{
    public class CashlinxDesktopLoader
    {
        public static readonly int THD_DELAY = 20;
        public static readonly int NUM_TRIES = 15;
        private Application desktopApp;
        private Window newDesktopWindow;
        private DictionaryMappedItemFactory factory;
        private bool initialized;

        public Application DesktopApp
        {
            get
            {
                return (this.desktopApp);
            }

        }

        public bool Initialized
        {
            get
            {
                return (this.initialized);
            }
        }

        public CashlinxDesktopLoader(string execFullPath)
        {
            if (string.IsNullOrEmpty(execFullPath))
            {
                return;
            }
            //try
            //{
                this.desktopApp = Application.Launch(execFullPath);
                this.newDesktopWindow = this.desktopApp.GetWindow("Cashlinx Desktop", InitializeOption.NoCache);
                this.initialized = true;
                this.factory = new DictionaryMappedItemFactory();
            //}
            //catch(Exception eX)
            //{
                //this.initialized = false;
                //throw new ApplicationException("CashlinxDesktopLoader ctor threw exception:" + eX.Message, eX);
            //    this.initialized = true;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuButtonNames"></param>
        /// <returns></returns>
        public bool NavigateMenu(List<string> menuButtonNames)
        {
            if (!this.initialized)
                return (false);
            if (CollectionUtilities.isEmpty(menuButtonNames))
            {
                return (false);
            }

            foreach(string s in menuButtonNames)
            {             
                if (string.IsNullOrEmpty(s))
                {
                    return (false);
                }

                bool success;
                var menuButton = TryRetrieveComponent<Button>(this.newDesktopWindow, s, NUM_TRIES, out success);
                if (menuButton == null)
                    return (false);
                menuButton.Click();
            }

            return (true);
        }

        public void KillApplication()
        {
            if (!this.initialized)
            {
                return;
            }

            this.initialized = false;
            this.desktopApp.Kill();
        }

        private static T TryRetrieveComponent<T>(Window w, string textId, int numTrys, out bool success) where T : UIItem
        {
            success = false;
            if (w == null || string.IsNullOrEmpty(textId) && numTrys >= 1)
            {
                return (default(T));
            }
            T rt = default(T);

            int cnt = 0;
            while(cnt <= numTrys)
            {
                rt = w.Get<T>(textId);
                if (rt != null)
                {
                    success = true;
                    break;
                }
                Thread.Sleep(THD_DELAY);
                ++cnt;
            }
            return (rt);
        }

        public bool ExecuteNewPawnLoanFlow(LoadTestInputVO loadVo)
        {
            if (!this.initialized || loadVo == null)
            {
                return(false);
            }

            try
            {
                var menuNav = new List<string>() {"PawnButton", "NewPawnLoanButton"};
                while(!this.NavigateMenu(menuNav))
                {
                    Thread.Sleep(THD_DELAY);
                }

                List<Window> windows = this.desktopApp.GetWindows();
                if (CollectionUtilities.isEmpty(windows))
                {
                    throw new ApplicationException("Cannot retrieve the child windows");
                }
                //Lookup customer form
                Window lookupWindow = windows.Last();
                if (lookupWindow == null)
                {
                    throw new ApplicationException("Cannot find lookup customer window");
                }
                bool success;
                var lookupLastName = TryRetrieveComponent<TextBox>(lookupWindow, "lookupCustomerLastName", NUM_TRIES, out success);
                bool success2;
                var lookupFirstName = TryRetrieveComponent<TextBox>(lookupWindow, "lookupCustomerFirstName", NUM_TRIES, out success2);
                bool success3;
                var findButton = TryRetrieveComponent<Button>(lookupWindow, "lookupCustomerFindButton", NUM_TRIES, out success3);
                if (!success || !success2 || !success3)
                {
                    throw new ApplicationException("Cannot retrieve input fields or find button");
                }
                lookupLastName.BulkText = loadVo.LastName;
                lookupFirstName.BulkText = loadVo.FirstName;
                findButton.Click();
                //Sleep for one second for results to be loaded from the database
                Thread.Sleep(1000);

                windows = this.desktopApp.GetWindows();
                int cnt = 0;
                while(CollectionUtilities.isEmpty(windows) && cnt++ < NUM_TRIES)
                {
                    Thread.Sleep(THD_DELAY);
                }

                Window lookupResults = windows.Last();
                if (lookupResults == null)
                {
                    throw new ApplicationException("Cannot find lookup customer results window");
                }

                bool success4;
                var findDataGrid = TryRetrieveComponent<Table>(
                    lookupResults, "lookupCustomerResultsGrid", NUM_TRIES, out success4);

                if (!success4)
                {
                    throw new ApplicationException("Cannot access data grid in lookup customer results window");
                }

                //Click the select button
                findDataGrid.Rows[0].Cells[0].Click();

            }
            catch (Exception eX)
            {
                MessageBox.Show("Application exception: " + eX.Message);
            }
            finally
            {
                this.KillApplication();
            }
            

            return (true);
        }
    }
}
