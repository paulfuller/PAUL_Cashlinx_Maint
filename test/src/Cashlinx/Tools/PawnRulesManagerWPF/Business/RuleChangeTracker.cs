using Common.Controllers.Rules.Data;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Common.Libraries.Objects.Rules.Structure;

namespace PawnRulesManagerWPF.Business
{
    /// <summary>
    /// Used to track changes to rules within the app.
    /// </summary>
    public class RulesChangeTracker
    {
        #region Constructor
        
        public RulesChangeTracker()
        {
        }

        
        #endregion Constructor

        #region Private Members

        private ObservableCollection<BusinessRuleComponentVO> _changedComponents = null;
        private ObservableCollection<BusinessRuleNodeVO> _changedRules = null;

        #endregion Private Members

        #region Events
        public delegate void DataChangeHandler();
        public event DataChangeHandler DataChanged;

        #endregion Events

        #region Public Properties
        public  ObservableCollection<BusinessRuleNodeVO> ChangedRules
        {
            get
            {
                if (_changedRules == null)
                {
                    _changedRules = new ObservableCollection<BusinessRuleNodeVO>();
                    //Fires when a change occurs.  This allows us to notify the UI that it is now "dirty."
                    _changedRules.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_CollectionChanged);

                }
                return _changedRules;
            }
        }

        public ObservableCollection<BusinessRuleComponentVO> ChangedComponents
        {
            get
            {
                if (_changedComponents == null)
                {
                    _changedComponents = new ObservableCollection<BusinessRuleComponentVO>();
                    _changedComponents.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_CollectionChanged);
                }
                return _changedComponents;
            }
        }

        #endregion Public Properties

        #region Public Methods
       
        /// <summary>
        /// Saves the tracking changes.
        /// </summary>
        /// <returns>Succesful save or not.</returns>
        public bool SaveChangesToWorkingFile()
        {
            try
            {
                RulesHelper.SaveRules(_changedRules != null ? _changedRules.ToList() : null, 
                    _changedComponents != null? _changedComponents.ToList() : null);

                this.ClearChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to save: " + ex.Message 
                    + Environment.NewLine + "Stack trace: " + ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clears out changes cache, usually to be done after a save.
        /// </summary>
        public void ClearChanges()
        {
            if(_changedComponents != null)
                _changedComponents.Clear();
            if(_changedRules != null)
                _changedRules.Clear();
        }        

        #endregion Public Methods

        #region Events

        private void _CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (DataChanged != null) DataChanged();

        }

        #endregion Events

    }
}
