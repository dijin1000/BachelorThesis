namespace LeprechaunHattingProblem
{
    class DialogManager
    {
        //Variables
        private World world;
        private bool isInstantiated;
        private bool gettingInput;
        private bool running;
        public bool Running { get { return running; } }

        //Constructor
        public DialogManager()
        {
            isInstantiated = false;
            gettingInput = false;
            StartUpDialog();
            running = true;
            gettingInput = true;
        }

        //Private Functions
        private void StartUpDialog()
        {
            System.Console.WriteLine("Starting Program.");
        }
        private World CreateWorld()
        {
            System.Console.WriteLine("Creating a world");
            System.Console.WriteLine("How many leprechaun's ?");
            int n = GetNumber();
            System.Console.WriteLine("How many colors's ?");
            int m = GetNumber();
            return new World(n, m);
        }
        private int GetNumber()
        {
            bool validAnswer = false;
            int result = 0;
            while (validAnswer == false)
            {
                string input = System.Console.ReadLine();
                if (int.TryParse(input, out result) == false)
                {
                    System.Console.WriteLine("Not a valid number.");
                    System.Console.WriteLine("Try again.");
                }
                else
                {
                    validAnswer = true;
                }
            }
            return result;
        }
        private void WorldCommandos(string fixedInput)
        {
            switch (fixedInput)
            {
                case "quit":
                    running = false;
                    break;
                default:
                    break;
            }
        }
        private void NoWorld(string fixedInput)
        {
            switch (fixedInput)
            {
                case "quit":
                    running = false;
                    break;
                case "create":
                    world = CreateWorld();
                    isInstantiated = true;
                    break;
                case "default":
                    world = new World();
                    isInstantiated = true;
                    break;
                default:
                    break;
            }
        }

        //Public Functions
        public void GetInput()
        {
            if (gettingInput == true)
            {
                gettingInput = false;
                string input = System.Console.ReadLine();
                string fixedInput = input.ToLower();
                if (isInstantiated)
                    WorldCommandos(fixedInput);
                else
                    NoWorld(fixedInput);
                gettingInput = true;
            }
        }
    }
}
