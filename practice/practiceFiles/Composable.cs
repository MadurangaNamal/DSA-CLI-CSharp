using practice.practiceFiles.Models;

namespace practice.practiceFiles
{
    public delegate void MyDelegate(int arg1, ref int arg2);

    public delegate int WorkPerformedHandler(int hours, WorkType workType);

    public class Composable
    {
        public static void Func1(int arg1, ref int arg2)
        {
            string result = (arg1 + arg2).ToString();
            Console.WriteLine("Result from f1:" + result);
            arg2 += 20;
        }

        public static void Func2(int arg1, ref int arg2)
        {
            string result = (arg1 * arg2).ToString();
            Console.WriteLine("Result from f2:" + result);
        }

    }
}
