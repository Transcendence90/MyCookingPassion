namespace MyCookingPassion.Services.Data
{
    using System.Threading.Tasks;

    using MyCookingPassion.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input);
    }
}
