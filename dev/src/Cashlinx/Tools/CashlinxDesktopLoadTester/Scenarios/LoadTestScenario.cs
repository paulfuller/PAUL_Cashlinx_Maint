using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CashlinxDesktopLoadTester.InputHandling;
using Common.Libraries.Utility.Type;

namespace CashlinxDesktopLoadTester.Scenarios
{
    public class LoadTestScenario
    {
        #region Constants And Enums
        public enum ControlType
        {
            UNKNOWN, 
            UNKNOWN_XY,
            TEXTBOX,
            BUTTON,
            LISTBOX,
            COMBOBOX,
            AUTOCOMPLETE,
            DATAGRIDROW,
            DATAGRIDROWCOLUMN
        }

        public enum ControlTriggerType
        {
            HOVER,
            CLICK,
            DOUBLECLICK,
            TEXTFOCUSOUT,
            TABOUT,
            SELECTIONCHANGED,
            COMBOSELECT
        }

        #endregion

        #region Properties
        public string TestName
        { 
            get
            {
                return (this.testName);
            }
        }
        #endregion


        #region Private fields
        private Dictionary<string, System.Windows.Forms.Form> loadForms;
        private Dictionary<string, Dictionary<string,
            TupleType<System.Windows.Forms.Control, ControlType, ControlTriggerType>>> loadFormControlTriggers;
        private Dictionary<string, Dictionary<string, string>> loadFormValues;
        private Dictionary<int, string> formExecutionOrder;
        private string testName;

        #endregion 

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmName"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        private bool isFormValid(string fmName, out Form form)
        {
            form = null;
            if (string.IsNullOrEmpty(fmName) ||
                this.loadForms.ContainsKey(fmName) == false ||
                this.loadFormValues.ContainsKey(fmName) == false ||
                this.loadFormControlTriggers.ContainsKey(fmName) == false)
            {
                return (false);
            }
            form = this.loadForms[fmName];
            return form != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ctlName"></param>
        /// <param name="ctl"></param>
        /// <returns></returns>
        private bool findControlByName(Form form, string ctlName, out Control ctl)
        {
            ctl = null;
            if (form == null || string.IsNullOrEmpty(ctlName))
            {
                throw new ApplicationException("Cannot find form to search for control in test: " + this.testName);
            }
            bool rt = false;
            foreach (Control curCtl in form.Controls)
            {
                if (curCtl == null)
                    continue;
                if (!curCtl.Name.Equals(ctlName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                ctl = curCtl;
                rt = true;
                break;
            }
            return (rt);
        }

        /// <summary>
        /// Populate the control triggers dictionary with valid Control objects
        /// The other fields are set by the user
        /// </summary>
        /// <param name="fmName"></param>
        private void populateControlTriggers(string fmName)
        {
            Form form;
            if (this.isFormValid(fmName, out form))
            {
                throw new ApplicationException("Cannot use null form to populate control trigger structures");
            }

            Dictionary<string, TupleType<System.Windows.Forms.Control, ControlType, ControlTriggerType>> triggerDct =
                this.loadFormControlTriggers[fmName];
            if (CollectionUtilities.isEmpty(triggerDct))
            {
                throw new ApplicationException("Trigger dictionary for form: " + fmName + " for test scenario: " + this.testName + " is invalid.");
            }

            foreach (string tKey in triggerDct.Keys)
            {
                if (string.IsNullOrEmpty(tKey)) continue;

                TupleType<Control, ControlType, ControlTriggerType> tVal = triggerDct[tKey];
                if (tVal == null) continue;

                Control ctl;
                if (!this.findControlByName(form, tKey, out ctl))
                {
                    throw new ApplicationException("Cannot find trigger dictionary control named: " + tKey + " in form " +
                                                   fmName + " for test scenario: " + this.testName);
                }

                //Set the control and update the entry
                tVal.Left = ctl;
                triggerDct[tKey] = tVal;
            }
        }

        private void internalTriggerControl(Control ctl, ControlType cType, ControlTriggerType cTrigType)
        {
            if (ctl == null)
            {
                throw new ApplicationException("internalTriggerControl cannot trigger a null control in test: " + this.testName);
            }

            Cursor.Position =
                new Point(ctl.Location.X + (ctl.Bounds.Width / 2), ctl.Location.Y + (ctl.Bounds.Height / 2));
            SendInputWrapper.Click();
/*            switch (cType)
            {
                case ControlType.UNKNOWN:
                    break;
                case ControlType.UNKNOWN_XY:
                    Cursor.Position =
                        new Point(ctl.Location.X + (ctl.Bounds.Width / 2), ctl.Location.Y + (ctl.Bounds.Height / 2));
                    SendInputWrapper.Click();
                    break;
                case ControlType.TEXTBOX:
                    break;
                case ControlType.BUTTON:
                    break;
                case ControlType.LISTBOX:
                    break;
                case ControlType.COMBOBOX:
                    break;
                case ControlType.AUTOCOMPLETE:
                    break;
                case ControlType.DATAGRIDROW:
                    break;
                case ControlType.DATAGRIDROWCOLUMN:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("cType");
            }*/
        }

        #endregion


        #region Constructor

        public LoadTestScenario(string nm)
        {
            this.loadForms = new Dictionary<string, Form>();
            this.loadFormValues = new Dictionary<string, Dictionary<string, string>>();
            this.formExecutionOrder = new Dictionary<int, string>();
            this.loadFormControlTriggers = new Dictionary<string, Dictionary<string, 
                TupleType<Control, ControlType, ControlTriggerType>>>();
            this.testName = nm;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmName"></param>
        /// <param name="form"></param>
        /// <param name="execOrder"></param>
        /// <param name="fmValues"></param>
        /// <param name="fmCtlTriggers"></param>
        public void AddLoadForm(string fmName, Form form, 
            int execOrder, Dictionary<string, string> fmValues,
            Dictionary<string, TupleType<Control, ControlType, ControlTriggerType>> fmCtlTriggers)
        {
            //Add passed in parameters
            CollectionUtilities.AddIfNoEntryExists(this.loadForms, fmName, form);
            CollectionUtilities.AddIfNoEntryExists(this.loadFormValues,
                                                   fmName, fmValues);
            CollectionUtilities.AddIfNoEntryExists(this.formExecutionOrder,
                                                   execOrder, fmName);
            CollectionUtilities.AddIfNoEntryExists(this.loadFormControlTriggers,
                                                   fmName, fmCtlTriggers);

            //If data is valid and form is valid, populate control triggers as they
            //will be passed in with null control references
            this.populateControlTriggers(fmName);

        }

        /// <summary>
        /// Take the field values passed in and place them in the form
        /// </summary>
        /// <param name="fmName"></param>
        public void SetFieldsOnForm(string fmName)
        {
            Form curForm;
            if (!this.isFormValid(fmName, out curForm))
            {
                throw new ApplicationException("Cannot set fields in form in test: " + this.testName);
            }

            Dictionary<string, string> fmValueDict = this.loadFormValues[fmName];
            if (CollectionUtilities.isEmpty(fmValueDict))
            {
                throw new ApplicationException("Form value dictionary is invalid in test: " + this.testName);
            }

            foreach (string curKey in fmValueDict.Keys)
            {
                if (string.IsNullOrEmpty(curKey)) continue;
                string curVal = fmValueDict[curKey];
                Control fndControl;
                if (!this.findControlByName(curForm, curKey, out fndControl))
                {
                    throw new ApplicationException("Could not find control " + curKey + " in form " + fmName +
                                                   " for test scenario " + this.testName);
                }
                fndControl.Text = curVal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmName"></param>
        /// <param name="ctlName"></param>
        public void TriggerControlOnForm(string fmName, string ctlName)
        {       
            if (string.IsNullOrEmpty(fmName) || string.IsNullOrEmpty(ctlName))
            {
                throw new ApplicationException("Invalid form name and/or control name specified in test: " + this.testName + " while trying to trigger a control.");
            }

            Form curForm;
            if (!this.isFormValid(fmName, out curForm))
            {
                throw new ApplicationException("Cannot trigger control in form in test: " + this.testName);
            }

            //Get control dictionary for this form
            Dictionary<string, TupleType<Control, ControlType, ControlTriggerType>> ctlDctToFind =
                this.loadFormControlTriggers[fmName];
            if (CollectionUtilities.isEmpty(ctlDctToFind))
            {
                throw new ApplicationException("Cannot find trigger control dictionary in form: " + fmName + " in test: " + this.testName);
            }

            //Find exact control entry
            if (!ctlDctToFind.ContainsKey(ctlName))
            {
                throw new ApplicationException("Cannot find trigger control: " + ctlName + " in form: " + fmName +
                                               " in test: " + this.testName);
            }

            //Retrieve control entry
            TupleType<Control, ControlType, ControlTriggerType> ctlTuple = ctlDctToFind[ctlName];
            Control ctlObj = ctlTuple.Left;
            ControlType ctlType = ctlTuple.Mid;
            ControlTriggerType ctlTrgType = ctlTuple.Right;

            this.internalTriggerControl(ctlObj, ctlType, ctlTrgType);
        }

        #endregion 
    }
}
