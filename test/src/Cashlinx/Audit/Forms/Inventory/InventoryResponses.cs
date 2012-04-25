using Audit.Logic;

namespace Audit.Forms.Inventory
{
    public partial class InventoryResponses : AuditWindowBase
    {
        public InventoryResponses(InventoryQuestion question)
        {
            Question = question;
            InitializeComponent();
        }

        public InventoryQuestion Question { get; set; }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            Question.Response = txtResponse.Text;
            this.Close();
        }

        private void InventoryResponses_Load(object sender, System.EventArgs e)
        {
            lblNumber.Text = Question.QuestionNumber.ToString();
            lblQuestion.Text = Question.Question;
            txtResponse.Text = Question.Response;
            txtResponse.Focus();
        }
    }
}
