using FractionGame.Inputs;
using UnityEngine;

namespace FractionGame.Utility
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
                    Debug.LogError("Denominator cannot be zero.");
                }
                denominator = value;
            }
        }

        public float Value
        {
            get
            {
                return (float)numerator / denominator;
            }
        }

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            Denominator = denominator; // This will check for zero
        }

        public Fraction()
        {
            numerator = 0;
            denominator = 1;
        }


        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }
        
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int denominator = CalculateCommonDenominator(a, b);
            int numerator = denominator / a.denominator * a.numerator + denominator / b.denominator * b.numerator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            int denominator = CalculateCommonDenominator(a, b);
            int numerator = denominator / a.denominator * a.numerator - denominator / b.denominator * b.numerator;
            return new Fraction(numerator, denominator);
        }

        /// <summary>
        /// Returns the common denominator between two Fractions.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CalculateCommonDenominator(Fraction a, Fraction b)
        {
            /* 
             * Cases:
             * 1) a and b are the same
             * 2) one number is a factor of the other
             * 3) the two numbers need to be multiplied together to get the common denominator
             */

            int x = a.denominator;
            int y = b.denominator;

            if (x == y || x % y == 0)
                return x;
            else if (y % x == 0)
                return y;
            else
                return x * y;
        }
    }
}