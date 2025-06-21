using UnityEngine;

namespace Utility
{
    public class Fraction
    {
        private int numerator;
        private int denominator;

        public int Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0)
                {
                    throw new System.DivideByZeroException("Denominator cannot be zero.");
                }
                denominator = value;
            }
        }

        // TODO: Make get value and to string
    }
}