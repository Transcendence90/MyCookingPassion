namespace MyCookingPassion.Web.Areas.Administration.Controllers
{
    using MyCookingPassion.Common;
    using MyCookingPassion.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
