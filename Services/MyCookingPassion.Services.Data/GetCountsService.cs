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

        public GetCountsService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.categoriesRepository = categoriesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                RecipesCount = this.recipesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
            };

            return data;
        }
    }
}
