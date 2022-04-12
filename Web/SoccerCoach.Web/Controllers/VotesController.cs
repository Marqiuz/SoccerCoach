namespace SoccerCoach.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Services.Data.Votes;
    using SoccerCoach.Web.ViewModels.Votes;

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
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel inputModel)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetVoteAsync(inputModel.CoachId, userId, inputModel.Value);

            var averageVotes = this.votesService.GetAverageVotes(inputModel.CoachId);

            return new PostVoteResponseModel { AverageVote = averageVotes };
        }
    }
}
