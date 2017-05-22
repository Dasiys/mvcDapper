using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Func<int, int, int> Add = (x, y) => x + y;
            Console.WriteLine(Add(2,3));

            Console.WriteLine("Hello Word");
            Console.ReadKey();

        }


       

    }
}
