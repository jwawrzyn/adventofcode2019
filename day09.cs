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
            program.process(new long[] {});

            //Console.WriteLine(string.Join(',', program.numbers));
            Console.WriteLine(program.output);

        }

        static long determineParameter(long type, long[] numbers, long relativeBase, long addr)
        {
            switch(type)
            {
                case 0 : return addr;
                case 1: return numbers[addr];
                case 2: return relativeBase + addr;
            }

            throw new ArgumentException(string.Format("Invalid parameter type passed: {0}", type));
        }
        static long handleOpCode(long opcode, long value1, long value2)
        {
            switch (opcode)
            {
                case 1: return value1 + value2;
                case 2: return value1 * value2;
                case 7: return value1 < value2 ? 1 : 0;
                case 8: return value1 == value2 ? 1 : 0;
            }

            throw new ArgumentException(string.Format("Invalid opcode passed: {0}", opcode));
        }

        static bool shouldJump(long opcode, long value1)
        {
            if (opcode == 5) return value1 != 0;
            if (opcode == 6) return value1 == 0;

            return false;
        }
    }


}