using System;
using System.Collections.Generic;
using System.Linq;

namespace secret_santa
{
    public static class Assignments
    {
        public static List<Tuple<string, string>> AssignRecipientToGuest(List<Tuple<string, string>> allCouples)
        {
            var secretSantaPairings = new List<Tuple<string, string>>();
            var allGuests = GetAllGuests(allCouples);
            var assigned = new List<string>();
            foreach (var guest in allGuests)
            {
                Random random = new Random();
                var spouse = GetSpouseOfGuest(guest, allCouples);
                List<string> cleanList = RemoveInvalidGuests(assigned, guest, spouse, allGuests);
                if (cleanList.Count != 0)
                {
                    var recipient = cleanList[random.Next(cleanList.Count)];
                    secretSantaPairings.Add(Tuple.Create(guest, recipient));
                    assigned.Add(recipient);
                }

                if (cleanList.Count == 0)
                {
                    // these two variables might need to be changed around so that we return secretSantaPairings
                    AssignRecipientToGuest(allCouples);
                }

            }

            return secretSantaPairings;

        }

        private static List<string> RemoveInvalidGuests(List<string> assigned, string guest, string spouse, List<string> allGuests)
        {
            var cleanList = allGuests.ToList();
            cleanList.Remove(guest);
            cleanList.Remove(spouse);
            foreach (var item in assigned)
            {
                cleanList.Remove(item);
            }

            return cleanList;
        }

        private static string GetSpouseOfGuest(string guest, List<Tuple<string, string>> allCouples)
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

        private static List<string> GetAllGuests(List<Tuple<string, string>> allCouples)
        {
            var allGuests = new List<string>();
            foreach (var couple in allCouples)
            {
                allGuests.Add(couple.Item1);
                allGuests.Add(couple.Item2);
            }

            return allGuests;
        }
    }
}
