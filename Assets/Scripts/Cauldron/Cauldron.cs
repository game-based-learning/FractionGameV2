using UnityEngine;
using System.Collections.Generic;
using FractionGame.Ingredients;
using FractionGame.Utility;

namespace FractionGame
{
    public class Cauldron : MonoBehaviour
    {
        private List<IIngredient> ingredients = new List<IIngredient>();//takes ingredients
        private Fraction value = new Fraction(0, 1);

        public Fraction Value => value;
        public IReadOnlyList<IIngredient> Ingredients => ingredients.AsReadOnly();

       public void AddIngredient(IIngredient ingredient)
{
    if (ingredient == null)
    {
        Debug.LogWarning("Tried to add a null ingredient to the cauldron.");
        return;
    }

    switch (ingredient.Type)
    {
        case IngredientType.CATALYST:
            // Must cast to access multiplier
            if (ingredient is Catalyst catalyst)
            {
                value = value * catalyst.Multiplier;
                Debug.Log($"Catalyst '{catalyst.Name}' applied. Multiplier: {catalyst.Multiplier}. New total: {value}");
            }
            else
            {
                Debug.LogError("Ingredient is marked as CATALYST but not a Catalyst instance.");
            }
            break;

        case IngredientType.PETAL:
            ingredients.Add(ingredient);
            value += ingredient.Value;
            Debug.Log($"Added {ingredient.Type} '{ingredient.Name}' with value {ingredient.Value}. Total: {value}");
            break;

        default:
            Debug.LogWarning($"Unhandled ingredient type: {ingredient.Type}");
            break;
    }

    // Optional: destroy used object
    Destroy((ingredient as MonoBehaviour)?.gameObject);
}
        public void Subtraction()
        {
            if (ingredients.Count < 2)
            {
                Debug.LogWarning("Need at least two ingredients to perform subtraction.");
                return;
            }

            Fraction result = ingredients[0].Value;
            for (int i = 1; i < ingredients.Count; i++)
            {
                result -= ingredients[i].Value;
            }

            value = result;
            Debug.Log($"Performed subtraction. Resulting value: {value}");

            
        }

        public void ResetCauldron()
        {
            ingredients.Clear();
            value = new Fraction(0, 1);
            Debug.Log("Cauldron reset.");
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"2D Trigger hit with {other.name}");

         var ingredient = other.GetComponent<IIngredient>();
          if (ingredient != null)
             {
             Debug.Log($"Accepted ingredient: {ingredient.Name}, value: {ingredient.Value}");
              AddIngredient(ingredient);
               Destroy(other.gameObject);
             }
          else
            {
        Debug.Log("Entered object is not an ingredient.");
             }
        }
    }
}
