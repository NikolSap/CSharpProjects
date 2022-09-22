using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExO3.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaximumAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaximumAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaximumAirPressure = i_MaximumAirPressure;

            if (i_CurrentAirPressure > i_MaximumAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaximumAirPressure);
            }

        }

        public Wheel(List<object> i_Wheel, float i_MaximumAirPressure)
        {
            r_ManufacturerName = (string)i_Wheel[0];
            m_CurrentAirPressure = (float)i_Wheel[1];
            r_MaximumAirPressure = i_MaximumAirPressure;

            if (m_CurrentAirPressure > i_MaximumAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaximumAirPressure);
            }
        }

        public void InflatingAWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd <= r_MaximumAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, (int)r_MaximumAirPressure);
            }
        }

        public Wheel(float i_MaximumAirPressure)
        {
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaximumAirPressure
        {
            get { return r_MaximumAirPressure; }
        }

        public override string ToString()
        {
            StringBuilder stringOfTheClass = new StringBuilder();

            stringOfTheClass.AppendFormat("\tManufacturer Name: {0} ,", r_ManufacturerName);
            stringOfTheClass.AppendFormat("\tCurrent Air Pressure: {0} ,", m_CurrentAirPressure);
            stringOfTheClass.AppendFormat("\tMaximum Air Pressure: {0} ", r_MaximumAirPressure);

            return stringOfTheClass.ToString();
        }

        public static Dictionary<string, object> GetDictionaryOfTheClass()
        {
            Dictionary<string, object> wheelDictionary = new Dictionary<string, object>();

            wheelDictionary.Add("manufacture name of thw wheel", typeof(string));
            wheelDictionary.Add("current air pressure", typeof(float));

            return wheelDictionary;
        }
    }
}