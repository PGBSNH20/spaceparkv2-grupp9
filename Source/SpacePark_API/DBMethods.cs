using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SpacePark_API.Models;
using System.Collections.Generic;

namespace SpacePark_API
{
    public static class DBMethods
    {
        public static void AddParking(string name, string StarShip )
        {
            using (var db = new MyContext())
            {
                var park = new Parking{PersonName = name, StarShip = StarShip, ArrivalTime =  DateTime.Now};

                db.Parking.Add(park);
                db.SaveChanges();

            }
        }

        public static bool AlreadyParkinged(string name)
        {
            using (var db = new MyContext())
            {
                if (db.Parking.Any(p => p.PersonName == name))
                {
                    
                    var query = db.Parking
                        .Where(p => p.PersonName == name)
                        .OrderByDescending(p => p.ID)
                        .Select(p => p.Paid).First();

                    if (query)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static void PayForParkinging(string name)
        {
            using (var db = new MyContext())
            {
                var query = db.Parking
                    .Where(e => e.PersonName == name)
                    .OrderByDescending(e => e.ID)
                    .FirstOrDefault();

                DateTime departTime = DateTime.Now;
                TimeSpan timeParkinged = departTime - query.ArrivalTime;

                if (query != null)
                {
                    var pay = new Pay { DepartTime = departTime, ParkID = query.ID };
                    db.Pay.Add(pay);
                    query.Paid = true;
                    db.SaveChanges();
                    ShowReceipt(name, timeParkinged);
                }
                else
                {
                }
            }
        }

        public static void ShowReceipt(string name, TimeSpan timeParkinged)
        {
            using (var db = new MyContext())
            {
                var query = db.Parking
                    .Where( x => x.PersonName == name)
                    .OrderByDescending(x => x.ID)
                    .Join(
                    db.Pay,
                    Parking => Parking.ID,
                    pay => pay.ParkID,
                    (Parking, pay) => new
                    {
                        ID = pay.ID,
                        PersonName = Parking.PersonName,
                        StarShip = Parking.StarShip
                    }).FirstOrDefault();
                    
                double totalPrice = Math.Round(timeParkinged.TotalHours * 100, 2);

                var receipt = new Receipt { PayID = query.ID, PersonName = query.PersonName,  StarShip = query.StarShip, Price = totalPrice};
                db.Receipts.Add(receipt);
                db.SaveChanges();
            }

            
        }

        public static bool EmptySpaces(SpacePort spacePort)
        {
            using (var db = new MyContext())
            {
                var query = db.Parking
                    .Where(p => p.Paid == false && p.SpacePort.ID == spacePort.ID)
                    .Count();

                //var spacePort = db.SpacePorts.Find(id);

                if (query < spacePort.TotalCapacity)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool AlreadyPaid(int id)
        {
            using var db = new MyContext();

            var payed = db.Parking
                .Where(p => p.ID == id)
                .Select(p => p.Paid).FirstOrDefault();

            return payed;
        }
    }
}
