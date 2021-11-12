using System;
using System.Collections.Generic;
using System.Linq;

namespace secret_santa
{
    public static class Assignments
    {
        public static void AssignRecipientToGuest(List<string> assigned, List<Tuple<string, string>> secretSantaPairings, List<string> allGuests, List<Tuple<string, string>> allCouples)
        {
            foreach (var guest in allGuests)
            {
                Random random = new Random();
                var spouse = GetSpouseOfGuest(guest, allCouples);
                List<string> cleanList = RemoveInvalidGuests(assigned, guest, spouse, allGuests);
                var recipient = cleanList[random.Next(cleanList.Count)];
                secretSantaPairings.Add(Tuple.Create(guest, recipient));
                assigned.Add(recipient);
            }

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
    }
}
