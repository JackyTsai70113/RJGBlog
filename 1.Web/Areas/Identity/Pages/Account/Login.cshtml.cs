using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Web.Services.Interfaces;

namespace Web.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    public class LoginModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            IUserService userService
            )
        {
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
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

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                // 用 Email 登入
                IdentityUser user = await _userService.GetUserByEmailAsync(Input.Email);
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                //Email是否驗證
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        //Email 驗證
                        //string code = await _userService.GetEmailConfirmTokenAsync(Input.Email);
                        //Email驗證頁面
                        //var callbackUrl = Url.Page(
                        //    "/Account/ConfirmEmail",
                        //    pageHandler: null,
                        //    values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        //    protocol: Request.Scheme);

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