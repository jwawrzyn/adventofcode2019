using System;
using System.Linq;

namespace adventofcode2019.day05
{
    public class problem01
    {
        public static void Run()
        {
            // for(int noun = 0; noun < 100;  noun ++){
            //     for(int verb = 0; verb < 100; verb ++){
            string result = calculate(12, 2);
            int output = 0;
            int.TryParse(result, out output);
            Console.WriteLine(result);
            // if(output == 19690720){
            //     Console.WriteLine(string.Format("Noun: {0}, Verb: {1}", noun.ToString(), verb.ToString()));
            //     Console.WriteLine((100 * noun + verb).ToString());
            // }
            //     }
            // }
        }

        public static string calculate(int noun, int verb)
        {

            string lines = System.IO.File.ReadAllText("day05_input.txt");

            int[] numbers = lines.Split(",").Select(i => int.Parse(i)).ToArray();

            int index = 0;
            int opcode, number, paramMode1, paramMode2;

            number = numbers[index];
            //numbers[1] = noun;
            //numbers[2] = verb;
            while (number != 99)
            {
                opcode = number % 10;

                int addr1 = 0, addr2 = 0, addr3 = 0;
                int input1 = 0, input2 = 0, result = 0;

                switch (opcode)
                {
                    case 1:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        addr1 = numbers[index + 1];
                        addr2 = numbers[index + 2];
                        addr3 = numbers[index + 3];

                        paramMode1 = (number / 100) % 10;
                        paramMode2 = (number / 1000) % 10;

                        input1 = paramMode1 == 1 ? addr1 : numbers[addr1];
                        input2 = paramMode2 == 1 ? addr2 : numbers[addr2];
                        if (opcode != 5 && opcode != 6)
                        {

                            result = handleOpCode(opcode, input1, input2);
                            index += 4;
                            numbers[addr3] = result;
                        }
                        else
                        {
                            index = shouldJump(opcode, input1) ? input2 : (index + 3);
                        }

                        break;

                    case 3:
                        addr1 = numbers[index + 1];
                        int input = 5;//1;
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

        static int handleOpCode(int opcode, int value1, int value2)
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

        static bool shouldJump(int opcode, int value1)
        {
            if (opcode == 5) return value1 != 0;
            if (opcode == 6) return value1 == 0;

            return false;
        }
    }


}