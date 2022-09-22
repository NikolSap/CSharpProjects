using System;
using System.Collections.Generic;
using System.Linq;

namespace ExO3.GarageLogic
{
    public static class Factory
    {
        public enum eVehicleTypes
        {
            ElectricMotorcycle = 1,
            GasolineMotorcycle,
            ElectricCar,
            GasolineCar,
            GasolineTrack
        }

        public enum eColor
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        public static List<eVehicleTypes> GetVehicleTypes()
        {
            return Enum.GetValues(typeof(Factory.eVehicleTypes)).Cast<Factory.eVehicleTypes>().ToList();
        }

        public static Dictionary<int, string> EnumNamedValues<T>() where T : Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }

        public static Dictionary<int, string> GetVehicleTypesDictionary()
        {
            return EnumNamedValues<eVehicleTypes>();
        }

        public static Dictionary<string, object> GetClassDictionary(int i_UserChoice)
        {
            Dictionary<string, object> dictionaryOfWantedClass = new Dictionary<string, object>();
            switch (i_UserChoice)
            {
                case 1:
                    ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle();
                    dictionaryOfWantedClass= electricMotorcycle.GetDictionaryOfTheClass();
                    break;
                case 2:
                    GasolineMotorcycle gasolineMotorcycle = new GasolineMotorcycle();
                    dictionaryOfWantedClass= gasolineMotorcycle.GetDictionaryOfTheClass();
                    break;
                case 3:
                    ElectricCar electricCar = new ElectricCar();
                    dictionaryOfWantedClass = electricCar.GetDictionaryOfTheClass();
                    break;
                case 4:
                    GasolineCar gasolineCar = new GasolineCar();
                    dictionaryOfWantedClass = gasolineCar.GetDictionaryOfTheClass();
                    break;
                case 5:
                    GasolineTrack gasolineTrack = new GasolineTrack();
                    dictionaryOfWantedClass = gasolineTrack.GetDictionaryOfTheClass();
                    break;
            }

            return dictionaryOfWantedClass;
        }

        public static Dictionary<string, Dictionary<int, string>> GetEnumDictionary(int i_UserChoice)
        {
            Dictionary<string, Dictionary<int, string>> dictionaryOfWantedEnum = new Dictionary<string, Dictionary<int, string>>();

            switch (i_UserChoice)
            {
                case 1:
                    ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle();
                    dictionaryOfWantedEnum = electricMotorcycle.GetDictionaryOfTheEnums();
                    break;
                case 2:
                    GasolineMotorcycle gasolineMotorcycle = new GasolineMotorcycle();
                    dictionaryOfWantedEnum = gasolineMotorcycle.GetDictionaryOfTheEnums();
                    break;
                case 3:
                    ElectricCar electricCar = new ElectricCar();
                    dictionaryOfWantedEnum = electricCar.GetDictionaryOfTheEnums();
                    break;
                case 4:
                    GasolineCar gasolineCar = new GasolineCar();
                    dictionaryOfWantedEnum = gasolineCar.GetDictionaryOfTheEnums();
                    break;
            }

            return dictionaryOfWantedEnum;
        }

        public static Vehicle BuildTheVehicle(int i_UserChoice, Dictionary<string, object> i_MembersData
                                              , Dictionary<string, int> i_EnumsData)
        {
            Vehicle vehicle;

            switch (i_UserChoice)
            {
                case 1:
                    vehicle = new ElectricMotorcycle(i_MembersData, i_EnumsData);
                    break;
                case 2:
                    vehicle = new GasolineMotorcycle(i_MembersData, i_EnumsData);
                    break;
                case 3:
                    vehicle = new ElectricCar(i_MembersData, i_EnumsData);
                    break;
                case 4:
                    vehicle = new GasolineCar(i_MembersData, i_EnumsData);
                    break;
                case 5:
                    vehicle = new GasolineTrack(i_MembersData);
                    break;
                default:
                    throw new ArgumentException("Not existing type.");
            }

            return vehicle;
        }
    }
}
