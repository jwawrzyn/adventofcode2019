using System;
using System.Linq;

namespace adventofcode2019.day09
{
    public class longcode
    {
        public static void Run()
        {
            string result = calculate();
            long output = 0;
            long.TryParse(result, out output);
            Console.WriteLine(result);

        }

        public static string calculate()
        {

            string lines = System.IO.File.ReadAllText("day09_input.txt");

            long[] numbers = lines.Split(",").Select(i => long.Parse(i)).ToArray();

            long index = 0;
            long relativeBase = 0;
            long opcode, number, paramMode1, paramMode2;

            number = numbers[index];

            while (number != 99)
            {
                opcode = number % 10;

                long addr1 = 0, addr2 = 0, addr3 = 0;
                long input1 = 0, input2 = 0, result = 0;

                switch (opcode)
                {
                    case 1:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        addr1 = numbers[index + 1];
                        addr2 = numbers[index + 2];
                        addr3 = numbers[index + 3];

                        paramMode1 = (number / 100) % 10;
                        paramMode2 = (number / 1000) % 10;

                        input1 = determineParameter(paramMode1, numbers, relativeBase, addr1);
                        input2 = determineParameter(paramMode2, numbers, relativeBase, addr2);

                        switch (opcode)
                        {
                            case 1:
                            case 2:
                            case 7:
                            case 8:
                                result = handleOpCode(opcode, input1, input2);
                                index += 4;
                                numbers[addr3] = result;
                                break;
                            case 5:
                            case 6:
                                index = shouldJump(opcode, input1) ? input2 : (index + 3);
                                break;
                            case 9:
                                relativeBase += addr1;
                                break;

                        }

                        break;

                    case 3:
                        addr1 = numbers[index + 1];
                        long input = 1;
                        numbers[addr1] = input;
                        index += 2;
                        break;
                    case 4:
                        addr1 = numbers[index + 1];
                        Console.WriteLine(string.Format("Output code: {0}", numbers[addr1]));
                        if (numbers[index + 2] == 99)
                        {
                            return numbers[addr1].ToString();
                        }
                        index += 2;
                        break;
                }

                number = numbers[index];
            }

            return numbers[0].ToString();
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