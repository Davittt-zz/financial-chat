using AutoMapper;
using JobsityFinancialChat.Domain.API.User;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Logic.Interfaces;
using JobsityFinancialChat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityFinancialChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly IDatabaseProvider _databaseProvider;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        
        public AccountsController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IDatabaseProvider databaseProvider,
            IMapper mapper,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _databaseProvider = databaseProvider;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var user = _userManager.Users.SingleOrDefault(r => r.Email == model.UserName && r.Active);

            if (user == null)
            {
                ModelState.AddModelError("notExists", "Usuario y/o contraseña incorrectos.");
                return BadRequest(ModelState);
            }

            user.RoleNames = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("notExists", "Usuario y/o contraseña incorrectos.");
                return BadRequest(ModelState);
            }
            else
            {
                var jsonWebToken = _tokenService.GenerateJwtToken(model.UserName, user);

                var requestedAt = DateTime.UtcNow;

                var returnModel = _mapper.Map<LoggedInUserDto>(user);
                returnModel.LastLoginDate = user.LastLoginDate;
                user.LastLoginDate = requestedAt;

                await _userManager.UpdateAsync(user);

                returnModel.Token = jsonWebToken.Token;
                returnModel.Expires = jsonWebToken.Expiration - requestedAt.Ticks;
                return Ok(returnModel);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email && r.Active);

            if (appUser != null)
            {
                ModelState.AddModelError("exists", "Usuario existente");
                return BadRequest(ModelState);
            }
            appUser = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);

                return Ok(_tokenService.GenerateJwtToken(appUser.Email, appUser));
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok($"User was logged out.");
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok("Password changed successfuly");
            }
            return NotFound();
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (model.CurrentPassword == model.NewPassword)
            {
                throw new ArgumentException("Current password and new password are the same.");
            }

            if (_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.CurrentPassword)
                == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Current password is incorrect.");
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

            await _databaseProvider.Save();

            return Ok("Password has been changed successfully");
        }

    }
}