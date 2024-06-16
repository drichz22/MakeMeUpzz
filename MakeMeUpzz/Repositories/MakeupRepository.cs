using MakeMeUpzz.Factories;
using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MakeMeUpzz.Repositories
{
    public class MakeupRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();

        public static List<Makeup> getAllMakeups()
        {
            return db.Makeups.ToList();
        }

        public static int getLastMakeupID()
        {
            return(from x in db.Makeups select x.MakeupID).ToList().LastOrDefault();
        }

        public static void insertMakeup(int MakeupID, String MakeupName, int MakeupPrice, int MakeupWeight,
            int MakeupTypeID, int MakeupBrandID) 
        {
            Makeup makeup = MakeupFactory.Create(MakeupID, MakeupName, MakeupPrice, MakeupWeight,
                MakeupTypeID, MakeupBrandID);
            db.Makeups.Add(makeup);
            db.SaveChanges();
        }
        
        public static void RemoveMakeupByID(int MakeupID)
        {
            Makeup makeup = db.Makeups.Find(MakeupID);
            db.Makeups.Remove(makeup);
            db.SaveChanges();
        }

        public static Makeup FindMakeupByID(int MakeupID)
        {
            Makeup makeup = db.Makeups.Find(MakeupID);
            return makeup;
        }


        public static void updateMakeup(int MakeupID, String MakeupName, int MakeupPrice, int MakeupWeight
            , int MakeupTypeID, int MakeupBrandID)
        {
            Makeup updateMakeup = FindMakeupByID(MakeupID);
            updateMakeup.MakeupName = MakeupName;
            updateMakeup.MakeupPrice = MakeupPrice;
            updateMakeup.MakeupWeight = MakeupWeight;
            updateMakeup.MakeupTypeID = MakeupTypeID;
            updateMakeup.MakeupBrandID = MakeupBrandID;
            db.SaveChanges();
        }

        public static int getMakeupID(String MakeupName, int MakeupPrice, int MakeupWeight, int MakeupTypeID,
            int MakeupBrandID) 
        { 
            return (from x in db.Makeups where x.MakeupName == MakeupName && 
                    x.MakeupPrice == MakeupPrice && x.MakeupWeight == MakeupWeight &&
                    x.MakeupTypeID == MakeupTypeID && x.MakeupBrandID == MakeupBrandID select x.MakeupID).FirstOrDefault();
        }

        public static Makeup getMakeupByID(int MakeupID)
        {
            return (from x in db.Makeups where x.MakeupID == MakeupID select x).FirstOrDefault();
        }

        public static int generateMakeupID()
        {
            int newID = 0;
            int lastID = getLastMakeupID();
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