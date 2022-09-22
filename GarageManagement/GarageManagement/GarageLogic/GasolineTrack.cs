using System.Collections.Generic;

namespace ExO3.GarageLogic
{
    public class GasolineTrack : Track
    {
        private const float k_MaxLitersCapacity = 120f;
        private const GasolineFuelCell.eGasolineType k_GasolineType = GasolineFuelCell.eGasolineType.Soler;

        public GasolineTrack():base(){}
        public GasolineTrack(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            bool i_IsCarryHazardousMaterials,
            float i_MaxCarryWeight)
            : base(
                i_ManufacturerName,
                i_LicenseNumber,
                i_PercentageOfLeftEnergy,
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * i_PercentageOfLeftEnergy,
                    k_GasolineType),
                i_IsCarryHazardousMaterials,
                i_MaxCarryWeight)
        {
        }

        public GasolineTrack(Dictionary<string, object> i_DataMembers)
            : base(
                (string)i_DataMembers["manufacture name"],
                (string)i_DataMembers["license number"],
                (float)i_DataMembers["percentage of left energy"],
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * (float)i_DataMembers["percentage of left energy"],
                    k_GasolineType),
                (bool)i_DataMembers["Carrying Hazardous Materials"],
                (float)i_DataMembers["Max Carry Weight"])
        {
            for (int i = 1; i <= NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel((List<object>)i_DataMembers["wheel - " + i], MaximumAirPressure);
                AddWheel(wheel);
            }
        }
    }
}
