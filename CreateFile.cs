using System;
using System.Collections.Generic;
using System.IO;

namespace secret_santa
{
    public static class CreateFile 
    {
        public static void SaveResultsToFileOnDesktop(List<Tuple<string, string>> secretSantaPairings)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream($"{desktopPath}/Santa.txt", FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Santa.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine(string.Format("|{0,-15}|{1,15}|", "Secret Santa ", "Gift Recipient "));
            Console.WriteLine("---------------------------------");
            foreach (var pair in secretSantaPairings)
            {
                Console.WriteLine(string.Format("|{0,-15}|{1,15}|", pair.Item1, pair.Item2));
            }
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Merry Christmas to all and to all a good night!");
        }
    }
}
