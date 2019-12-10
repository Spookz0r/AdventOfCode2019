using System;
using System.Collections.Generic;
using System.Linq; 
using System.IO;

namespace _6
{
    public class Orbit
    {
        public string name {get;set;}
        public string parent {get;set;}
        public Orbit parent_orbit {get;set;}
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code: 6 December!");

            string[] lines = System.IO.File.ReadAllLines("input.txt");
            Dictionary<string, Orbit> orbits = new Dictionary<string, Orbit>();
            foreach (string line in lines)
            {
                String[] strlist = line.Split(")",StringSplitOptions.RemoveEmptyEntries);
                Orbit temp = new Orbit{};
                temp.name = strlist[1];
                temp.parent = strlist[0];
                orbits.Add(strlist[1],temp);
            }
            // Add parent orbit to each Orbit, There is no COM orbit. Orbit Map
            foreach(KeyValuePair<string, Orbit> entry in orbits)
            {
                if(entry.Value.parent != "COM") entry.Value.parent_orbit = orbits[entry.Value.parent];
            }

            Console.WriteLine("Answer Part 1:  Sum = " + GetAllDirectAndIndirectOrbits(orbits));
            Console.WriteLine("Answer Part 2:  Steps " + ShortestPathFromYouToSAN(orbits));
        }

        static int GetAllDirectAndIndirectOrbits(Dictionary<string, Orbit> orbits)
        {
            // Go through each object and go all the way to COM
            int sum = 0;
            foreach(KeyValuePair<string, Orbit> entry in orbits)
            {
                sum += CalcOrbits(entry.Value);
            }
            return sum;
        }
        static int CalcOrbits(Orbit obj)
        {
            if(obj.parent == "COM") return 1;
            return 1 + CalcOrbits(obj.parent_orbit);
        }

        static int ShortestPathFromYouToSAN(Dictionary<string,Orbit> orbits)
        {
            // First check SAN's parent, check if any of YOU's parents are SAN's parent, if not, step up SAN's parent once more
            Orbit SANs_Parent = orbits["SAN"].parent_orbit;
            int SAN_counter = 0;
            int count_to_shared_parent = 0;
            for(int i = 1; i<9999; i++)
            {
                count_to_shared_parent = 0;  // Counter sent as a reference so value is updated when returned
                string shared_parent = FindParent(orbits["YOU"], SANs_Parent.name, ref count_to_shared_parent);
                if(shared_parent.Length != 0) break;
                SANs_Parent = SANs_Parent.parent_orbit;
                SAN_counter++;
            }
            return count_to_shared_parent+SAN_counter;  //Short path is number of steps to common parent
        }
        static string FindParent(Orbit obj, string wanted_parent,ref int count_to_shared_parent)
        {
            if(obj.parent == wanted_parent) return obj.parent;
            if(obj.parent == "COM") return "";

            count_to_shared_parent++;
            return FindParent(obj.parent_orbit, wanted_parent, ref count_to_shared_parent);
        }
    }

}
