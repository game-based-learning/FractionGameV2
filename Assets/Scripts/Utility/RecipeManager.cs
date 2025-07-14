using FractionGame.Ingredients;
using System.Collections.Generic;

namespace FractionGame.Utility
{
    public class RecipeManager
    {
        private static RecipeManager instance;
        private List<Recipe> recipeList;

        private class Recipe
        {
            private Dictionary<PlantType, int> ingredients;


            public Recipe(string name, params (PlantType, int)[] pairs)
            {
                // Add each plant type, number 
                foreach ((PlantType, int) pair in pairs)
                {
                    ingredients.Add(pair.Item1, pair.Item2);
                }
            }
        }

        private RecipeManager()
        {
            recipeList = new List<Recipe>()
            {
            };
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