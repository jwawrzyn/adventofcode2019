using System;
using System.Linq;

namespace adventofcode2019.day09
{
    public class longcode
    {
        public static void Run()
        {

            calculate();

        }

        public static void calculate()
        {
            
            //string lines = System.IO.File.ReadAllText("day09_input.txt");
            intcode program = new intcode();
            program.initialize("day09_input.txt");
            program.process(new long[] {1});

            //Console.WriteLine(string.Join(',', program.numbers));
            Console.WriteLine(program.output);

        }

    }


}