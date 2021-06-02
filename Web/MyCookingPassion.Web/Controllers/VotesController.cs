namespace MyCookingPassion.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookingPassion.Services.Data;
    using MyCookingPassion.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        public async Task<ActionResult<PostVoteResultModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.RecipeId, userId, input.Value);
            var averageVotesResult = this.votesService.GetAverageVotes(input.RecipeId);

            return new PostVoteResultModel { AverageVoteResult = averageVotesResult };
        }
    }
}
