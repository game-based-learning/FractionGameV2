using UnityEngine;
using FractionGame.Utility;

namespace FractionGame.Ingredients
{
    public class Catalyst : Draggable, IIngredient
    {
        [SerializeField] private int multiplierNumerator = 1;
        [SerializeField] private int multiplierDenominator = 1;
        [SerializeField] private string catalystName = "Catalyst";

        public Fraction Multiplier => new Fraction(multiplierNumerator, multiplierDenominator);

        public Fraction Value
        {
            get
            {
                return new Fraction(0, 1);
            }
        }

        public IngredientType Type => IngredientType.CATALYST;

        public string Name => catalystName;

        void Start()
        {
            if (multiplierDenominator == 0)
            {
                Debug.LogError("Catalyst denominator cannot be zero.");
                multiplierDenominator = 1;
            }
        }
    }
}
