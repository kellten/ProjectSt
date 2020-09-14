using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen
{
    public enum Recipe
    {
        Ratatouille = 0,
        Aloo_gobi = 1
    }
    class Recipes
    {
        public delegate string[] PrepareMasala();
        public static Dictionary<Recipe, PrepareMasala> PrepareSpiceIngredientsAlgorithms = new Dictionary<Recipe, PrepareMasala>();

        public delegate string[] buyVegie();
        public static Dictionary<Recipe, buyVegie> buyVegieAlgorithms = new Dictionary<Recipe, buyVegie>();

        public delegate string[] cleanVessel();
        public static Dictionary<Recipe, cleanVessel> cleanVesselAlgorithms = new Dictionary<Recipe, cleanVessel>();

        public delegate string[] decorate();
        public static Dictionary<Recipe, decorate> decorateAlgorithms = new Dictionary<Recipe, decorate>();

        static Recipes()
        {
            /*
            * Create algorithms for each Recipe
            */
            Recipes.buyVegieAlgorithms.Add(Recipe.Ratatouille, () => new string[4] { "Ginger", "Onion", "zucchini", "eggplant" });
            Recipes.buyVegieAlgorithms.Add(Recipe.Aloo_gobi, () => new string[4] { "Potatoes", "Cauliflower", "Onion", "Ginger" });

            Recipes.PrepareSpiceIngredientsAlgorithms.Add(Recipe.Ratatouille, () => new string[2] { "bell peppers", "fennel and basil" });
            Recipes.PrepareSpiceIngredientsAlgorithms.Add(Recipe.Aloo_gobi, () => new string[4] { "Garam masala", "Turmeric", "Cumin", "Chilli powder" });

            Recipes.cleanVesselAlgorithms.Add(Recipe.Ratatouille, () => new string[4] { "Pot", "Pan Lid", "Ladle", "Knife" });
            Recipes.cleanVesselAlgorithms.Add(Recipe.Aloo_gobi, () => new string[3] { "Pan", "Knife", "Ladle" });

            Recipes.decorateAlgorithms.Add(Recipe.Ratatouille, () => new string[2] { "Bay leaf", "Thyme" });
            Recipes.decorateAlgorithms.Add(Recipe.Aloo_gobi, () => new string[2] { "Fenu greek", "Coriander leaves", });
        }
    }
}
