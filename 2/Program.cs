using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; 

namespace _2
{
    
    class Program
    {
        public static int Calculate(int[] numbers)
        {
            var startIndex = 0;
            while(true)
            {
                var opcode = numbers[startIndex];
                if(opcode == 99) break;
                var val_1 = numbers[startIndex + 1];
                var val_2 = numbers[startIndex + 2];
                var pos = numbers[startIndex + 3];
                if(opcode == 1){
                    numbers[pos] = numbers[val_1] + numbers[val_2];
                    startIndex += 4;
                } else if(opcode == 2){
                    numbers[pos] = numbers[val_1] * numbers[val_2];
                    startIndex += 4;
                } else{
                    startIndex ++;
                }
            }
            return numbers[0];
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 2 December!");
            // Testing given values
            int[] numbers1 = {1,0,0,0,99};
            int[] numbers2 = {2,3,0,3,99};
            int[] numbers3 = {2,4,4,5,99,0};
            int[] numbers4 = {1,1,1,4,99,5,6,0,99};
            int result1 = Calculate(numbers1);
            int result2 = Calculate(numbers2);
            int result3 = Calculate(numbers3);
            int result4 = Calculate(numbers4);

            // Part 1: Actual test
            string line = File.ReadLines("input.txt").First();
            var numbers = line.Split(',').Select(Int32.Parse).ToList();
            numbers[1] = 12;
            numbers[2] = 2;
            int[] array = new int[numbers.Count];
            numbers.CopyTo(array);
            int result5 = Calculate(array);
            Console.WriteLine("Result Part 1 = " + result5);

            // Part 2 Brute force finding correct value :D
            var wanted_value = 19690720;
            for(int noun = 0; noun < 99; noun++)
            {
                for(int verb = 0; verb < 99; verb++)
                {
                    // Copy original input to memory
                    int[] memory = new int[numbers.Count];
                    numbers.CopyTo(memory);
                    // Update memory with noun and verb
                    memory[1] = noun;
                    memory[2] = verb;
                    // Calculate
                    int result = Calculate(memory);
                    if(result == wanted_value){
                        int sum = 100 * noun + verb;
                        Console.WriteLine("Result Part 2 = " + sum);
                        break;
                    }
                }
            }
        }
    }
}
