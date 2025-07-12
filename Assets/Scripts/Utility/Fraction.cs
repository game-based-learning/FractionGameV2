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
            int numerator = a.numerator * b.denominator + b.numerator * a.denominator;
            int denominator = a.denominator * b.denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
          int numerator = a.numerator * b.denominator - b.numerator * a.denominator;
           int denominator = a.denominator * b.denominator;
          return new Fraction(numerator, denominator);
        }

    }
}