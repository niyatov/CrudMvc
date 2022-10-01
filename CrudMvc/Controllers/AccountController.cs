using CrudMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace CrudMvc.AccountController;

public class AccountController :Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


    [HttpGet]
    public IActionResult Register(string returnUrl) => View(new Register() { ReturnUrl = returnUrl });

    
    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {

        var user = new IdentityUser(model.Username);
        var result = await _userManager.CreateAsync(user,model.Password);
        if(!result.Succeeded)
        {
            _logger.LogError($"User create bo'madi {result.Errors.First().Description}");
            ModelState.AddModelError("1","Something went wrong!");
            return View();
        }
        return  LocalRedirect($"/account/login?returnUrl={model.ReturnUrl}");
    }
    
    [HttpGet]
    public ActionResult Login(string returnUrl) => View(new Login() { ReturnUrl = returnUrl });
    [HttpPost]

    public async Task<ActionResult> Login(Login model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if(user == null) return LocalRedirect($"/account/Register?returnUrl={model.ReturnUrl}");
        var result = await _signInManager.PasswordSignInAsync(user, model.Password,false,false);
        if(!result.Succeeded) return RedirectToAction("login");
        return LocalRedirect($"{model.ReturnUrl ?? "/" }");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

}