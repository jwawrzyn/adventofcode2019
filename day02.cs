using System;

namespace adventofcode.day02
{
    public class Day02
    {
        public static void process(){
            for(int noun = 0; noun < 100;  noun ++){
                for(int verb = 0; verb < 100; verb ++){
                    string result = calculate(noun, verb);
                    int output = 0;
                    int.TryParse(result, out output);

                    if(output == 19690720){
                        Console.WriteLine(string.Format("Noun: {0}, Verb: {1}", noun.ToString(), verb.ToString()));
                        Console.WriteLine((100 * noun + verb).ToString());
                    }
                }
            }
        }

        public static string calculate(int noun, int verb)
        {

            string lines = System.IO.File.ReadAllText("day02_input.txt");

            string[] inputList = lines.Split(",");

            int position = 0;
            string op = "1";
            op = inputList[position];
            inputList[1] = noun.ToString();
            inputList[2] = verb.ToString();
            while(op != "99"){
                int inputOne = 0;
                int inputTwo = 0;
                
                int positionOne = 0;
                int positionTwo = 0;
                int positionThree = 0;

                int.TryParse(inputList[position + 1], out positionOne);
                int.TryParse(inputList[position + 2], out positionTwo);
                int.TryParse(inputList[position + 3], out positionThree);

                int.TryParse(inputList[positionOne], out inputOne);
                int.TryParse(inputList[positionTwo], out inputTwo);
                //int.TryParse(inputList[positionThree], out inputThree);
                int result = 0;
                if(op == "1"){
                    result = inputOne + inputTwo;
                }
                if (op == "2"){
                    result = inputOne * inputTwo;
                }
                if(op == "1" || op == "2"){
                    inputList[positionThree] = result.ToString();
                }
                
                position += 4;
                if(position > inputList.Length){
                    op = "99";
                }
                op = inputList[position];
            }

            System.IO.File.WriteAllLines("day02_output.txt",inputList);
            return inputList[0];
        }
    }
}