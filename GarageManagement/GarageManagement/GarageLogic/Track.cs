using System;
using System.Collections.Generic;
using System.Linq;

namespace ExO3.GarageLogic
{
    public class Track : Vehicle
    {
        public enum eWheelPlace
        {
            FirstLeft = 1,
            SecondLeft,
            ThirdLeft,
            FourthLeft,
            FifthLeft,
            SixthLeft,
            SeventhLeft,
            EighthLeft,
            FirstRight,
            SecondRight,
            ThirdRight,
            FourthRight,
            FifthRight,
            SixthRight,
            SeventhRight,
            EighthRight,
        }

        private bool m_IsCarryHazardousMaterials;
        private readonly float r_MaxCarryWeight;
        private const int k_NumberOfWheels = 16;
        private const int k_MaximumAirPressure = 26;

        public Track():base(){}
        protected Track(
            string i_ManufacturerName,
            string i_LicenseNumber,
            float i_PercentageOfLeftEnergy,
            FuelCell i_FuelCell,
            bool i_IsCarryHazardousMaterials,
            float i_MaxCarryWeight)
            : base(i_ManufacturerName, i_LicenseNumber, i_PercentageOfLeftEnergy, i_FuelCell)
        {
            m_IsCarryHazardousMaterials = i_IsCarryHazardousMaterials;
            r_MaxCarryWeight = i_MaxCarryWeight;
        }

        public int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return k_MaximumAirPressure;
            }
        }
        public bool IsCarryHazardousMaterials
        {
            get
            {
                return m_IsCarryHazardousMaterials;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return r_MaxCarryWeight;
            }
        }

        public override Dictionary<string, object> GetDictionaryOfTheClass()
        {
            Dictionary<string, object> trackDictionary = new Dictionary<string, object>(base.GetDictionaryOfTheClass());

            trackDictionary.Add("Carrying Hazardous Materials", typeof(bool));
            trackDictionary.Add("Max Carry Weight", typeof(float));
            trackDictionary.Add("number of wheels", typeof(int));

            for (int i = 1; i <= k_NumberOfWheels; i++)
            {
                trackDictionary.Add("wheel - " + i, typeof(Wheel));
            }

            return trackDictionary;
        }
    }
}

