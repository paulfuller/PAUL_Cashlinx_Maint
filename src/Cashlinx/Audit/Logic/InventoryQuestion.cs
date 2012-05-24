
namespace Audit.Logic
{
    public class InventoryQuestion
    {
        public InventoryQuestion(int questionNumber, string question)
        {
            Question = question;
            QuestionNumber = questionNumber;
        }

        public InventoryAnswer Answer { get; set; }
        public string Response { get; set; }
        public string Question { get; set; }
        public int QuestionNumber { get; set; }
    }
}
