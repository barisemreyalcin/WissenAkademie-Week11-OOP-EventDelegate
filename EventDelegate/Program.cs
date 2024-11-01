using System.Reflection;

namespace EventDelegate
{
    class Program
    {
        delegate void MessageStringHandler();
        delegate int SumIntHandler();
        delegate string MessageHandler();
        delegate int OperationHandler(int number1, int number2);
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //ShowMessage(); // Normal çalışıyor

            // ShowMessage çağırmanın başka yolu
            //MessageStringHandler msg = new MessageStringHandler(ShowMessage);
            //msg();
            //msg.Invoke();

            //SumIntHandler sumOperation = new SumIntHandler(Sum);
            //int sum = sumOperation.Invoke();
            //Console.WriteLine(sum);

            //MessageHandler msgHandler = new MessageHandler(Message);
            //string message = msgHandler.Invoke();
            //Console.WriteLine(message);

            OperationHandler operations = new OperationHandler(Sum);
            operations += Substract;
            operations += Multiply;
            operations += Divide; // En sonuncuyu yazar (3)

            //operations -= Substract;
            //operations -= Multiply;
            //operations -= Divide; // Geri kaldırır ve Sum result yazar

            //int result = operations.Invoke(24, 8);
            //Console.WriteLine(result);

            Delegate[] delegateList = operations.GetInvocationList();

            foreach (Delegate item in delegateList)
            {
                Console.WriteLine($"Method Name: {item.Method.Name}");
                Console.WriteLine($"Return Type: {item.Method.ReturnType}");
                Console.WriteLine($"Method Static State: {item.Method.IsStatic}");
                Console.WriteLine($"Method Public State: {item.Method.IsPublic}");

                ParameterInfo[] parameterList = item.Method.GetParameters();
                foreach (ParameterInfo param in parameterList)
                {
                    Console.WriteLine(
                        $"{param.Name} - " +
                        $"{param.IsOptional} - " +
                        $"{param.DefaultValue} - " +
                        $"{param.IsOut} - "
                        );
                }

                int result = (int)item.DynamicInvoke(24, 8);
                Console.WriteLine($"{item.Method.Name} operation result: {result}");
                Console.WriteLine(new string('-',50));
            }
        }

        static int Sum(int x, int y)
        {
            return x + y;
        }

        static int Substract(int x, int y)
        {
            return x - y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }

        static int Divide(int x, int y)
        {
            return x / y;
        }

        static string Message()
        {
            return "Hello world!";
        }

        static void ShowMessage()
        {
            Console.WriteLine("Void ve parametre almayan method");
        }

        //static int Sum()
        //{
        //    Random rand = new Random();
        //    int number1 = rand.Next(0,50);
        //    int number2 = rand.Next(0,50);
        //    int result = number1 + number2;
        //    return result;
        //}
    }
}
