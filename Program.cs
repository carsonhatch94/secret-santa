using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace secret_santa
{
    class Program
    {
        public static List<string> allGuests = new List<string>();
        public static List<string> assigned = new List<string>();
        public static List<Tuple<string, string>> allCouples = new List<Tuple<string, string>>();
        public static List<Tuple<string, string>> secretSantaPairings = new List<Tuple<string, string>>();
        public static bool done = false;

        static void Main(string[] args)
        {
            PrintWelcomeMessage();
            do
            {
                ParseGuests();
            } while (!done);

            foreach (var guest in allGuests)
            {
                AssignRecipientToGuest(assigned, guest, GetSpouseOfGuest(guest));
            }

            SaveResultsToFileOnDesktop();
        }

        private static void SaveResultsToFileOnDesktop()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream($"{desktopPath}/Santa.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Santa.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine("Secret Santa: " + "Gift Recipient:");
            foreach (var pair in secretSantaPairings)
            {
                Console.WriteLine($"{pair.Item1}" + "     -->     " + $"{pair.Item2}");
            }
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }

        private static void AssignRecipientToGuest(List<string> assigned, string guest, string spouse)
        {
            Random random = new Random();
            List<string> cleanList = RemoveInvalidGuests(assigned, guest, spouse);
            var recipient = cleanList[random.Next(cleanList.Count)];
            secretSantaPairings.Add(Tuple.Create(guest, recipient));
            assigned.Add(recipient);
        }

        private static List<string> RemoveInvalidGuests(List<string> assigned, string guest, string spouse)
        {
            var cleanList = allGuests.ToList();
            cleanList.Remove(guest);
            cleanList.Remove(spouse);
            foreach (var item in assigned)
            {
                cleanList.Remove(item);
            }
            for (int i = 0; i < cleanList.Count; i++)
            {
                Console.Write($"{cleanList[i]}, ");
                if (i == cleanList.Count - 1)
                {
                    Console.Write("\n");
                }
            }

            return cleanList;
        }

        private static string GetSpouseOfGuest(string guest)
        {
            var spouse = "";
            foreach (var couple in allCouples)
            {
                if (guest == couple.Item1)
                {
                    spouse = couple.Item2;
                }

                if (guest == couple.Item2)
                {
                    spouse = couple.Item1;
                }
            }

            return spouse;
        }

        private static void ParseGuests()
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

            if (pair == "DONE")
            {
                done = true;
                return;
            }

            if (pair == "REMOVE")
            {
                RemoveEntry();
            }
            else
            {
                string[] names = pair.Split('/');
                allCouples.Add(Tuple.Create(names[0], names[1]));
                allGuests.Add(names[0]);
                allGuests.Add(names[1]);
            }
        }

        private static void RemoveEntry()
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

        private static void PrintWelcomeMessage()
        {
            Console.Write('\n');
            PrintColorfulHoHoHo();
            Console.WriteLine(", Merry Christmas!");
            Console.WriteLine("This will help you pair people up with someone who isn't their spouse for Secret Santa");
            Console.WriteLine("Be sure to format names as \n participant/spouse \n");
        }

        private static void PrintColorfulHoHoHo()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (i == 0)
                {
                    Console.Write("H");
                }
                else
                {
                    Console.Write("h");
                }

                Console.ForegroundColor = ConsoleColor.Red;
                if (i == 2)
                {
                    Console.Write("o");
                }
                else
                {
                    Console.Write("o ");
                }
            }

            Console.ResetColor();
        }
    }
}
