using System;
using System.Linq;

namespace adventofcode.day05
{
    public class problem01
    {
        public static void Run(){
            // for(int noun = 0; noun < 100;  noun ++){
                // for(int verb = 0; verb < 100; verb ++){
                    int result = calculate();
                    int output = 0;
                    //int.TryParse(result, out output);

                    // if(output == 19690720){
                    //     Console.WriteLine(string.Format("Noun: {0}, Verb: {1}", noun.ToString(), verb.ToString()));
                    Console.WriteLine(result);
                    Console.ReadLine();
                    // }
                // }
            // }
        }

        public static int calculate()
        {

            //string lines = System.IO.File.ReadAllText("day05_input.txt");
            int[] numbers = System.IO.File.ReadAllText("day05_input.txt").Split(',').Select(i => int.Parse(i)).ToArray();
            
            int addr1, addr2, addr3 = 0;
            int opcode = 0;

            int index = 0;

            

            Console.Write("Length: ");
            Console.WriteLine(numbers.Length);

            while(numbers[index] != 99){
                int number = numbers[index];
                opcode = numbers[index] % 10;

                Console.WriteLine(opcode);
                
                int paramMode1 = (opcode / 100) % 10;
                int paramMode2 = (opcode / 1000) % 10;

                int input1 = 0, input2 = 0, input3 = 0;

                addr1 = numbers[index + 1];
                input1 = (paramMode1 == 1) ? addr1 : numbers[addr1];

                if(opcode == 1 || opcode == 2){
                    addr2 = numbers[index + 2];
    
                    input2 = (paramMode2 == 2) ? addr2 : numbers[addr2];
                    input3 = numbers[index + 3];
                }

                int result = 0;
                switch(opcode){
                    case 1 :
                        result = input1 + input2;
                        Console.Write("Position 3");
                        Console.WriteLine(input3);
                        numbers[input3] = result;
                        index += 4;
                        break;
                    case 2 :
                        result = input1 * input2;
                        numbers[input3] = result;
                        index += 4;
                        break;
                    case 3 : 
                        int userInput = 1; //Console.ReadLine();
                        
                        numbers[addr1] = userInput;
                        index += 2;
                        break;
                    case 4 :
                        Console.WriteLine(numbers[addr1]);
                        index += 2;
                        break;
                }
                                
                
                if(index > numbers.Length){
                    opcode = 99;
                }
                opcode = numbers[index];
            }

            //System.IO.File.WriteAllLines("day02_output.txt",inputList);
            return numbers[0];
        }

/*
ABCDE
 1002

DE - two-digit opcode,      02 == opcode 2
 C - mode of 1st parameter,  0 == position mode
 B - mode of 2nd parameter,  1 == immediate mode
 A - mode of 3rd parameter,  0 == position mode,
                                  omitted due to being a leading zero
*/
        private static string[] parseOperator(string input){
            string[] result = new string[4];
            char[] inputNumbers = input.ToCharArray();
            int start = 0;
            if(inputNumbers.Length == 4)
            {
                result[0] = "0";
            }
            else {
                result[0] = inputNumbers[0].ToString();
                start ++;
            }
            result[1] = inputNumbers[start + 1].ToString();
            result[2] = inputNumbers[start + 2].ToString();
            result[3] = inputNumbers[start + 3].ToString() + inputNumbers[start + 4].ToString();
            
            return result;

        }
    }
}