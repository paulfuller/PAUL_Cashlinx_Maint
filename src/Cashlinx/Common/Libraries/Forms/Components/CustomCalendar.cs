using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomCalendar : MonthCalendar
    {

        private List<DateTime> _allowedDates;
        public List<DateTime> AllowedDates
        {
            get
            {
                return _allowedDates;
            }
            set
            {
                _allowedDates = value;
                if (AllowedDates != null)
                {
                    boldDates();
                }
            }
        }

        public DateTime SelectedDate { get; set; }

        public CustomCalendar()
        {
            InitializeComponent();
            
            
        }



        private void boldDates()
        {

            foreach (DateTime dt in AllowedDates)
            {
                AddBoldedDate(dt);

            }
            UpdateBoldedDates();

        }

        
        
  


        protected override void OnDateSelected(DateRangeEventArgs drevent)
        {
            SelectedDate = drevent.End.Date;
            DateTime dt = DateTime.MaxValue;
   
            bool validDate = false;
            do
            {

                dt=AllowedDates.Find(dt1 => dt1 == SelectedDate);
                if (dt==DateTime.MaxValue || dt==DateTime.MinValue)
                {
                    SetDate(SelectedDate.AddDays(1));
                    SelectedDate = SelectedDate.AddDays(1);
                    validDate = false;
                }
                else
                {
                    validDate = true;
                }

            } while (!validDate);

            base.OnDateSelected(drevent);

        }


 

    }
}
