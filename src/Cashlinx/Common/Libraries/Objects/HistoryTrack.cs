/************************************************************************
* Namespace:       CashlinxDesktop.Desktop
* Class:           HistoryTrack
* 
* Description      The class keeps the information on navigation while
*                  end user is navagating around the app
* 
* History
* David D Wise, Initial Development
* 
* **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;

namespace Common.Libraries.Objects
{
    public class HistoryTrack
    {
        private List<PairType<IntPtr, string>> TransitionForms;
        private string initialFormName;

        // Initial Call is made by the parent Form that will be "king" of the Forms
        public HistoryTrack(Form formParent)
        {
            TransitionForms = new List<PairType<IntPtr, string>>();
            this.initialFormName = string.Empty;
            AddForm(formParent);
        }

        /// <summary>
        /// Returns instance of previous Form after disposing the current.  If only "king" Form is in List,
        /// return instance of the "king"
        /// </summary>
        /// <returns>Returns the previous Form</returns>
        public Form Back()
        {
            Form returnForm = null;
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)           // History has been used at least once. 
            {
                var currentPointer = TransitionForms[iFormCnt - 1].Left;
                var previousPointer = TransitionForms[TransitionForms.Count - 2].Left;

                if (!Control.FromHandle(currentPointer).IsDisposed)
                {
                    var currentForm = (Form)Control.FromHandle(currentPointer);
                    currentForm.Close();
                    currentForm.Dispose();
                    TransitionForms.RemoveAt(iFormCnt - 1);
                    returnForm = (Form)Control.FromHandle(previousPointer);
                    if (returnForm != null && returnForm.Visible)
                    {
                        returnForm.Show();
                    }
                }
            }
            else if (iFormCnt == 1)      // History has only Desktop form loaded.
            {
                var currentPointer = TransitionForms[0].Left;
                if (Control.FromHandle(currentPointer) != null)
                {
                    returnForm = (Form)Control.FromHandle(currentPointer);
                }
            }
            return returnForm;
        }

        public Form Desktop()
        {
            this.initialFormName = string.Empty;
            var returnForm = (Form)Control.FromHandle(TransitionForms[0].Left);
            if (TransitionForms.Count > 1)
            {
                while (TransitionForms.Count > 1)
                {
                    try
                    {
                        var currentForm = (Form)Control.FromHandle(TransitionForms[TransitionForms.Count - 1].Left);
                        if (currentForm != null)
                        {
                            if (!Control.FromHandle(TransitionForms[TransitionForms.Count - 1].Left).IsDisposed)
                            {
                                currentForm.Close();
                                currentForm.Dispose();
                            }
                        }
                        TransitionForms.RemoveAt(TransitionForms.Count - 1);
                    }
                    catch (Exception eX)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                        }
                        BasicExceptionHandler.Instance.AddException("HistoryTrack", eX);
                    }
                }
            }
            returnForm.TopLevel = true;
            returnForm.Focus();
            returnForm.Show();
            return returnForm;
        }

        /// <summary>
        /// Method to lookup a Form in the List.  Returns instance in case calling application needs
        /// to get/set values.  Returns Form instance if not found.
        /// </summary>
        /// <param name="lookupForm"> </param>
        /// <returns>Instance of Form found</returns>
        public Form Lookup(Form lookupForm)
        {
            var returnForm = new Form();
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == lookupForm.Name);
                if (iIdx >= 0)
                {
                    if (!Control.FromHandle(TransitionForms[iIdx].Left).IsDisposed)
                    {
                        returnForm = (Form)Control.FromHandle(TransitionForms[iIdx].Left);
                    }
                }
            }
            return returnForm;
        }

        public bool Lookup(string lookupFormName)
        {
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == lookupFormName);
                if (iIdx >= 0)
                {
                    if (!Control.FromHandle(TransitionForms[iIdx].Left).IsDisposed)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void HideForm(string lookupFormName)
        {
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == lookupFormName);
                if (iIdx >= 0)
                {
                    if (!Control.FromHandle(TransitionForms[iIdx].Left).IsDisposed)
                    {
                        Control.FromHandle(TransitionForms[iIdx].Left).Visible = false;
                    }
                }
            }
        }

        public void VisibleForm(string lookupFormName)
        {
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == lookupFormName);
                if (iIdx >= 0)
                {
                    if (!Control.FromHandle(TransitionForms[iIdx].Left).IsDisposed)
                    {
                        Control.FromHandle(TransitionForms[iIdx].Left).Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Method to record the child Form to List.  Calling application handles whatever it needs to do
        /// with the current Form
        /// </summary>
        /// <param name="formChild"></param>
        public void AddForm(Form formChild)
        {
            var newPair = new PairType<IntPtr, string>(formChild.Handle, formChild.Name);
            if (string.IsNullOrEmpty(this.initialFormName))
            {
                this.initialFormName = formChild.Name;
            }
            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == formChild.Name);
                if (iIdx >= 0)
                {
                    if (!Control.FromHandle(TransitionForms[iIdx].Left).IsDisposed)
                    {
                        var historyForm = (Form)Control.FromHandle(TransitionForms[iIdx].Left);
                        historyForm.Close();
                        historyForm.Dispose();
                        TransitionForms.RemoveAt(iIdx);
                    }
                }
            }
            TransitionForms.Add(newPair);
        }

        /// <summary>
        /// Provides number of Forms populated within the HistoryTrack session.
        /// </summary>
        /// <returns></returns>
        public int FormsInTree()
        {
            return TransitionForms.Count();
        }

        public void ResetFocus()
        {
            Control.FromHandle(TransitionForms[FormsInTree() - 1].Left).Focus();
        }

        public bool HasFormName(string formName)
        {
            if (string.IsNullOrEmpty(formName))
            {
                return(false);
            }

            var iFormCnt = TransitionForms.Count;

            if (iFormCnt > 1)
            {
                var iIdx = TransitionForms.FindIndex(pt => pt.Right == formName);
                if (iIdx >= 0)
                {
                    return (true);
                }
            }

            return (false);
        }

        public string InitialFormName
        {
            get
            {
                return (this.initialFormName);
            }
        }

        public string TriggerName { set; get; }

        public string Trigger { get; set; }
    }
}