using System;
using System.Collections.Generic;

namespace secret_santa
{
    class Program
    {
        public static List<Tuple<string, string>> allCouples = new List<Tuple<string, string>>();

        static void Main(string[] args)
        {
            Welcome.PrintWelcomeMessage();
            GuestEntry.ParseGuests(allCouples);
            var secretSantaPairings = Assignments.AssignRecipientToGuest(allCouples);
            CreateFile.SaveResultsToFileOnDesktop(secretSantaPairings);
        }
    }
}
