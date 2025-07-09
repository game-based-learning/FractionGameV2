namespace FractionGame.Utility
{
    public class RecipeManager
    {
        private static RecipeManager instance;

        private RecipeManager() { }

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