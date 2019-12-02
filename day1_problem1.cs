using System;

namespace adventofcode2019.day01
{
    public class problem01
    {
        public static long process()
        {
            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(@"day01_input.txt");
            long fuelNeeded = 0;
        
            foreach (string line in lines)
            {
                long mass = 0;
                if (long.TryParse(line, out mass)){
                    long additionalFuel = calculateFuel(mass);

                    while(additionalFuel > 0){
                        fuelNeeded += additionalFuel;
                        additionalFuel = calculateFuel(additionalFuel);
                    }
                    
                }
                
            }

            return fuelNeeded;
        }

        private static long calculateFuel(long mass){
            long fuelNeeded = (long)(Math.Floor((double)mass/3) - 2);
            return fuelNeeded;
        }
    }
}   
    