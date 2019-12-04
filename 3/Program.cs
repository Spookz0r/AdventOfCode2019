using System;
using System.Collections.Generic;
using System.Linq; 
using System.IO;

namespace _3
{

    // public class Wire
    // {
    //     public List<int,int> coordinates;
    // }

    class Program
    {
        static void AddEntry(ref Dictionary<string, int> dict, int x_val, int y_val, ref int step_counter, int nr_of_steps, int x_increment, int y_increment)
        {
            int curr_x = x_val;
            int curr_y = y_val;
            for(int i = 1; i <= nr_of_steps; i++)
            {
                step_counter++;
                curr_x += x_increment;
                curr_y += y_increment;
                string x = curr_x.ToString();
                string y = curr_y.ToString();
                string key = x + "," + y;
                if(!dict.ContainsKey(key))
                {
                    dict.Add(key, step_counter);
                }
            }
        }
        static void Path(ref Dictionary<string, int> wire, string commands)
        {
            int curr_x = 0;
            int curr_y = 0;
            int step_counter = 0;
            var command_list = commands.Split(',').ToList();
            foreach (string command in command_list)
            {
                var command_value_str = command.Substring(1);
                int command_value = 0;
                Int32.TryParse(command_value_str, out command_value);
                
                switch(command[0])
                {
                    case 'R':
                        AddEntry(ref wire, curr_x, curr_y, ref step_counter, command_value , 1, 0);
                        curr_x += command_value;
                        break;
                    case 'L':
                            AddEntry(ref wire, curr_x, curr_y, ref step_counter, command_value, -1, 0 );
                        curr_x -= command_value;
                        break;
                    case 'U':
                            AddEntry(ref wire, curr_x, curr_y, ref step_counter, command_value, 0, 1);
                        curr_y += command_value;
                    break;
                    case 'D':
                            AddEntry(ref wire, curr_x, curr_y, ref step_counter, command_value, 0, -1);
                        curr_y -= command_value;
                        break;
                    default:
                        Console.WriteLine("Weird command");
                        break;
                }

            }
        }

        static void FindShortestDistance(ref Dictionary<string, int> wire_1, ref Dictionary<string, int> wire_2)
        {
            int shortest_distance = int.MaxValue;
            int shortest_step_sum = int.MaxValue;
            foreach(KeyValuePair<string, int> entry in wire_1)
            {
                if(wire_2.ContainsKey(entry.Key))
                {
                    if(entry.Key != "0,0")
                    {
                        var coordinates = entry.Key.Split(',').Select(Int32.Parse).ToList();
                        int distance = Math.Abs(coordinates[0]) + Math.Abs(coordinates[1]);
                        if(distance < shortest_distance){ shortest_distance = distance;}

                        int step_sum = entry.Value + wire_2[entry.Key];
                        if(step_sum < shortest_step_sum)
                        {
                            shortest_step_sum = step_sum;
                        }
                    }
                }
            }
            Console.WriteLine("Shortest distance: " + shortest_distance);
            Console.WriteLine("Shortest Step sum: " + shortest_step_sum);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 3 December!");

            // Test values
            Dictionary<string, int> wire_1 = new Dictionary<string, int>();
            Dictionary<string, int> wire_2 = new Dictionary<string, int>();
            Dictionary<string, int> wire_3 = new Dictionary<string, int>();
            Dictionary<string, int> wire_4 = new Dictionary<string, int>();

            string test_1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            string test_2 = "U62,R66,U55,R34,D71,R55,D58,R83";
            string test_3 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            string test_4 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            Path(ref wire_1, test_1);
            Path(ref wire_2, test_2);
            Path(ref wire_3, test_3);
            Path(ref wire_4, test_4);
            FindShortestDistance(ref wire_1, ref wire_2); //Distance should be 159, steps should be 610
            FindShortestDistance(ref wire_3, ref wire_4); //Distance should be 135, steps should be 410

            // Actual Values

            // Wires
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            var wire_commands = new List<String>();
            foreach (string line in lines)
            {
                wire_commands.Add(line);
            }

            Dictionary<string, int> wire_5 = new Dictionary<string, int>();
            Dictionary<string, int> wire_6 = new Dictionary<string, int>();

            Path(ref wire_5, wire_commands[0]);
            Path(ref wire_6, wire_commands[1]);
            FindShortestDistance(ref wire_5, ref wire_6); //Distance should be 5319, steps should be 122514
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Elapsed time ms: " + elapsedMs);
        }
    }
}
