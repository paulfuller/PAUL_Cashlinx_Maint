using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms
{
    public partial class ShopOffset : CustomBaseForm
    {
        private bool isLoading;

        public ShopOffset()
        {
            InitializeComponent();

            // pre-populate offset fields with current ShopDateTime instance
            this.yearTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.YearOffset.ToString()) ? "0" : ShopDateTime.Instance.YearOffset.ToString();
            this.monthTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.MonthOffset.ToString()) ? "0" : ShopDateTime.Instance.MonthOffset.ToString();
            this.dayTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.DayOffset.ToString()) ? "0" : ShopDateTime.Instance.DayOffset.ToString();
            this.hourTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.HourOffset.ToString()) ? "0" : ShopDateTime.Instance.HourOffset.ToString();
            this.minuteTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.MinuteOffset.ToString()) ? "0" : ShopDateTime.Instance.MinuteOffset.ToString();
            this.secondTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.SecondOffset.ToString()) ? "0" : ShopDateTime.Instance.SecondOffset.ToString();
            this.millisecondTextBox.Text = string.IsNullOrEmpty(ShopDateTime.Instance.MillisecondOffset.ToString()) ? "0" : ShopDateTime.Instance.MillisecondOffset.ToString();


            if (CheckHasOffset())
            {
                SetDateFromOffsets();
            }
            else
            {
                specificDateTextBox.Value = DateTime.Today;
            }

            // Add event handler for date changed.
            specificDateTextBox.TextChanged += new EventHandler(specificDateTextBox_TextChanged);

            // Add logic to update the date when any offset text is changed
            AddTextboxHandler();
        }

        /* Begin TG 04062012 */
        private void AddTextboxHandler()
        {
            // select each text box on form
            var temp = (from Control c in this.Controls
                        where (c.GetType() == typeof(TextBox))
                        select c).ToList();


            foreach (var txtbox in temp)
            {
                txtbox.TextChanged += new EventHandler(offsetChanged);
            }

        }

        void offsetChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            int offset;

            if (int.TryParse(((TextBox)sender).Text, out offset))
                SetDateFromOffsets();

        }

        void specificDateTextBox_TextChanged(object sender, EventArgs e)
        {

            DateTime dt = new DateTime();

            if (DateTime.TryParse(specificDateTextBox.Text, out dt))
            {
                SetOffsetFromDate();
            }

            isLoading = false;
        }

        private bool CheckHasOffset()
        {
            // select all the textboxes on the form that have a value other than "0"
            var temp = (from Control c in this.Controls
                        where (c.GetType() == typeof(TextBox)) && c.Text.Trim() != "0"
                        select c).ToList();

            return temp.Any();
        }

        private void SetOffsetFromDate()
        {
            DateTime dtToConvert = new DateTime();
            // check for not empty, and that there is a valid datetime
            if (specificDateTextBox.Text.Trim() == string.Empty || DateTime.TryParse(specificDateTextBox.Text, out dtToConvert) == false || isLoading)
                return;

            isLoading = true;
            DateTime tempDate = DateTime.Now;

            int MonthCalculation = DateTimeToMonths(tempDate, dtToConvert);
            // Subtract years
            tempDate = tempDate.AddYears(MonthCalculation / 12);
            yearTextBox.Text = ((MonthCalculation / 12)).ToString();
            // Remove the amount of years from the month's.... (number of years) * 12 = months elapsed
            MonthCalculation -= ((MonthCalculation / 12) * 12);

            // Subtract the number of months.
            tempDate = tempDate.AddMonths(MonthCalculation);
            monthTextBox.Text = MonthCalculation.ToString();

            // Check to see if we have overshot the day we are trying to get to. If we have, add a month.
            if (tempDate < dtToConvert)
            {
                tempDate = tempDate.AddMonths(1);
                // Add a month to the month text box
                monthTextBox.Text = (int.Parse(monthTextBox.Text) + 1).ToString();
            }
            
            // Get the number of days to subtract
            TimeSpan dayTimeSpan = dtToConvert.Subtract(tempDate);
            dayTextBox.Text = ((int)dayTimeSpan.TotalDays).ToString();
            
            isLoading = false;
        }

        private void SetDateFromOffsets()
        {
            if (isLoading)
                return;

            isLoading = true;
            DateTime newDate = new DateTime();
            newDate = DateTime.Now;

            // Add offset to date
            newDate = newDate.AddYears(int.Parse(yearTextBox.Text == "" ? "0" : yearTextBox.Text));
            newDate = newDate.AddMonths(int.Parse(monthTextBox.Text == "" ? "0" : monthTextBox.Text));
            newDate = newDate.AddDays(int.Parse(dayTextBox.Text == "" ? "0" : dayTextBox.Text));
            newDate = newDate.AddHours(int.Parse(hourTextBox.Text == "" ? "0" : hourTextBox.Text));
            newDate = newDate.AddSeconds(int.Parse(secondTextBox.Text == "" ? "0" : minuteTextBox.Text));
            newDate = newDate.AddMinutes(int.Parse(minuteTextBox.Text == "" ? "0" : secondTextBox.Text));
            newDate = newDate.AddMilliseconds(int.Parse(millisecondTextBox.Text == "" ? "0" : millisecondTextBox.Text));

            // Remove the handler for text changed, if not will cause an infinite loop
            specificDateTextBox.Text = newDate.ToString();

            isLoading = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns>Number of months between two DateTime values</returns>
        private int DateTimeToMonths(DateTime dt1, DateTime dt2)
        {
            return (dt2.Year * 12 + dt2.Month) - (dt1.Year * 12 + dt1.Month);
        }

        /* End TG 04062012*/

        private void submitButton_Click(object sender, EventArgs e)
        {
            int years = 0;
            int months = 0;
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int milliseconds = 0;

            // check for valid (numeric) field entries
            if (!int.TryParse(this.yearTextBox.Text, out years) ||
                !int.TryParse(this.monthTextBox.Text, out months) ||
                !int.TryParse(this.dayTextBox.Text, out days) ||
                !int.TryParse(this.hourTextBox.Text, out hours) ||
                !int.TryParse(this.minuteTextBox.Text, out minutes) ||
                !int.TryParse(this.secondTextBox.Text, out seconds) ||
                !int.TryParse(this.millisecondTextBox.Text, out milliseconds))
            {
                MessageBox.Show(
                    "Offset values must be numeric",
                    "Invalid Offsets - Non-numeric values",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {

                // check to make sure that values do not go out of range
                //if (DateTime.Compare(dtToConvert, DateTime.Now) == -1 && DateTime.Now.Year -  dtToConvert.Year  < 50)
                if (years > 0 || months > 0 || days > 0 || hours > 0 || minutes > 0 || milliseconds > 0 ||
                    years < -50 || months < -12 || days < -365 || hours < -24 || minutes < -60 || seconds < -60 || milliseconds < -1000)
                {
                    MessageBox.Show(
                    "Offset values are not in an acceptable range",
                    "Invalid Offsets - Offset values are out of range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                else
                {
                    //Set offsets
                    ShopDateTime.Instance.setOffsets(
                        years,
                        months, days, hours, minutes, seconds, milliseconds);
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
