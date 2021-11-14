using System;
using System.Collections.Generic;

namespace secret_santa
{
    class Program
    {
        public static List<Tuple<string, string>> allCouples = new List<Tuple<string, string>>();
        public static List<Tuple<string, string>> secretSantaPairings = new List<Tuple<string, string>>();

        static void Main(string[] args)
        {
            Welcome.PrintWelcomeMessage();
            GuestEntry.ParseGuests(allCouples);
            Assignments.AssignRecipientToGuest(secretSantaPairings, allCouples);
            CreateFile.SaveResultsToFileOnDesktop(secretSantaPairings);
        }
    }
}
