using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Super.Core.Repos;
using Super.Mvc.Models;

namespace Super.Mvc.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IRepoCajero _repocajero;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IRepoCajero repo)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _repocajero = repo;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(VMLoguinCajero model, string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Convertimos el DNI (uint) a string para usarlo como UserName
        string userName = model.Dni.ToString();

        // 1. USA IDENTITY para validar el DNI (como UserName) y Contraseña
        var result = await _signInManager.PasswordSignInAsync(
            userName, // El DNI es el UserName
            model.Pass,
            model.Recuerdame,
            lockoutOnFailure: true
        );

        if (result.Succeeded)
        {
            // 2. USA DAPPER para buscar el Cajero por su DNI (¡la PK!)
            var cajero = await _repocajero.GetByUserIdAsync(model.Dni);

            if (cajero is null)
            {
                // Esto NUNCA debería pasar si el registro se hizo bien.
                // Significa que hay un IdentityUser pero no un Cajero.
                await _signInManager.SignOutAsync();
                ModelState.AddModelError(string.Empty, "Error: Faltan datos del cajero.");
                return View(model);
            }

            // 3. (Opcional pero recomendado) Obtener el IdentityUser
            var identityUser = await _userManager.FindByNameAsync(userName);

            // 4. AÑADIMOS DATOS DEL CAJERO A LA COOKIE (Claims)
            var oldClaims = await _userManager.GetClaimsAsync(identityUser!);
            await _userManager.RemoveClaimsAsync(identityUser!, oldClaims);

            var claims = new List<Claim>
            {
                // Guardamos el DNI (aunque ya lo tenemos en el Name)
                new Claim(ClaimTypes.NameIdentifier, cajero.Dni.ToString()),
                // Guardamos el Nombre Completo
                new Claim("NombreCompleto", cajero.NombreCompleto)
            };

            await _userManager.AddClaimsAsync(identityUser!, claims);

            // 5. Refrescamos la cookie (esto es redundante si acabamos de hacer Sign In,
            // pero es buena práctica si añadimos claims)
            await _signInManager.RefreshSignInAsync(identityUser!);

            return LocalRedirect(returnUrl);
        }

        if (result.IsLockedOut)
            ModelState.AddModelError(string.Empty, "Cuenta bloqueada.");
        else
            ModelState.AddModelError(string.Empty, "DNI o Contraseña inválidos.");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Borra la cookie de Identity
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}