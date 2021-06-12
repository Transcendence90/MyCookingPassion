namespace MyCookingPassion.Web.ViewModels.SearchRecipes
{
    using System.Collections.Generic;

    using MyCookingPassion.Web.ViewModels.Recipes;

    public class ListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
