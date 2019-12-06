using System;

namespace adventofcode.day05
{
    public class problem01
    {
        public static void Run(){
            // for(int noun = 0; noun < 100;  noun ++){
                // for(int verb = 0; verb < 100; verb ++){
                    string result = calculate();
                    int output = 0;
                    int.TryParse(result, out output);

                    // if(output == 19690720){
                    //     Console.WriteLine(string.Format("Noun: {0}, Verb: {1}", noun.ToString(), verb.ToString()));
            Console.WriteLine(result);
                    // }
                // }
            // }
        }

        public static string calculate()
        {

            string lines = System.IO.File.ReadAllText("day05_input.txt");

            string[] inputList = lines.Split(",");

            int position = 0;
            string op = "1";
            op = inputList[position];

            

            while(op != "99"){
                int inputOne = 0;
                int inputTwo = 0;
                
                int positionOne = 0;
                int positionTwo = 0;
                int positionThree = 0;
                
                string[] operatorType;

                if(op.Length > 2){
                    operatorType = parseOperator(op);
                }

                int.TryParse(inputList[position + 1], out positionOne);
                int.TryParse(inputList[position + 2], out positionTwo);
                int.TryParse(inputList[position + 3], out positionThree);

                int.TryParse(inputList[positionOne], out inputOne);
                int.TryParse(inputList[positionTwo], out inputTwo);
                //int.TryParse(inputList[positionThree], out inputThree);
                int result = 0;
                switch(op){
                    case "1" :
                        result = inputOne + inputTwo;
                        inputList[positionThree] = result.ToString();
                        position += 4;
                        break;
                    case "2" :
                        result = inputOne * inputTwo;
                        inputList[positionThree] = result.ToString();
                        position += 4;
                        break;
                    case "3" : 
                        string userInput = Console.ReadLine();
                        
                        inputList[positionOne] = userInput;
                        position += 2;
                        break;
                    case "4" :
                        Console.WriteLine(inputList[positionOne]);
                        position += 2;
                        break;
                }
                                
                
                if(position > inputList.Length){
                    op = "99";
                }
                op = inputList[position];
            }

            System.IO.File.WriteAllLines("day02_output.txt",inputList);
            return inputList[0];
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