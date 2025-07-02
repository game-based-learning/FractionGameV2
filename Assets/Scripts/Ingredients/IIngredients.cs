using FractionGame.Ingredients;
using FractionGame.Utility;

namespace FractionGame.Ingredients
{
    public enum IngredientType {PETAL, POTION, CATALYST}
    /**
     * Interface for ingredients
     */
    public interface IIngredient
    {
        Fraction Value { get; }
        IngredientType Type { get; }
        string Name { get; }
    }
}

