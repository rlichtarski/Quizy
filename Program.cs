namespace quizy
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager.CreateTable();
            UserInterface.DisplayMainMenu();
        }
    }
}
