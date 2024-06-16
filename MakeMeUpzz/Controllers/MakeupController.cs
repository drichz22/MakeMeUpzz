using MakeMeUpzz.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Controllers
{
    public class MakeupController
    {
        protected Boolean isValidTypeID(int typeID)
        {
            List<int> IDs = MakeupHandler.getAllMakeupTypeIDs();
            for (int i = 0; i < IDs.Count; i++)
            {
                if (typeID.Equals(IDs[i])) { return true; }
            }
            return false;
        }

        protected Boolean isValidBrandID(int BrandID)
        {
            List<int> IDs = MakeupHandler.getAllMakeupBrandIDs();
            for (int i = 0; i < IDs.Count; i++)
            {
                if (BrandID.Equals(IDs[i])) { return true; }
            }
            return false;
        }

        public String InsertMakeup(String MakeupName, int MakeupPrice, int MakeupWeight,
            int MakeupTypeID, int MakeupBrandID)
        {
            String lbl = "";

            if (MakeupName.Length < 1 || MakeupName.Length > 99)
            {
                lbl = "Makeup Name length must be between 1 and 99!";
                return lbl;
            }
            else if (MakeupName == null || MakeupName.Length == 0)
            {
                lbl = "Makeup Name cannot be empty!";
                return lbl;
            }
           
            if (MakeupPrice < 1)
            {
                lbl = "Makeup Price must be greater or equal to one!";
                return lbl;
            }          

            if (MakeupWeight < 1 || MakeupWeight > 1500)
            {
                lbl = "Makeup Weight cannot exceed 1500 or less than 1!";
                return lbl;
            }            

            int? MakeupTypeIDNullable = MakeupTypeID;
            if (!isValidTypeID(MakeupTypeID) || MakeupTypeIDNullable == null)
            {
                lbl = "Makeup Type ID cannot be empty and invalid!";
                return lbl;
            }

            int? MakeupBrandIDNullable = MakeupBrandID;
            if (!isValidBrandID(MakeupBrandID) || MakeupBrandIDNullable == null)
            {
                lbl = "Makeup Brand ID cannot be empty and invalid!";
                return lbl;
            }

            int id = MakeupHandler.generateMakeupID();
            MakeupHandler.insertMakeup(id, MakeupName, MakeupPrice, MakeupWeight, MakeupTypeID, MakeupBrandID);
            lbl = "Operation Successful!";
            return lbl;
        }

        public String UpdateMakeup(int MakeupID, String MakeupName, int MakeupPrice, int MakeupWeight,
            int MakeupTypeID, int MakeupBrandID)
        {
            String lbl;

            if (MakeupName.Length < 1 || MakeupName.Length > 99)
            {
                lbl = "Makeup Name length must be between 1 and 99!";
                return lbl;
            }
            else if (MakeupName == null || MakeupName.Length == 0)
            {
                lbl = "Makeup Name cannot be empty!";
                return lbl;
            }

            if (MakeupPrice < 1)
            {
                lbl = "Makeup Price must be greater or equal to one!";
                return lbl;
            }

            if (MakeupWeight < 1 || MakeupWeight > 1500)
            {
                lbl = "Makeup Weight cannot exceed 1500 or less than 1!";
                return lbl;
            }

            int? MakeupTypeIDNullable = MakeupTypeID;
            if (!isValidTypeID(MakeupTypeID) || MakeupTypeIDNullable == null)
            {
                lbl = "Makeup Type ID cannot be empty and invalid!";
                return lbl;
            }

            int? MakeupBrandIDNullable = MakeupBrandID;
            if (!isValidBrandID(MakeupBrandID) || MakeupBrandIDNullable == null)
            {
                lbl = "Makeup Brand ID cannot be empty and invalid!";
                return lbl;
            }

            MakeupHandler.updateMakeup(MakeupID, MakeupName, MakeupPrice, MakeupWeight, MakeupTypeID, MakeupBrandID);
            lbl = "Operation Successful!";
            return lbl;
        }

        public String InsertMakeupType(String MakeupTypeName)
        {
            String lbl = "";

            if (MakeupTypeName.Length < 1 || MakeupTypeName.Length > 99)
            {
                lbl = "Makeup Type Name length must be between 1 and 99!";
                return lbl;
            }

            int id = MakeupHandler.generateMakeupTypeID();
            MakeupHandler.insertMakeupType(id, MakeupTypeName);
            lbl = "Operation Successful!";
            return lbl;
        }

        public String UpdateMakeupType(int MakeupID, String MakeupTypeName)
        {
            String lbl = "";

            if (MakeupTypeName.Length < 1 || MakeupTypeName.Length > 99)
            {
                lbl = "Makeup Type Name length must be between 1 and 99!";
                return lbl;
            }

            MakeupHandler.insertMakeupType(MakeupID, MakeupTypeName);
            lbl = "Operation Successful!";
            return lbl;
        }

        public String InsertMakeupBrand(String MakeupBrandName, int MakeupBrandRating)
        {
            String lbl = "";

            if (MakeupBrandName.Length < 1 || MakeupBrandName.Length > 99)
            {
                lbl = "Makeup Brand Name length must be between 1 and 99!";
                return lbl;
            }

            if (MakeupBrandRating < 0 || MakeupBrandRating > 100)
            {
                lbl = "Makeup Brand Rating must be between 0 and 100!";
                return lbl;
            }

            int id = MakeupHandler.generateMakeupBrandID();
            MakeupHandler.insertMakeupBrand(id, MakeupBrandName, MakeupBrandRating);
            lbl = "Operation Successful!";
            return lbl;
        }

        public String UpdateMakeupBrand(int MakeupID, String MakeupBrandName, int MakeupBrandRating)
        {
            String lbl = "";

            if (MakeupBrandName.Length < 1 || MakeupBrandName.Length > 99)
            {
                lbl = "Makeup Brand Name length must be between 1 and 99!";
                return lbl;
            }

            if (MakeupBrandRating < 0 || MakeupBrandRating > 100)
            {
                lbl = "Makeup Brand Rating must be between 0 and 100!";
                return lbl;
            }

            MakeupHandler.updateMakeupBrand(MakeupID, MakeupBrandName, MakeupBrandRating);
            lbl = "Operation Successful!";
            return lbl;            
        }
    }
}