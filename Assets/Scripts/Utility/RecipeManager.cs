using FractionGame.Ingredients;
using System.Collections.Generic;
using System.Linq;
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

            /**
             * Compares an incoming ingredient list to see if it matches this recipe. Requires two checks
             *    - Recipe and ingredient list are of the same length
             *    - All items in the list can be found in the correct ratio in the recipe
             */
            public bool isTypeOf(List<(string, int)> ingredientList)
            {
                // If current recipe does not have the same number of ingredient types as incoming ingredients skip it
                if (ingredients.Count != ingredientList.Count) return false;
                foreach (var (ingredientName, ratio) in ingredientList)
                {
                    // If the current ingredient is NOT in the recipe OR does not have the right ratio, ignore this recipe
                    if (!ingredients.ContainsKey(ingredientName) || ingredients[ingredientName] != ratio) return false;
                }

                // Otherwise, return true
                return true;
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

        /**
         * Method to check if the current list of ingredients is of a certain recipe type.
         * 
         * The function will first normalize the incoming ingredients list to a list of tuples of the following form:
         *      (ingredient name, ratio of this ingredient in the ingredient list)
         * Finally, returns the name of the recipe found. Returns an empty string if no recipe matches
         */
        public string GetRecipe(List<IIngredient> incomingIngredients)
        {
            List<(string, int)> countedIngredients = IngredientListToTuples(incomingIngredients);

            foreach (Recipe recipe in recipeList)
            {
                // For each recipe, check if the list follows the recipe
                if (recipe.isTypeOf(countedIngredients))
                {
                    return recipe.recipeName;
                }
            }
            // If no recipe was found, then return an empty string
            return string.Empty;
        }

        /**
         * Collects all incoming ingredients into a list of tuples.
         * All duplicates are then tallied up and converted to ratios for ease of recipe identification
         */
        private List<(string, int)> IngredientListToTuples(List<IIngredient> incomingIngredients)
        {
            Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
            // Count up all the ingredients
            foreach (IIngredient ingredient in incomingIngredients)
            {
                if (ingredientCount.ContainsKey(ingredient.Name))
                {
                    ingredientCount[ingredient.Name]++;
                }
                else
                {
                    ingredientCount.Add(ingredient.Name, 1);
                }
            }

            // Use the aggregate function to find the joint GCD between every ingredient count
            // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-9.0
            int gcd = ingredientCount.Values.Aggregate((a, b) => GCD(a, b));

            // Convert to counted ingredients
            List<(string, int)> countedIngredients = new List<(string, int)>();
            foreach (var (ingredientName, count) in ingredientCount)
            {
                countedIngredients.Add((ingredientName, count / gcd));
            }
            return countedIngredients;
        }

        /**
         * Simple GCD function to aid in converting ingredient counts to ratios.
         * Uses the euclidean algorithm of finding GCD's
         * https://www.w3schools.com/dsa/dsa_ref_euclidean_algorithm.php
         */
        private int GCD(int a, int b)
        {
            // Requires a to be larger or equal to b
            if (a < b)
            {
                int temp = b;
                b = a; 
                a = temp;
            }
            int r = 1;
            // If remainder is 0, b is the GCD
            while (true)
            {
                r = a % b;
                if (r == 0) break;
                a = b;
                b = r;
            }
            return b;
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