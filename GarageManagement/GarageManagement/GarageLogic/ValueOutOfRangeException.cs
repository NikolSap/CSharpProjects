using System;

namespace ExO3.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) :
            base(string.Format($"Out of range error occurred{Environment.NewLine}" +
        $"The min value is: {i_MinValue}{Environment.NewLine}The max value is: {i_MaxValue}"))
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}
