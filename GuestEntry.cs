using System;
using System.Collections.Generic;

namespace secret_santa
{
    public static class GuestEntry
    {
        public static void ParseGuests(List<Tuple<string, string>> allCouples, List<string> allGuests)
        {
            if (allCouples.Count > 0)
            {
                Console.Write("\n");
                for (int i = 0; i < allCouples.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {allCouples[i].Item1}/{allCouples[i].Item2}");
                }

                Console.Write("\n");
            }
            Console.WriteLine("If you are done, type DONE");
            Console.WriteLine("If you want to remove an entry, type REMOVE");
            Console.Write("Enter name of partcipant and spouse: ");
            string pair = "";
            pair = PreventEmptyEntries();

            if (pair == "DONE")
            {
                return;
            }

            if (pair != "REMOVE")
            {
                string[] names = pair.Split('/');
                Console.WriteLine(names.Length);
                allCouples.Add(Tuple.Create(names[0], names[1]));
                allGuests.Add(names[0]);
                allGuests.Add(names[1]);
                ParseGuests(allCouples, allGuests);
            }

            if (pair == "REMOVE")
            {
                RemoveEntry(allCouples);
            }

        }

        private static string PreventEmptyEntries()
        {
            string pair;
            do
            {
                pair = Console.ReadLine();
                if (string.IsNullOrEmpty(pair))
                {
                    Console.WriteLine("Empty input, please try again");
                    Console.WriteLine("If you are done, type DONE");
                    Console.Write("Enter name of partcipant and spouse: ");
                }
            } while (string.IsNullOrEmpty(pair));
            return pair;
        }

        private static void RemoveEntry(List<Tuple<string, string>> allCouples)
        {
            Console.WriteLine("Enter the number of the entry you want to remove: ");
            var entry = "";
            bool success;
            int num;

            do
            {
                entry = Console.ReadLine();
                success = int.TryParse(entry, out num);
                if (string.IsNullOrEmpty(entry))
                {
                    Console.WriteLine("Empty input, please try again");
                    Console.WriteLine("Enter the number of the entry you want to remove: ");
                }
                else if (!success)
                {
                    Console.WriteLine("Input is not a number, please try again");
                    Console.WriteLine("Enter the number of the entry you want to remove: ");
                }
                else if (num > allCouples.Count)
                {
                    Console.WriteLine("Input too large, please try again");
                    Console.WriteLine("Enter the number of the entry you want to remove: ");
                }
            } while (string.IsNullOrEmpty(entry) || success == false || num > allCouples.Count);

            allCouples.RemoveAt(num - 1);
        }
    }
}
