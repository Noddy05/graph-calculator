namespace GraphCalculator
{
    internal class Program
    {
        private static Window? window;
        public static Window GetWindow() => window!;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (window = new Window())
            {
                window.Run();
            }
        }
    }
}