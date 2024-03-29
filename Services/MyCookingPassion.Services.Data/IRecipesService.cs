﻿namespace MyCookingPassion.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCookingPassion.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 15);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        T GetByName<T>(string recipeName);

        Task DeleteAsync(int id);
    }
}
