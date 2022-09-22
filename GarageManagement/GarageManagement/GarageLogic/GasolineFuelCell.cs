using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExO3.GarageLogic
{
    public class GasolineFuelCell : FuelCell
    {
        public enum eGasolineType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        private readonly float r_MaxGasolineCapacityInLiters;
        private float m_GasolineRemainInLiters;
        private readonly eGasolineType r_GasolineType;

        public GasolineFuelCell(float i_MaxGasolineCapacityInLiters, float i_GasolineRemain, eGasolineType i_GasolineType)
        {
            m_GasolineRemainInLiters = i_GasolineRemain;
            r_GasolineType = i_GasolineType;
            r_MaxGasolineCapacityInLiters = i_MaxGasolineCapacityInLiters;
        }

        public void AddGasoline(float i_AdditionalLittersToAdd, eGasolineType i_GasolineType)
        {

            if (GasolineType == i_GasolineType)
            {

                if (m_GasolineRemainInLiters + i_AdditionalLittersToAdd <= r_MaxGasolineCapacityInLiters)
                {
                    m_GasolineRemainInLiters += i_AdditionalLittersToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, (int)r_MaxGasolineCapacityInLiters);
                }
            }
            else
            {
                throw new ArgumentException("The fuel type is invalid.");
            }

        }

        public virtual Dictionary<string, Dictionary<int, string>> GetDictionaryOfTheEnums()
        {
            Dictionary<string, Dictionary<int, string>> dictionaryOfGasolineFuelCellEnums =
                new Dictionary<string, Dictionary<int, string>>();
            Dictionary<int, string> singleEnum = Factory.EnumNamedValues<eGasolineType>();
            dictionaryOfGasolineFuelCellEnums.Add("Gasoline type", singleEnum);

            return dictionaryOfGasolineFuelCellEnums;
        }

        public eGasolineType GasolineType
        {
            get { return r_GasolineType; }
        }

        public float MaxGasolineCapacity
        {
            get { return r_MaxGasolineCapacityInLiters; }
        }

        public float GasolineRemain
        {
            get { return m_GasolineRemainInLiters; }
            set { m_GasolineRemainInLiters = value; }
        }

        public static List<eGasolineType> GetGasolineTypes()
        {
            return Enum.GetValues(typeof(eGasolineType)).Cast<eGasolineType>().ToList();
        }

        public override string ToString()
        {
            StringBuilder classToString = new StringBuilder();

            classToString.AppendFormat("\tGasoline Remain In Liters: {0}", m_GasolineRemainInLiters);
            classToString.AppendFormat("\tGasoline Type: {0}", r_GasolineType);

            return classToString.ToString();
        }
    }
}
