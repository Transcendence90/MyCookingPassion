namespace MyCookingPassion.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyCookingPassion.Data.Common.Repositories;
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Services.Mapping;
    using MyCookingPassion.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId, string imagePath)
        {
            var recipe = new Recipe()
            {
                CategoryId = input.CategoryId,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                Instructions = input.Instructions,
                Name = input.Name,
                PortionsCount = input.PortionsCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientsRepository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);
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

            Directory.CreateDirectory($"{imagePath}/recipes/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                recipe.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<Т> GetAll<Т>(int page, int itemsPerPage = 15)
        {
            var recipes = this.recipesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<Т>()
                .ToList();

            return recipes;
        }

        public T GetById<T>(int id)
        {
            var recipe = this.recipesRepository.AllAsNoTracking().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();

            return recipe;
        }

        public IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds)
        {
            var query = this.recipesRepository.All().AsQueryable();
            foreach (var ingredientId in ingredientIds)
            {
                query = query.Where(x => x.Ingredients.Any(ing => ing.IngredientId == ingredientId));
            }

            return query.To<T>().ToList();
        }

        public int GetCount()
        {
            return this.recipesRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.recipesRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .To<T>()
                .Take(count)
                .ToList();
        }

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);
            recipe.Name = input.Name;
            recipe.Instructions = input.Instructions;
            recipe.CookingTime = TimeSpan.FromMinutes(input.CookingTime);
            recipe.PreparationTime = TimeSpan.FromMinutes(input.PreparationTime);
            recipe.CategoryId = input.CategoryId;
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
