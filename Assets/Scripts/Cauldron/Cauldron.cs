using UnityEngine;
using System.Collections.Generic;
using FractionGame.Ingredients;
using FractionGame.Utility;
using Unity.VisualScripting;

namespace FractionGame.Cauldron
{
    public class Cauldron : MonoBehaviour
    {
        private List<IIngredient> ingredients = new List<IIngredient>();//takes ingredients
        private Fraction value = new Fraction(0, 1);
        
        public Fraction Value => value;
        public IReadOnlyList<IIngredient> Ingredients => ingredients.AsReadOnly();
        public RecipeManager recipeManager;
        [SerializeField] potion potionPrefab; 
        void Start()
        {
            recipeManager = RecipeManager.GetInstance();
        }

        public void AddIngredient(IIngredient ingredient)
        {
            if (ingredient == null)
            {
                Debug.LogWarning("Tried to add a null ingredient to the cauldron.");
                return;
            }

            ingredients.Add(ingredient);
            value += ingredient.Value;

            Debug.Log($"Added {ingredient.Type} '{ingredient.Name}' with value {ingredient.Value}. Total: {value}");

            // TODO: Once potions are created, this functionality will be moved to the potion creation process
            string recipeName = recipeManager.GetRecipe(ingredients);
            if (recipeName.NullIfEmpty() != null)
            {
                Debug.Log("Current recipe: " + recipeName);
            }

            CreatePotion(ingredient.Name);
        }
        public void CreatePotion(string RecipeName) {
            potion potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);
            string name = RecipeName;
            potion.Initialize(value, name);
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
