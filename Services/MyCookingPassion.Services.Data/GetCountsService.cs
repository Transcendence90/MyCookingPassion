namespace MyCookingPassion.Services.Data
{
    using System.Linq;

    using MyCookingPassion.Data.Common.Repositories;
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IRecipesService recipesService;

        public GetCountsService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IRecipesService recipesService)
        {
            this.recipesRepository = recipesRepository;
            this.categoriesRepository = categoriesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesService = recipesService;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                RecipesCount = this.recipesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                RandomRecipes = this.recipesService.GetRandom<IndexPageRecipeViewModel>(9),
            };

            return data;
        }
    }
}
