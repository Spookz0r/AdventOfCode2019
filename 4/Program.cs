using System;
using System.Collections.Generic;
using System.Linq; 
using System.IO;

namespace _4
{
    class Program
    {
        static Boolean ContainsTwoAdjacentDigits(int value)
        {
            string value_string = value.ToString();
            for(int i = 0; i < 5; i++)
            {
                if(value_string[i] == value_string[i+1])
                {
                    return true;
                }
            }
            return false;
        }

        static Boolean CheckIncrementingValue(int value)
        {
            string value_string = value.ToString();
            for(int i = 0; i < 5; i++)
            {
                int val = (int)Char.GetNumericValue(value_string[i]);
                int next_val = (int)Char.GetNumericValue(value_string[i+1]);
                if(val > next_val)
                {
                    return false;
                }
            }
            return true;
        }

        static Boolean ContainsTwoAdjacentDigits_2(int value)
        {
            Boolean doubleFound = false;
            string value_string = value.ToString();
            for(int i = 0; i < 5; i++)
            {
                if(value_string[i] == value_string[i+1])  //Two adjacent values, check next
                {
                    i++;
                    doubleFound = true;
                    if( i < 5){
                        if(value_string[i] == value_string[i+1]) //There are three adjacent values, check next
                        {
                            doubleFound = false;
                            i++;
                            if( i < 5)
                            {
                                if(value_string[i] == value_string[i+1]) // there are four adjacent values,
                                {
                                    i++;
                                    if(i < 5)
                                    {
                                        if(value_string[i] == value_string[i+1]) //Now theres no room for a double, return false
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if(doubleFound)
                {
                    return true;
                }
            }
            return false;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 4 December!");

            // Pussle input = 234208-765869
            int min_value = 234208;
            int max_value = 765869;
            int password_counter = 0;
            int password_counter_part_2 = 0;
            for(int i = min_value; i < 765869; i++)
            {
                Boolean twoAdjacent = ContainsTwoAdjacentDigits(i);
                Boolean incrementing = CheckIncrementingValue(i);
                if(twoAdjacent && incrementing)
                {
                    password_counter++;
                }
                // part 2
                Boolean twoAdjacent_part_2 = ContainsTwoAdjacentDigits_2(i);
                if(incrementing && twoAdjacent_part_2)
                {
                    password_counter_part_2++;
                }
            }
            // testing
            Boolean twoadj = ContainsTwoAdjacentDigits_2(112233);
            Console.WriteLine(twoadj);
            twoadj = ContainsTwoAdjacentDigits_2(123444);
            Console.WriteLine(twoadj);
            twoadj = ContainsTwoAdjacentDigits_2(111122);
            Console.WriteLine(twoadj);
            twoadj = ContainsTwoAdjacentDigits_2(113456);
            Console.WriteLine(twoadj);
            twoadj = ContainsTwoAdjacentDigits_2(223333);
            Console.WriteLine(twoadj);

            
            Console.WriteLine("Number of passwords: " + password_counter);
            Console.WriteLine("Number of passwords Part 2: " + password_counter_part_2);
        }
    }
}
