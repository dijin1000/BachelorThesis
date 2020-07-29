namespace LeprechaunHattingProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            DialogManager manager = new DialogManager();
            while (manager.Running)
            {
                manager.GetInput();
            }
            return;
        }
    }
}
