using System;
using System.Windows.Forms;
using Audit.Logic;

namespace Audit.UserControls
{
    public partial class YesNoDropDownList : UserControl
    {
        public YesNoDropDownList()
            : this(null)
        {
        }

        public YesNoDropDownList(InventoryQuestion question)
        {
            InitializeComponent();
            Question = question == null ? new InventoryQuestion(0, string.Empty) : question;
        }

        public bool AnswerNo
        {
            get { return Question.Answer == InventoryAnswer.No; }
        }

        public bool AnswerYes
        {
            get { return Question.Answer == InventoryAnswer.Yes; }
        }

        public InventoryQuestion Question { get; private set; }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Yes"))
            {
                Question.Answer = InventoryAnswer.Yes;
            }
            else
            {
                Question.Answer = InventoryAnswer.No;
            }
        }
    }
}
