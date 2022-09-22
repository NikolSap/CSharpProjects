using System.Text;

namespace ExO3.GarageLogic
{
    public class ElectricFuelCell : FuelCell
    {
        private readonly float r_MaxBatteryCapacityInHours;
        private float m_CurrentBatteryStatusCapacityInHours;

        public ElectricFuelCell(float i_MaxBatteryCapacity, float i_CurrentBatteryStatusCapacity)
        {
            r_MaxBatteryCapacityInHours = i_MaxBatteryCapacity;
            m_CurrentBatteryStatusCapacityInHours = i_CurrentBatteryStatusCapacity;
        }

        public void ChargeBattery(float i_AdditionalHoursAddingToBattery)
        {
            if (m_CurrentBatteryStatusCapacityInHours + i_AdditionalHoursAddingToBattery <= r_MaxBatteryCapacityInHours)
            {
                m_CurrentBatteryStatusCapacityInHours += i_AdditionalHoursAddingToBattery;
            }
            else
            {
                throw new ValueOutOfRangeException(
                    0,
                    (int)(MaxBatteryCapacity - m_CurrentBatteryStatusCapacityInHours));
            }
        }

        public float MaxBatteryCapacity
        {
            get { return r_MaxBatteryCapacityInHours; }
        }

        public float CurrentBatteryStatusCapacity
        {
            get { return m_CurrentBatteryStatusCapacityInHours; }
            set { m_CurrentBatteryStatusCapacityInHours = value; }
        }

        public override string ToString()
        {
            StringBuilder classToString = new StringBuilder();

            classToString.AppendFormat("\tMaximum Battery Capacity In Hours: {0}", r_MaxBatteryCapacityInHours);
            classToString.AppendFormat("\tCurrent Battery Status Capacity In Hours: {0}", m_CurrentBatteryStatusCapacityInHours);

            return classToString.ToString();
        }
    }
}
