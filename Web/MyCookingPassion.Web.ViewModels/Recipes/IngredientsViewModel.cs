namespace MyCookingPassion.Web.ViewModels.Recipes
{
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
