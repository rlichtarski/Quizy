namespace quizy
{
    class Program
    {
        static void Main()
        {
            DatabaseManager.CreateTable();
            UserInterface.DisplayMainMenu();
        }
    }
}
