namespace SoccerCoach.Web.Areas.Administration.Controllers
{
    using SoccerCoach.Common;
    using SoccerCoach.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
