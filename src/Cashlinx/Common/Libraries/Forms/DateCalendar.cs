using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms
{
    public partial class DateCalendar : UserControl
    {
        MonthCalendar newCalendar = new MonthCalendar();
        public delegate void SelectedDateHandler();
        public event SelectedDateHandler SelectDate;
        public event EventHandler CalendarMouseLeave;
        public event EventHandler CalendarMouseLeaving;
        public event EventHandler SelectedDateChanged;
        public event EventHandler SelectedDateChanging;
        public string SelectedDate
        {
            get
            {
                return this.dateText.DateTextBox.Text;
            }
            set
            {
                this.dateText.DateTextBox.Text = value;
            }
        }
        public bool AllowWeekends
        {
            get;
            set;
        }
        public bool AllowMonthlySelection
        {
            get;
            set;
        }
        public bool AllowKeyUpAndDown
        {
            get;
            set;
        }

        [Browsable(false)]
        public Date DateText
        {
            get { return dateText; }
        }

        public bool PositionPopupCalendarOverTextbox { get; set; }

        public HorizontalAlignment TextAlign
        {
            get { return dateText.TextAlign; }
            set { dateText.TextAlign = value; }
        }

        public void SetSelectedDate(DateTime date)
        {
            dateText.DateTextBox.Text = date.ToString("MM/dd/yyyy");
            newCalendar.SetDate(date.Date);
        }

        public event EventHandler TextBoxTextChanged;

        public DateCalendar()
        {
            InitializeComponent();
        }

        private void DateCalendar_Load(object sender, EventArgs e)
        {
            this.newCalendar.Name = "newCalendar";
            this.newCalendar.Visible = false;
            this.newCalendar.DateSelected += this.newCalendar_DateSelected;
            this.newCalendar.MouseLeave += this.newCalendar_MouseLeave;
            this.dateText.ErrorMessage = Commons.GetMessageString("InvalidDate");
            if (!AllowWeekends)
            {
                DateTime todaysDate = ShopDateTime.Instance.ShopDate;
                int startYear = todaysDate.Year;
                int endYear = startYear + 20;
                DateTime startDate = new DateTime(startYear, 1, 1);
                DateTime endDate = new DateTime(endYear, 12, 31);
                DateTime currentDate;
                for (currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
                {
                    DateTime dt = currentDate;
                    if (!new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession).IsShopClosed(dt))
                        newCalendar.AddBoldedDate(dt);

                }
                newCalendar.UpdateBoldedDates();
            }
            this.Controls.Add(newCalendar);

        }

        private void pictureBoxCalendar_Click(object sender, EventArgs e)
        {
            if (SelectedDate != string.Empty && SelectedDate != "mm/dd/yyyy")   
            {
                newCalendar.SetDate(Utilities.GetDateTimeValue(SelectedDate));
            }
            if (AllowMonthlySelection)
            {
                newCalendar.RemoveAllBoldedDates();
                newCalendar.RemoveAllMonthlyBoldedDates();
                newCalendar.RemoveAllAnnuallyBoldedDates();
                DateTime todaysDate = Utilities.GetDateTimeValue(SelectedDate);
                int startYear = todaysDate.Year;
                int endYear = startYear + 20;

                DateTime endDate = new DateTime(endYear, 12, todaysDate.Day);
                DateTime currentDate;
                for (currentDate = todaysDate; currentDate <= endDate; currentDate = currentDate.AddMonths(1))
                {
                    DateTime dt = currentDate;
                    bool dateAdded = false;
                    do
                    {
                        if (!AllowWeekends)
                        {
                            if (!new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession).IsShopClosed(dt))
                            {
                                newCalendar.AddBoldedDate(dt);
                                dateAdded = true;
                            }
                            else
                                dt = dt.AddDays(1);
                        }
                        else
                        {
                            newCalendar.AddBoldedDate(dt);
                            dateAdded = true;
                        }

                    } while (!dateAdded);

                }
                newCalendar.UpdateBoldedDates();

            }

            if (PositionPopupCalendarOverTextbox)
            {
                newCalendar.Location = new Point(dateText.Location.X, dateText.Location.Y);
            }
            else
            {
                newCalendar.Location = new System.Drawing.Point(pictureBoxCalendar.Location.X, pictureBoxCalendar.Location.Y);
            }
            newCalendar.Visible = true;
            newCalendar.BringToFront();
            this.pictureBoxCalendar.Visible = false;

        }

        private void newCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            newCalendar.Visible = false;
            this.pictureBoxCalendar.Visible = true;

            if (SelectedDateChanging != null)
            {
                SelectedDateChanging(sender, EventArgs.Empty);
            }

            this.dateText.Controls[0].Text = newCalendar.SelectionEnd.Date.FormatDate();

            if (SelectedDateChanged != null)
            {
                SelectedDateChanged(sender, e);
            }
        }

        private void newCalendar_MouseLeave(object sender, EventArgs e)
        {
            newCalendar.Visible = false;
            this.pictureBoxCalendar.Visible = true;

            if (CalendarMouseLeaving != null)
            {
                CalendarMouseLeaving(sender, EventArgs.Empty);
            }

            this.dateText.Controls[0].Text = newCalendar.SelectionEnd.Date.FormatDate();

            if (CalendarMouseLeave != null)
            {
                CalendarMouseLeave(sender, EventArgs.Empty);
            }
        }

        private void dateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (AllowKeyUpAndDown)
            {
                DateTime currentDate;
                try
                {
                    currentDate = DateTime.Parse(SelectedDate);
                }
                catch (Exception)
                {
                    currentDate = newCalendar.SelectionEnd.Date;
                }

                if (AllowMonthlySelection)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        SelectedDate = currentDate.AddMonths(1).FormatDate();

                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        SelectedDate = currentDate.AddMonths(-1).FormatDate();

                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        SelectedDate = currentDate.AddDays(1).FormatDate();

                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        SelectedDate = currentDate.AddDays(-1).FormatDate();

                    }
                }
                SelectDate();
            }
        }

        private void dateText_TextBoxTextChanged(object sender, EventArgs e)
        {
            if (TextBoxTextChanged != null)
            {
                TextBoxTextChanged(sender, e);
            }
        }


    }
}
