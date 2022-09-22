using System;
using System.Collections;
using System.Collections.Generic;

namespace ExO3.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryCapacityInHours = 1.8f;

        public ElectricMotorcycle():base(){}

        public ElectricMotorcycle(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            eMotorcycleLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(
                i_ManufacturerName,
                i_LicenseNumber,
                i_PercentageOfLeftEnergy,
                new ElectricFuelCell(
                    k_MaxBatteryCapacityInHours,
                    k_MaxBatteryCapacityInHours * i_PercentageOfLeftEnergy),
                i_LicenseType,
                i_EngineCapacity)
        {
        }

        public ElectricMotorcycle(Dictionary<string, object> i_DataMembers, Dictionary<string, int> i_EnumMembers)
            : base(
                (string)i_DataMembers["manufacture name"],
                (string)i_DataMembers["license number"],
                (float)i_DataMembers["percentage of left energy"],
                new ElectricFuelCell(
                    k_MaxBatteryCapacityInHours,
                    k_MaxBatteryCapacityInHours * (float)i_DataMembers["percentage of left energy"]),
                (eMotorcycleLicenseType)i_EnumMembers["Motorcycle License Type"],
                (int)i_DataMembers["engine capacity"])
        {
            for (int i = 1; i <= NumberOfWheels; i++)
            {
                Wheel tempwheel = new Wheel((List<object>)i_DataMembers["wheel - " + i], MaximumAirPressure);
                AddWheel(tempwheel);
            }
        }

        public  float MaxBatteryCapacityInHours
        {
            get { return k_MaxBatteryCapacityInHours; }
        }
    }
}
