using System;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2019
{
    public class intcode
    {
        public int index { get; set; }
        public long output { get; set; }
        public bool halt { get; set; }
        public List<long> numbers { get; set; }

        protected int relativeBase { get; set; }
        public void initialize(string inputFile)
        {
            string lines = System.IO.File.ReadAllText(inputFile);

            numbers = lines.Split(",").Select(i => long.Parse(i)).ToList();

            relativeBase = 0;
        }
        public int process(long[] signals)
        {

            //int index = 0;
            long opcode, number, paramMode1, paramMode2;

            int signalPosition = 0;

            number = numbers[index];
            halt = false;

            while (!halt)
            {
                opcode = number % 10;

                long addr1 = 0, addr2 = 0, addr3 = 0;
                long input1 = 0, input2 = 0, result = 0;

                addr1 = numbers[index + 1];
                addr2 = numbers[index + 2];
                

                paramMode1 = (number / 100) % 10;
                paramMode2 = (number / 1000) % 10;

                switch (opcode)
                {
                    case 1:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        addr3 = numbers[index + 3];

                        input1 = getValue(addr1, paramMode1);
                        input2 = getValue(addr2, paramMode2);
                        if (opcode != 5 && opcode != 6)
                        {

                            result = handleOpCode(opcode, input1, input2);
                            index += 4;
                            writeValue(addr3, result);
                        }
                        else
                        {
                            index = (int)(shouldJump(opcode, input1) ? input2 : (index + 3));
                        }

                        break;

                    case 3:
                        input1 = getValue(addr1, paramMode1);

                        writeValue(input1, signals[signalPosition]);
                        signalPosition++;
                        index += 2;
                        break;
                    case 4:
                        input1 = getValue(addr1, paramMode1);
                        //Console.WriteLine(string.Format("Output code: {0}", numbers[addr1]));
                        output = input1;
                        index += 2;
                        return 0;
                    case 9:
                        
                        relativeBase += (int)getValue(addr1, 1);
                        index += 2;
                        break;
                }

                number = numbers[index];
                if (number == 99) halt = true;
            }

            return 1;
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

        private long getValue(long address, long paramMode)
        {
            long value = 0;

            switch (paramMode)
            {
                case 0:
                    value = address > numbers.Count ? 0 : numbers[(int)address];
                    break;
                case 1:
                    value = address;
                    break;
                case 2:
                    int newAddress = relativeBase + (int)address;
                    value = newAddress > numbers.Count ? 0 : numbers[newAddress];
                    break;
            }

            return value;
        }

        private bool writeValue(long address, long value)
        {
            if (address > numbers.Count)
            {
                for (int x = numbers.Count; x < address; x++)
                {
                    numbers.Add(0);
                }
            }

            try
            {
                numbers[(int)address] = value;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}