using System;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2019.day07
{
    public class program {
        public static void Run()
        {
            long[] input = {9,7,8,5,6};
            long maxThrust = -1;

            List<long[]> firstPerms = GetPermutations(new List<long> { 0, 1, 2, 3, 4 });
            List<long[]> secondPerms = GetPermutations(new List<long> { 5, 6, 7, 8, 9 });

            Queue<long> outputs;
            //secondPerms.Clear();
            //secondPerms.Add(input);
            foreach(long[] perm in secondPerms){
                int totalThurst = 0;
                int result = 0;
                bool halt = false;
                outputs = new Queue<long>();
                intcode process1 = new intcode();
                intcode process2 = new intcode();
                intcode process3 = new intcode();
                intcode process4 = new intcode();
                intcode process5 = new intcode();

                process1.initialize();
                process2.initialize();
                process3.initialize();
                process4.initialize();
                process5.initialize();
                
            
                process1.process(new int[] {(int)perm[0], 0});
                int result1 = process1.output;
                process2.process(new int[] {(int)perm[1], result1} );
                int result2 = process2.output;
                process3.process(new int[] {(int)perm[2], result2});
                int result3 = process3.output;
                process4.process(new int[] {(int)perm[3], result3});
                int result4 = process4.output;
                process5.process(new int[] {(int)perm[4], result4});
                int result5 = process5.output;
                do {
                    int stopProcessing = 0;
                    stopProcessing = process1.process(new int[] {result5});
                    result1 = process1.output;
                     stopProcessing = process2.process(new int[] {result1});
                    result2 = process2.output;
                stopProcessing = process3.process(new int[] {result2});
                    result3 = process3.output;
                stopProcessing = process4.process(new int[] {result3});
                    result4 = process4.output;
                stopProcessing = process5.process(new int[] {result4});
                    result5 = process5.output;


                    halt = stopProcessing > 0;
                    
                    totalThurst = result5;
                    //Console.WriteLine(String.Format("Output 5 {0}", process5.output));
                }while(!halt);

                Console.WriteLine(string.Format("Max thrust {0}", totalThurst));
                if(totalThurst > maxThrust){
                    maxThrust = totalThurst;
                }
            }
            Console.WriteLine(maxThrust);
        }
        private static List<long[]> GetPermutations(List<long> things, List<long> current = null)
        {
            List<long[]> res = new List<long[]>();
            if(current == null)
            {
                current = new List<long>();
            }
            if (things.Count > 0)
            {
                foreach (long t in things)
                {
                    List<long> newP = new List<long>(current);
                    newP.Add(t);

                    List<long> newThings = new List<long>(things);
                    newThings.Remove(t);
                    res.AddRange(GetPermutations(newThings, newP));
                }
            }
            else
            {
                res.Add(current.ToArray());
            }

            return res;
        }


    }
    public class intcode
    {
        public int index {get; set;}
        public int output {get; set;}
        public bool halt {get; set;}
        public int[] numbers {get; set;}
        public void initialize(){
            string lines = System.IO.File.ReadAllText("day07_input.txt");

            numbers = lines.Split(",").Select(i => int.Parse(i)).ToArray();

        }
        public int process(int[] signals)
        {
          
            //int index = 0;
            int opcode, number, paramMode1, paramMode2;

            int signalPosition = 0;

            number = numbers[index];
            halt = false;
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
                        
                        numbers[addr1] = signals[signalPosition];
                        signalPosition ++;
                        index += 2;
                        break;
                    case 4:
                        addr1 = numbers[index + 1];
                        //Console.WriteLine(string.Format("Output code: {0}", numbers[addr1]));
                        output = numbers[addr1];
                        index += 2;
                        return 0;
                }

                number = numbers[index];
                if(number == 99) halt = true;
            }

            return 1;
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