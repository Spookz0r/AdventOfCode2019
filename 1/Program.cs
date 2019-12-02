using System;


namespace _1
{
    class Program
    {
        static int calculate(int mass)
        {
            int result =  mass/3 - 2;
            return result;        
        }

        static int calculate_recurv(int mass)
        {
            int rest = 1;
            int result = 0;
            while( rest != 0)
            {
                int fuel = calculate(mass);
                if(fuel < 0){
                    break;
                }
                rest = calculate_recurv(fuel);
                return fuel + rest;
            }
            return result;        
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 1 December!");
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            int sum2 = 0;
            foreach (string line in lines)
            {
                // Part one
                int mass = Int32.Parse(line);
                sum += calculate(mass);
                // Part two
                int mass2 = Int32.Parse(line);
                sum2 += calculate_recurv(mass2);
            }
            Console.WriteLine(sum);
            Console.WriteLine(sum2);
        }
    }
}
