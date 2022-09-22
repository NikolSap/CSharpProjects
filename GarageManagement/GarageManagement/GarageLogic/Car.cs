using System.Collections.Generic;
using System.Text;

namespace ExO3.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eNumberOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public enum eWheelPlace
        {
            LeftForward = 1,
            LeftBackward,
            RightForward,
            RightBackward
        }

        private const float k_MaximumAirPressure = 30;
        private const int k_NumberOfWheels = 4;
        private readonly Factory.eColor r_Color;
        private readonly eNumberOfDoors r_NumberOfDoors;

        public Car():base(){}
        protected Car(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            FuelCell i_FuelCell,
            Factory.eColor i_Color,
            eNumberOfDoors i_NumberOfDoors)
            : base(i_ManufacturerName, i_LicenseNumber, i_PercentageOfLeftEnergy, i_FuelCell)
        {
            r_Color = i_Color;
            r_NumberOfDoors = i_NumberOfDoors;
        }

        public  int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public  float MaximumAirPressure
        {
            get { return k_MaximumAirPressure; }
        }

        public Factory.eColor Color
        {
            get { return r_Color; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return r_NumberOfDoors; }
        }

        public override string ToString()
        {
            StringBuilder classToString = new StringBuilder(base.ToString());

            classToString.AppendLine();
            classToString.AppendFormat("\tCar Color: {0}", r_Color);
            classToString.AppendLine();
            classToString.AppendFormat("\tNumber Of Doors: {0}", r_NumberOfDoors);

            return classToString.ToString();
        }

        public override Dictionary<string, object> GetDictionaryOfTheClass()
        {
            Dictionary<string, object> dictionaryOfCar =
                new Dictionary<string, object>(base.GetDictionaryOfTheClass());

            for (int i = 1; i <= k_NumberOfWheels; i++)
            {
                dictionaryOfCar.Add("wheel - " + i, typeof(Wheel));
            }

            return dictionaryOfCar;
        }

        public virtual Dictionary<string, Dictionary<int, string>> GetDictionaryOfTheEnums()
        {
            Dictionary<string, Dictionary<int, string>> dictionaryOfCarEnums =
                new Dictionary<string, Dictionary<int, string>>();

            Dictionary<int, string> singleEnum = Factory.EnumNamedValues<eNumberOfDoors>();
            dictionaryOfCarEnums.Add("Number Of Doors", singleEnum);
            singleEnum = Factory.EnumNamedValues<Factory.eColor>();
            dictionaryOfCarEnums.Add("Color", singleEnum);

            return dictionaryOfCarEnums;
        }
    }
}

