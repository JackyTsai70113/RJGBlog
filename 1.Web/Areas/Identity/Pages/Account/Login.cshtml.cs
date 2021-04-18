using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Web.Services.Interfaces;

namespace Web.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class LoginModel : PageModel {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel {

            [Display(Name = "電子郵件")]
            [Required(ErrorMessage = "此欄位為必填")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "密碼")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "此欄位為必填")]
            public string Password { get; set; }

            [Display(Name = "記住我")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null) {
            if (!string.IsNullOrEmpty(ErrorMessage)) {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                // 這邊是用 Email 當作 UserName 登入
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    //var user = await _userManager.FindByNameAsync(Input.Email);

                    //var role = await _roleManager.FindByIdAsync("001");

                    //await _roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Authentication, "privacyOK"));

                    //var r = await _userManager.AddToRoleAsync(user, "admin");

                    //var role = await _roleManager.FindByIdAsync("001");
                    //if(role == null)
                    //{
                    //    IdentityRole role1 = new IdentityRole()
                    //    {
                    //        Id = "001",
                    //        Name = "admin",
                    //        NormalizedName = "系統管理員"
                    //    };
                    //    await _roleManager.CreateAsync(role1);
                    //}
                    //else
                    //{
                       //await _roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Authentication, "backhomeOK"));

                    //}

                    //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Authentication, "backhomeOK"));

                    return LocalRedirect(returnUrl);
                }

                //Email是否驗證
                IdentityUser user = await _userService.GetUserByEmailAsync(Input.Email);
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        //Email 驗證
                        string code = await _userService.GetEmailConfirmTokenAsync(Input.Email);
                        //Email驗證頁面
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code, returnUrl },
                            protocol: Request.Scheme);

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "帳號不存在");
                    return Page();
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}