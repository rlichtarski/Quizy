namespace quizy
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public required string Question { get; set; }
        public required string[] Answers { get; set; }
        public required string Correct_Answer { get; set; }
    }
}