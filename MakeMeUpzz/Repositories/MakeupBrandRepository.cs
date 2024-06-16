using MakeMeUpzz.Factories;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Repositories
{
    public class MakeupBrandRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();
        public static List<MakeupBrand> getAllMakeupBrands()
        {
            return db.MakeupBrands.ToList();
        }

        public static List<String> getAllMakeupBrandNames() 
        { 
            return (from x in db.MakeupBrands select x.MakeupBrandName).ToList();
        }

        public static List<int> getAllMakeupBrandIDs()
        {
            return (from x in db.MakeupBrands select x.MakeupBrandID).ToList();
        }

        public static int getLastMakeupBrandID()
        {
            return (from x in db.MakeupBrands select x.MakeupBrandID).ToList().LastOrDefault();
        }

        public static void insertMakeupBrand(int MakeupBrandID, String MakeupBrandName, int MakeupBrandRating)
        {
            MakeupBrand makeupBrand = MakeupBrandFactory.Create(MakeupBrandID, MakeupBrandName, MakeupBrandRating);
            db.MakeupBrands.Add(makeupBrand);
            db.SaveChanges();
        }

        public static MakeupBrand FindMakeupBrandByID(int MakeupBrandID)
        {
            MakeupBrand makeupBrand = db.MakeupBrands.Find(MakeupBrandID);
            return makeupBrand;
        }

        public static void RemoveMakeupBrandByID(int MakeupBrandID)
        {
            MakeupBrand makeupBrand = FindMakeupBrandByID(MakeupBrandID);
            db.MakeupBrands.Remove(makeupBrand);
            db.SaveChanges();
        }

        public static void updateMakeupBrand(int MakeupBrandID, String MakeupBrandName, int MakeupBrandRating)
        {
            MakeupBrand makeupBrand = FindMakeupBrandByID(MakeupBrandID);
            makeupBrand.MakeupBrandID = MakeupBrandID;
            makeupBrand.MakeupBrandName = MakeupBrandName;
            makeupBrand.MakeupBrandRating = MakeupBrandRating;
            db.SaveChanges();
        }

        public static int getMakeupBrandIDbyName(String MakeupBrandName)
        {
            return (from x in db.MakeupBrands
                    where x.MakeupBrandName == MakeupBrandName
                    select x.MakeupBrandID).FirstOrDefault();
        }

        public static String getMakeupBrandNamebyID(int MakeupBrandID) 
        { 
            return (from x in db.MakeupBrands
                    where x.MakeupBrandID == MakeupBrandID
                    select x.MakeupBrandName).FirstOrDefault();
        }

        public static int generateMakeupBrandID()
        {
            int newID = 0;
            int lastID = getLastMakeupBrandID();
            int? lastIDLogic = lastID;
            if (lastIDLogic == null)
            {
                newID = 1;
            }
            else
            {
                newID = lastID;
                newID++;
            }
            return newID;
        }
    }
}