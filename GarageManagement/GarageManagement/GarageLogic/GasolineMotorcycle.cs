using System.Collections.Generic;

namespace ExO3.GarageLogic
{
    public class GasolineMotorcycle : Motorcycle
    {
        private const float k_MaxLitersCapacity = 6f;
        private const GasolineFuelCell.eGasolineType k_GasolineType = GasolineFuelCell.eGasolineType.Octan98;

        public GasolineMotorcycle(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            eMotorcycleLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(
                i_ManufacturerName,
                i_LicenseNumber,
                i_PercentageOfLeftEnergy,
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * i_PercentageOfLeftEnergy,
                    k_GasolineType),
                i_LicenseType,
                i_EngineCapacity)
        {
        }

        public GasolineMotorcycle():base(){}
        public GasolineMotorcycle(Dictionary<string, object> i_DataMembers, Dictionary<string, int> i_EnumMembers)
            : base(
                (string)i_DataMembers["manufacture name"],
                (string)i_DataMembers["license number"],
                (float)i_DataMembers["percentage of left energy"],
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * (float)i_DataMembers["percentage of left energy"],
                    k_GasolineType),
                (eMotorcycleLicenseType)i_EnumMembers["Motorcycle License Type"],
                (int)i_DataMembers["engine capacity"])
        {
            for (int i = 1; i <= NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel((List<object>)i_DataMembers["wheel - " + i], MaximumAirPressure);
                AddWheel(wheel);
            }
        }

        public  float MaxLitersCapacity
        {
            get { return k_MaxLitersCapacity; }
        }
    }
}
