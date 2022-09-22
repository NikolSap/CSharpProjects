using System.Collections.Generic;

namespace ExO3.GarageLogic
{
    class GasolineCar : Car
    {
        private const float k_MaxLitersCapacity = 45f;
        private const GasolineFuelCell.eGasolineType k_GasolineType = GasolineFuelCell.eGasolineType.Octan95;

        public GasolineCar():base(){}
        public GasolineCar(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            Factory.eColor i_Color,
            eNumberOfDoors i_NumberOfDoors)
            : base(
                i_ManufacturerName,
                i_LicenseNumber,
                i_PercentageOfLeftEnergy,
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * i_PercentageOfLeftEnergy,
                    k_GasolineType),
                i_Color,
                i_NumberOfDoors)
        {
        }

        public GasolineCar(Dictionary<string, object> i_DataMembers, Dictionary<string, int> i_EnumMembers)
            : base(
                (string)i_DataMembers["manufacture name"],
                (string)i_DataMembers["license number"],
                (float)i_DataMembers["percentage of left energy"],
                new GasolineFuelCell(
                    k_MaxLitersCapacity,
                    k_MaxLitersCapacity * (float)i_DataMembers["percentage of left energy"],
                    k_GasolineType),
                (Factory.eColor)i_EnumMembers["Color"],
                (eNumberOfDoors)i_EnumMembers["Number Of Doors"])
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
