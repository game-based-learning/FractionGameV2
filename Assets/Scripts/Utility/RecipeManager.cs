using FractionGame.Ingredients;
using System.Collections.Generic;
using UnityEngine;

namespace FractionGame.Utility
{
    public class RecipeManager
    {
        private static RecipeManager instance;
        private List<Recipe> recipeList;

        //---------
        // Private recipe class used only in recipe manager
        private class Recipe
        {
            public Dictionary<string, int> ingredients { get; }
            public string recipeName { get; }

            public Recipe(string recipeName, params (string, int)[] pairs)
            {
                ingredients = new Dictionary<string, int>();
                this.recipeName = recipeName;
                // Add each ingredient name and number
                foreach ((string, int) pair in pairs)
                {
                    ingredients.Add(pair.Item1, pair.Item2);
                }
            }
        }
        //----------
        private RecipeManager()
        {
            // Temporarily created recipes for testing purposes
            recipeList = new List<Recipe>()
            {
                new Recipe("Recipe 1", ("IrisPetal", 2), ("LilyPetal", 1)),
                new Recipe("Recipe 2", ("IrisPetal", 1), ("LilyPetal", 2)),
                new Recipe("Recipe 3", ("IrisPetal", 1), ("LilyPetal", 1))
            };
        }

        public string GetRecipe(List<IIngredient> incomingIngredients)
        {
            List<(string, int)> countedIngredients = IngredientListToTuples(incomingIngredients);

            // Check each recipe
            foreach (Recipe recipe in recipeList)
            {
                bool isRecipe = true;
                // If every single counted ingredient has the right ratio, then it must of a certain recipe
                foreach (var (ingredientName, ratio) in countedIngredients)
                {
                    // If the current ingredient is NOT in the recipe OR does not have the right ratio, ignore this recipe
                    if (!recipe.ingredients.ContainsKey(ingredientName) || recipe.ingredients[ingredientName] != ratio)
                    {
                        isRecipe = false;
                        break;
                    }
                }
                if (isRecipe)
                {
                    return recipe.recipeName;
                } else
                {
                    isRecipe = false;
                }
            }
            // If no recipe was found, then return an empty string
            return string.Empty;
        }

        private List<(string, int)> IngredientListToTuples(List<IIngredient> incomingIngredients)
        {
            Dictionary<IIngredient, int> ingredientCount = new Dictionary<IIngredient, int>();
            // Count up all the ingredients
            foreach (IIngredient ingredient in incomingIngredients)
            {
                if (ingredientCount.ContainsKey(ingredient))
                {
                    ingredientCount[ingredient]++;
                }
                else
                {
                    ingredientCount.Add(ingredient, 1);
                }
            }

            // Convert to counted ingredients
            List<(string, int)> countedIngredients = new List<(string, int)>();
            foreach (var (ingredient, count) in ingredientCount)
            {
                countedIngredients.Add((ingredient.Name, count));
            }
            return countedIngredients;
        }

        public static RecipeManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RecipeManager();
            }
            return instance;
        }
    }
}