using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExO3.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_VehiclesInGarage=new Dictionary<string, VehicleInGarage>();

        public class VehicleInGarage
        {
            public enum eVehicleFixingStatus
            {
                InTreatment = 1,
                Fixed,
                Paid
            }

            private readonly string r_OwnerName;
            private readonly string r_PhoneNumber;
            private readonly Vehicle r_Vehicle;
            private eVehicleFixingStatus m_VehicleFixingStatus;

            public VehicleInGarage(
                Vehicle i_Vehicle,
                string i_Owner,
                string i_PhoneNumber)
            {
                r_OwnerName = i_Owner;
                r_PhoneNumber = i_PhoneNumber;
                r_Vehicle = i_Vehicle;
                m_VehicleFixingStatus = eVehicleFixingStatus.InTreatment;
            }

            public Vehicle Vehicle
            {
                get { return r_Vehicle; }
            }

            public eVehicleFixingStatus FixingStatus
            {
                get { return m_VehicleFixingStatus; }
                set { m_VehicleFixingStatus = value; }
            }

            public string OwnerName
            {
                get { return r_OwnerName; }
            }

            public string PhoneNumber
            {
                get { return r_PhoneNumber; }
            }

            public override string ToString()
            {
                StringBuilder vehicleData = new StringBuilder("This is the vehicle data: ");

                vehicleData.AppendLine();
                vehicleData.AppendFormat("\tOwner Name: {0}", r_OwnerName);
                vehicleData.AppendLine();
                vehicleData.AppendFormat("\tPhone number: {0}", r_PhoneNumber);
                vehicleData.AppendLine();
                vehicleData.AppendFormat("\tFixing status: {0}", FixingStatus);
                vehicleData.AppendLine();
                vehicleData.Append("\tVehicle data: ");
                vehicleData.AppendLine();
                vehicleData.AppendFormat(r_Vehicle.ToString());

                return vehicleData.ToString();
            }

            public static Dictionary<string, object> GetDictionaryOfTheClass()
            {
                Dictionary<string, object> vehicleInGarageDictionary = new Dictionary<string, object>();

                vehicleInGarageDictionary.Add("Owner Name", typeof(string));
                vehicleInGarageDictionary.Add("Phone number", typeof(string));

                return vehicleInGarageDictionary;
            }
        }

        public  VehicleInGarage BuildTheVehicleInGarage(Vehicle i_Vehicle, Dictionary<string,object> i_Properties)
        {
            VehicleInGarage newVehicleInGarage = new VehicleInGarage(
                i_Vehicle,
                (string)i_Properties["Owner Name"],
                (string)i_Properties["Phone number"]);
            return newVehicleInGarage;
        }

        public Vehicle GetVehicle(string i_LicenseNumber)
        {
            return r_VehiclesInGarage[i_LicenseNumber].Vehicle;
        }

        public bool IsVehicleExists(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        private FuelCell getVehicleFuelCell(string i_LicenseNumber)
        {
            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("This vehicle doesn't exists in the garage.");
            }

            return GetVehicle(i_LicenseNumber).FuelCell;
        }

        public Dictionary<string, VehicleInGarage> vehiclesInGarage
        {
            get { return r_VehiclesInGarage; }
        }

        public List<string> GetWheelsPlaceByVehicleType(Vehicle i_Vehicle)
        {
            List<string> wheelsPlacesByVehicleType = new List<string>();

            if (i_Vehicle.GetType() == typeof(Car))
            {
                wheelsPlacesByVehicleType = Enum.GetValues(typeof(Car.eWheelPlace)).Cast<string>().ToList();
            }
            else if (i_Vehicle.GetType() == typeof(Motorcycle))
            {
                wheelsPlacesByVehicleType = Enum.GetValues(typeof(Motorcycle.eWheelPlace)).Cast<string>().ToList();
            }
            else if (i_Vehicle.GetType() == typeof(Track))
            {
                wheelsPlacesByVehicleType = Enum.GetValues(typeof(Track.eWheelPlace)).Cast<string>().ToList();
            }

            return wheelsPlacesByVehicleType;
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_AmountOfFuel)
        {
            FuelCell fuelCellToCharge = getVehicleFuelCell(i_LicenseNumber);

            if (!(fuelCellToCharge is ElectricFuelCell))
            {
                throw new ArgumentException("This vehicle doesn't works on battery.");
            }

            ((ElectricFuelCell)fuelCellToCharge).ChargeBattery(i_AmountOfFuel);
        }

        public void RefuelVehicle(
            string i_LicenseNumber,
            float i_AmountOfFuel,
            GasolineFuelCell.eGasolineType i_GasolineType)
        {
            FuelCell fuelCellToCharge = getVehicleFuelCell(i_LicenseNumber);

            if (!(fuelCellToCharge is GasolineFuelCell))
            {
                throw new ArgumentException("This vehicle doesn't works on battery.");
            }

            ((GasolineFuelCell)fuelCellToCharge).AddGasoline(i_AmountOfFuel, i_GasolineType);
        }

        public List<string> GetVehicleFixingStatusTypes()
        {
            return Enum.GetValues(typeof(VehicleInGarage.eVehicleFixingStatus))
                .Cast<String>().ToList();
        }

    }
}
