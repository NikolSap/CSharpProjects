using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExO3.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseType
        {
            A = 1,
            B1,
            AA,
            BB
        }

        public enum eWheelPlace
        {
            Forward = 1,
            Backward,
        }

        private const int k_NumberOfWheels = 2;
        private const float k_MaximumAirPressure = 30;
        private readonly eMotorcycleLicenseType r_LicenseType;
        private readonly int r_EngineCapacity;

        public Motorcycle():base(){}
        protected Motorcycle(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            FuelCell i_FuelCell,  //
            eMotorcycleLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(i_ManufacturerName, i_LicenseNumber, i_PercentageOfLeftEnergy, i_FuelCell)
        {
            r_LicenseType = i_LicenseType;
            r_EngineCapacity = i_EngineCapacity;
        }

        public eMotorcycleLicenseType LicenseType
        {
            get { return r_LicenseType; }
        }

        public int EngineCapacity
        {
            get { return r_EngineCapacity; }
        }

        public float MaximumAirPressure
        {
            get
            {
                return k_MaximumAirPressure;
            }
        }
        public  int NumberOfWheels
        {
            get
            { return k_NumberOfWheels; }
        }

        public override string ToString()
        {
            StringBuilder classToString = new StringBuilder(base.ToString());

            classToString.AppendLine();
            classToString.AppendFormat("\tLicense Type: {0}", r_LicenseType);
            classToString.AppendLine();
            classToString.AppendFormat("\tEngine Capacity: {0}", r_EngineCapacity);

            return classToString.ToString();
        }



        public override Dictionary<string, object> GetDictionaryOfTheClass()
        {
            Dictionary<string, object> dictionaryOfMotorcycle =
                new Dictionary<string, object>(base.GetDictionaryOfTheClass());
            dictionaryOfMotorcycle.Add("engine capacity", typeof(int));

            for (int i = 1; i <= k_NumberOfWheels; i++)
            {
                dictionaryOfMotorcycle.Add("wheel - " + i, typeof(Wheel));
            }

            return dictionaryOfMotorcycle;
        }

        public virtual Dictionary<string, Dictionary<int, string>> GetDictionaryOfTheEnums()
        {
            Dictionary<string, Dictionary<int, string>> dictionaryOfMotorcycleEnums =
                new Dictionary<string, Dictionary<int, string>>();
            Dictionary<int, string> singleEnum = Factory.EnumNamedValues<eMotorcycleLicenseType>();
            dictionaryOfMotorcycleEnums.Add("Motorcycle License Type", singleEnum);

            return dictionaryOfMotorcycleEnums;
        }

        public  List<eMotorcycleLicenseType> GetMotorcycleLicenseTypes()
        {
            return Enum.GetValues(typeof(eMotorcycleLicenseType)).Cast<eMotorcycleLicenseType>().ToList();
        }

        public  List<eWheelPlace> GetWheelPlaces()
        {
            return Enum.GetValues(typeof(eWheelPlace)).Cast<eWheelPlace>().ToList();
        }
    }
}
