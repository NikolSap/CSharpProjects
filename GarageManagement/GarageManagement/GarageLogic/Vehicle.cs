using System.Collections.Generic;
using System.Text;

namespace ExO3.GarageLogic
{
    public class Vehicle
    {
        private readonly string r_ManufacturerName;
        private readonly string r_LicenseNumber;
        private float m_PercentageOfLeftEnergy;
        private readonly FuelCell r_FuelCell;
        private readonly List<Wheel> r_WheelCollection=new List<Wheel>();

        public Vehicle() { }

        public Vehicle(string i_ManufacturerName, string i_LicenseNumber, float i_PercentageOfLeftEnergy, FuelCell i_FuelCell)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_LicenseNumber = i_LicenseNumber;
            m_PercentageOfLeftEnergy = i_PercentageOfLeftEnergy;
            r_FuelCell = i_FuelCell;
            if(m_PercentageOfLeftEnergy < 0 || m_PercentageOfLeftEnergy > 1)
            {
                throw new ValueOutOfRangeException(0, 1);
            }
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public float PercentageOfLeftEnergy
        {
            get { return m_PercentageOfLeftEnergy; }
            set { m_PercentageOfLeftEnergy = value; }
        }

        public List<Wheel> WheelCollection
        {
            get { return r_WheelCollection; }
        }

        public FuelCell FuelCell
        {
            get { return r_FuelCell; }
        }

        public override string ToString()
        {
            StringBuilder stringOfTheClass = new StringBuilder();

            stringOfTheClass.AppendFormat("\tManufacturer Name: {0},", r_ManufacturerName);
            stringOfTheClass.AppendLine();
            stringOfTheClass.AppendFormat("\tLicense Number: {0},", r_LicenseNumber);
            stringOfTheClass.AppendLine();
            stringOfTheClass.AppendFormat("\tPercentage Of Left Energy: {0}", m_PercentageOfLeftEnergy);
            stringOfTheClass.AppendLine();
            stringOfTheClass.Append("Fuel cell data:");
            stringOfTheClass.AppendLine();
            stringOfTheClass.Append(r_FuelCell);
            stringOfTheClass.AppendLine();
            stringOfTheClass.Append("Wheels data:");
            stringOfTheClass.AppendLine();

            foreach (Wheel wheel in r_WheelCollection)
            {
                stringOfTheClass.Append(wheel);
            }

            return stringOfTheClass.ToString();
        }

        public virtual Dictionary<string, object> GetDictionaryOfTheClass()
        {
            Dictionary<string, object> vehicleDictionary = new Dictionary<string, object>();
            vehicleDictionary.Add("manufacture name", typeof(string));
            vehicleDictionary.Add("license number",typeof(string));
            vehicleDictionary.Add("percentage of left energy", typeof(float));

            return vehicleDictionary;
        }

        public void AddWheel(Wheel i_Wheel)
        {
            r_WheelCollection.Add(i_Wheel);
        }
    }
}