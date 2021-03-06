namespace SoccerCoach.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using SoccerCoach.Common;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Services.Data.Client;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Web.ViewModels;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICoachesService _coachService;
        private readonly IClientsService _clientService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICoachesService coachService,
            IClientsService clientService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _coachService = coachService;
            _clientService = clientService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(80, ErrorMessage = "Full name must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Range(1,30)]
            [Display(Name = "Coach Experience")]
            public int? Experience { get; set; }

            [Display(Name = "Do you have any soccer experience?")]
            public bool HasExperience { get; set; }

            [StringLength(100, ErrorMessage = "Description must be atleast {2} and at max {1} characters long.", MinimumLength = 30)]
            public string Description { get; set; }

            public string Phone { get; set; }

            [Display(Name = "Choose the role you play")]
            public PositionName PositionPlayed { get; set; }

            public string SelectedRole { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (Input.SelectedRole == "Coach")
                {
                    var coachInputModel = new CreateCoachInputModel
                    {
                        FullName = Input.FullName,
                        Email = Input.Email,
                        Description = Input.Description,
                        Experience = Input.Experience.HasValue ? Input.Experience.Value : 1,
                        Phone = Input.Phone,
                    };

                    var coachUser = _userManager.FindByEmailAsync(coachInputModel.Email).Result;
                    await _coachService.CreateCoachAsync(coachInputModel, coachUser);
                    await _userManager.AddToRoleAsync(user, GlobalConstants.CoachRoleName);
                }
                else
                {
                    var clientInputModel = new CreateClientInputModel
                    {
                        FullName = Input.FullName,
                        Email = Input.Email,
                        PositionPlayed = Input.PositionPlayed,
                        HasExperience = Input.HasExperience,
                        Phone = Input.Phone,
                    };

                    var clientUser = _userManager.FindByEmailAsync(clientInputModel.Email).Result;
                    await _clientService.CreateClientAsync(clientInputModel, clientUser);
                    await _userManager.AddToRoleAsync(user, GlobalConstants.ClientRoleName);
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}