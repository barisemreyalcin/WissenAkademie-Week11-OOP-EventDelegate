
namespace EventNumberCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            NumberCheck numberCheck = new NumberCheck();
            numberCheck.NumberState += new NumberCheck.NumberCheckEventHandler(ControlNum);

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int num = rand.Next(40, 70);
                numberCheck.Number = num;
                Thread.Sleep(5000);
            }
        }

        private static void ControlNum()
        {
            Console.WriteLine("You entered a smaller number than 50");
        }
    }

    class NumberCheck
    {
        private const int constantNum = 50;
        public delegate void NumberCheckEventHandler();
        public event NumberCheckEventHandler NumberState;

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                Console.WriteLine(number);

                if (number < constantNum)
                {
                    if (NumberState != null)
                    {
                        NumberState();
                    }
                }
            }
        }
    }
}
