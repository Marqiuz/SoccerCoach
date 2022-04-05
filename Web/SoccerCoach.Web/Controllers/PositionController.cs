﻿namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Services.Data;

    public class PositionController : BaseController
    {
        private readonly IPositionsService positionsService;

        public PositionController(IPositionsService positionsService)
        {
            this.positionsService = positionsService;
        }

        public async Task<IActionResult> Position(string id)
        {
            var viewModel = await this.positionsService.GetPlayerAsync(id);

            return this.View(viewModel);
        }
    }
}
