using Ingredients;

namespace Ingredients
{
    public enum IngredientType = {PETAL, POTION, CATALYST}
    /**
     * Interface for ingredients
     */
    public interface IIngredient
    {
        Fraction Value { get; }
        IngredientType Type { get; }
        String Name { get; }
    }
}

