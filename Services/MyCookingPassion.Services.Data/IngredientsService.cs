namespace MyCookingPassion.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyCookingPassion.Data.Common.Repositories;
    using MyCookingPassion.Data.Models;
    using MyCookingPassion.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.ingredientsRepository.All()
                .Where(x => x.Recipes.Count() >= 9)
                .OrderByDescending(x => x.Recipes.Count())
                .To<T>().ToList();
        }
    }
}
