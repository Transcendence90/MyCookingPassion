namespace MyCookingPassion.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookingPassion.Data.Common.Repositories;
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.IngredientsRepository = ingredientsRepository;
        }

        public IDeletableEntityRepository<Ingredient> IngredientsRepository { get; }

        public async Task CreateAsync(CreateRecipeInputModel input)
        {
            var recipe = new Recipe()
            {
                CategoryId = input.CategoryId,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                Instructions = input.Instructions,
                Name = input.Name,
                PortionsCount = input.PortionsCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.IngredientsRepository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = inputIngredient.IngredientName };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
