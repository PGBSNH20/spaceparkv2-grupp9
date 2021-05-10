using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;

namespace SpacePark_API
{
    public static class DBMethods
    {
        public static async Task<bool> EmptySpaces(int spacePortID, MyContext context)
        {
            var query = context.Parking
                .Where(p => p.Paid == false && p.SpacePort.ID == spacePortID)
                .Count();

            var spacePort = await context.SpacePorts.FindAsync(spacePortID);

            if (query < spacePort.TotalCapacity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool AlreadyPaid(int id, MyContext context)
        {
            var payed = context.Parking
                .Where(p => p.ID == id)
                .Select(p => p.Paid).FirstOrDefault();

            return payed;
        }
    }
}
