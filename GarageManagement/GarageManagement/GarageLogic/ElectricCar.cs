using System.Collections.Generic;

namespace ExO3.GarageLogic
{
    class ElectricCar : Car
    {
        private const float k_MaxBatteryCapacityInHours = 3.2f;

        public ElectricCar():base(){}
        public ElectricCar(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            Factory.eColor i_Color,
            eNumberOfDoors i_NumberOfDoors)
            : base(
                i_ManufacturerName,
                i_LicenseNumber,
                i_PercentageOfLeftEnergy,
                new ElectricFuelCell(
                    k_MaxBatteryCapacityInHours,
                    k_MaxBatteryCapacityInHours * i_PercentageOfLeftEnergy),
                i_Color,
                i_NumberOfDoors)
        {
        }

        public ElectricCar(Dictionary<string, object> i_DataMembers, Dictionary<string, int> i_EnumMembers)
            : base(
                (string)i_DataMembers["manufacture name"],
                (string)i_DataMembers["license number"],
                (float)i_DataMembers["percentage of left energy"],
                new ElectricFuelCell(
                    k_MaxBatteryCapacityInHours,
                    k_MaxBatteryCapacityInHours * (float)i_DataMembers["percentage of left energy"]),
                (Factory.eColor)i_EnumMembers["Color"],
                (eNumberOfDoors)i_EnumMembers["Number Of Doors"])
        {
            for (int i = 1; i <= NumberOfWheels ; i++)
            {
                Wheel wheel = new Wheel((List<object>)i_DataMembers["wheel - " + i], MaximumAirPressure);
                AddWheel(wheel);
            }
        }

        public  float MaxBatteryCapacityInHours
        {
            get { return k_MaxBatteryCapacityInHours; }
        }
    }
}
