using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; 

namespace _5
{
    
    class Program
    {
        public static int GetValueFromMode(int[] array, int mode, int index)
        {
            int value = 0;
            if(mode == 0) //parameter_threeition mode
            {
                value = array[ array[index] ];
            } else if(mode == 1)  //immediate mode
            {
                value = array[index];
            }
            return value;
        }
        public static int Calculate(int[] numbers)
        {
            var startIndex = 0;
            while(true)
            {
                
                int opcode = numbers[startIndex];
                // Console.WriteLine("Opcode:" + opcode);
                if(opcode == 99) break;
                int parameter_one = 0;
                int parameter_two = 0;
                int parameter_three = 0;
                var pos = 0;


                // Mode check
                string opcode_string = opcode.ToString();
                int mode_first_parameter  = 0;
                int mode_second_parameter = 0;
                int mode_third_parameter  = 0;

                // Try to get Opcodes and modes for each parameter. if index does not exist the try/catch will handle it :D
                try
                {
                    opcode = (int)Char.GetNumericValue(opcode_string[opcode_string.Length - 1]);
                    opcode = System.Convert.ToInt32(opcode_string[opcode_string.Length - 2].ToString() + opcode.ToString());
                    mode_first_parameter  = (int)Char.GetNumericValue(opcode_string[opcode_string.Length - 3]);
                    mode_second_parameter = (int)Char.GetNumericValue(opcode_string[opcode_string.Length - 4]);
                    mode_third_parameter  = (int)Char.GetNumericValue(opcode_string[opcode_string.Length - 5]);
                } catch{}

                try
                {
                    parameter_one = GetValueFromMode(numbers, mode_first_parameter, startIndex+1);
                    parameter_two = GetValueFromMode(numbers, mode_second_parameter, startIndex+2);
                } catch{}
            
                switch(opcode)
                {
                    case 1:
                        parameter_three = numbers[startIndex + 3];
                        numbers[parameter_three] = parameter_one + parameter_two;
                        startIndex += 4;
                        break;
                    case 2:
                        parameter_three = numbers[startIndex + 3];
                        numbers[parameter_three] = parameter_one * parameter_two;
                        startIndex += 4;
                        break;
                    case 3: //Takes a single integer as input and saves it to the position given by its only parameter
                        pos = numbers[startIndex+1];
                        Console.Write("Opcode 3: Type input:  ");
                        int x = Int32.Parse(Console.ReadLine());
                        numbers[pos] = x;
                        startIndex += 2;
                        break;
                    case 4: // Outputs the value of its only parameter. Output value at address
                        pos = numbers[startIndex+1];
                        var output_value = numbers[pos];
                        Console.WriteLine("Opcode 4: Output = " + output_value);
                        if(output_value != 0)  // Indicates a fail, print out the 10 previous instructions
                        {
                            var newArray = numbers.Skip(startIndex-10).Take(10).ToArray();
                            // Console.WriteLine("[{0}]", string.Join(", ", newArray));
                        }
                        startIndex += 2;
                        break;
                    case 5: // Jump if true: If the first paramter is non-zero, it sets the instructions pointer to the value
                            // from the second paramter. Otherwise it does nothing
                        startIndex = parameter_one != 0 ? parameter_two : startIndex + 2;
                        break;
                    case 6: // Jump if false: If the first paramter is zero, it sets the instructions pointer to the value
                            // from the second paramter. Otherwise it does nothing
                        startIndex = parameter_one == 0 ? parameter_two : startIndex + 2;
                        break;
                    case 7: // Less than: If the first paramter is less than the second parameter, it stores 1 in the
                            // position given by the third parameter. Otherwise it stores 0
                        parameter_three = numbers[startIndex + 3];
                        numbers[parameter_three] = parameter_one < parameter_two ? 1 : 0;
                        startIndex +=4;
                        break;
                    case 8: // Equals: If the first paramter is equal to the second parameter, it stores 1 in the
                            // position given by the third parameter. Otherwise it stores 0
                        parameter_three = numbers[startIndex + 3];
                        numbers[parameter_three] = parameter_one == parameter_two ? 1 : 0;
                        startIndex +=4;
                        break;
                    default:
                        startIndex++;
                        break;
                }
                Console.WriteLine("Startindex: "  + startIndex + " opcode: " + opcode + "  [{0}]", string.Join(", ", numbers));
            }
            Console.WriteLine("[{0}]", string.Join(", ", numbers));
            return numbers[0];
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 5 December!");

            
            // int[] numbers1 = {1,0,0,0,99};
            // int[] numbers2 = {2,3,0,3,99};
            // int[] numbers3 = {2,4,4,5,99,0};
            // int[] numbers4 = {1,1,1,4,99,5,6,0,99};
            // int result1 = Calculate(numbers1);
            // int result2 = Calculate(numbers2);
            // int result3 = Calculate(numbers3);
            // int result4 = Calculate(numbers4);

            // Part 1: Actual test
            // string line = File.ReadLines("input.txt").First();
            // var numbers = line.Split(',').Select(Int32.Parse).ToList();
            // int[] array = new int[numbers.Count];
            // numbers.CopyTo(array);
            // int result5 = Calculate(array);

            // More test

            // int[] array_2 = {1002,4,3,4,33};
            // int result_test_1 = Calculate(array_2);
            // int[] array_3 = {1101,100,-1,4,0};
            // result_test_1 = Calculate(array_3);
            // int[] array_4 = {3,9,8,9,10,9,4,9,99,-1,8};  // IF input is 8 output is 1, 0 if not
            // int[] array_4 = {3,9,7,9,10,9,4,9,99,-1,8};  // If input is less than 8 output is 1, 0 if not
            // int[] array_4 = {3,3,1108,-1,8,3,4,3,99};  // IF input is 8 output is 1, 0 if not
            // int[] array_4 = {3,3,1107,-1,8,3,4,3,99};  // if input is less than 8 output is 1, 0 if not
            // int[] array_4 = {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9}; // If
            int[] array_4 = {3,3,1105,-1,9,1101,0,0,12,4,12,99,1}; // If
            int result_test_1 = Calculate(array_4);
            // int[] array_5 = {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};
            // result_test_1 = Calculate(array_5);
            // int[] array_6 = {3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31, 1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99};
            // result_test_1 = Calculate(array_6);

        }
    }
}
