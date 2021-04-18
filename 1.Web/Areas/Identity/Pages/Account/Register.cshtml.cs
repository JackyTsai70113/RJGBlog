using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Web.Services.Interfaces;

namespace Web.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class RegisterModel : PageModel {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserService _userService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IUserService userService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel 
        {

            [Required(ErrorMessage = "此欄位為必填")]
            [Display(Name = "使用者名稱 *")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "此欄位為必填")]
            [EmailAddress(ErrorMessage = "Email格式不符")]
            [Display(Name = "Email *")]
            public string Email { get; set; }

            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "密碼 *")]
            public string Password { get; set; }

            [Required(ErrorMessage = "此欄位為必填")]
            [DataType(DataType.Password)]
            [Display(Name = "確認密碼 *")]
            [Compare("Password", ErrorMessage = "密碼需一致")]
            public string ConfirmPassword { get; set; }

            [CheckboxIsCheckedAttribute(ErrorMessage = "請同意條款並勾選")]
            public bool IsAgreeWithTerms { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null) 
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(); //取得外部驗，暫時沒用到
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(); //取得外部驗，暫時沒用到
            //Model驗證
            if (ModelState.IsValid) {
                IdentityResult result = _userService.CreateUser(Input.UserName, Input.Email, Input.Password).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation("使用者建立一組新的帳號，userName:{0}，Email:{1}", Input.UserName, Input.Email);

                    //取得帳號資訊
                    var user = await _userService.GetUserByEmailAsync(Input.Email);

                    //Email 驗證
                    string code = await _userService.GetEmailConfirmTokenAsync(Input.Email);

                    //Email驗證頁面
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    //寄出驗證Email emailSender是空殼 沒用到
                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"驗證您的帳號請 <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>按這裡</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                //錯誤訊息
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            //如果出錯，重新顯示頁面
            return Page();
        }

        [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        class CheckboxIsCheckedAttribute : RequiredAttribute 
        {
            public override bool IsValid(object value) {
                bool isRequiredValid = base.IsValid(value);
                if (!isRequiredValid)
                    return false;

                return (bool)value == true;
            }
        }
    }
}