namespace MyCookingPassion.Web.ViewModels.SearchRecipes
{
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Services.Mapping;

    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
