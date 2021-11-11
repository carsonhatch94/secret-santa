using System;
using System.Collections.Generic;
using System.IO;

namespace secret_santa
{
    class CreateFile 
    {
        public void SaveResultsToFileOnDesktop(List<Tuple<string, string>> secretSantaPairings)
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
    }
}