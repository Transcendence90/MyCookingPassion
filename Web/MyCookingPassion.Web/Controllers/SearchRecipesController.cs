namespace MyCookingPassion.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookingPassion.Services.Data;
    using MyCookingPassion.Web.ViewModels.Recipes;
    using MyCookingPassion.Web.ViewModels.SearchRecipes;

    public class SearchRecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IIngredientsService ingredientsService;

        public SearchRecipesController(
            IRecipesService recipesService,
            IIngredientsService ingredientsService)
        {
            this.recipesService = recipesService;
            this.ingredientsService = ingredientsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = this.ingredientsService.GetAll<IngredientNameIdViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipesService
                     .GetByIngredients<RecipeInListViewModel>(input.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
