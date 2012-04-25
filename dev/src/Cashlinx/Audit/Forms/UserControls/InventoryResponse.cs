using System;
using System.Windows.Forms;
using Audit.Forms.Inventory;
using Audit.Logic;

namespace Audit.UserControls
{
    public partial class InventoryResponse : UserControl
    {
        public InventoryResponse(InventoryQuestion question)
        {
            InitializeComponent();
            Question = question;
        }

        public InventoryQuestion Question { get; private set; }

        private void InventoryResponse_Load(object sender, EventArgs e)
        {
            lblQuestionNumber.Text = Question.QuestionNumber.ToString();
            lblQuestion.Text = Question.Question;
            LoadResponse();
        }

        private void lblQuestion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InventoryResponses response = new InventoryResponses(Question);
            response.ShowDialog();

            LoadResponse();
        }

        private void LoadResponse()
        {
            lblComment.Text = Question.Response;
        }
    }
}
