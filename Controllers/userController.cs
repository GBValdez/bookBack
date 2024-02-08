using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using prueba.DTOS;
using prueba.services;

namespace prueba.Controllers
{
    [ApiController]
    [Route("user")]
    public class userController : ControllerBase
    {
        //Esto nos va servir para crear nuevos usuarios 
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        // Esto nos va a servir para el login
        private readonly SignInManager<IdentityUser> signManager;
        private readonly emailService emailService;
        public userController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signManager, emailService emailService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signManager = signManager;
            this.emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> register(userCreationDto credentials)
        {
            IdentityUser user = new IdentityUser() { UserName = credentials.userName, Email = credentials.email };
            IdentityResult result = await userManager.CreateAsync(user, credentials.password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "userNormal");
                string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                emailService.SendEmail(new emailSendDto
                {
                    email = credentials.email,
                    subject = "Confirmacion de correo",
                    message = $"<h1>Correo de confirmación</h1> <a href='{configuration["FrontUrl"]}/user/confirmEmail?email={credentials.email}&token={encodedToken}'>Confirmar correo</a>"
                });
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult> confirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            IdentityUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Usuario no encontrado");
            byte[] decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        [HttpPost("login")]

        public async Task<ActionResult<authenticationDto>> login(credentialsDto credentials)
        {

            IdentityUser EMAIL = await userManager.FindByEmailAsync(credentials.email);
            if (EMAIL == null)
                return BadRequest(new errorMessageDto("Credenciales invalidas"));

            if (await userManager.IsEmailConfirmedAsync(EMAIL) == false)
                return BadRequest(new errorMessageDto("Credenciales invalidas"));

            var result = await signManager.PasswordSignInAsync(EMAIL.UserName, credentials.password, false, false);
            if (result.Succeeded)
                return await createToken(credentials);
            else
                return BadRequest(new errorMessageDto("Credenciales invalidas"));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("renewToken")]
        public async Task<ActionResult<authenticationDto>> renewToken()
        {
            Claim email = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            credentialsDto credential = new credentialsDto
            {
                email = email.Value
            };
            return await createToken(credential);
        }

        [HttpPost("createAdmin")]
        public async Task<ActionResult> createAdmin(emailDto email)
        {
            IdentityUser user = await userManager.FindByEmailAsync(email.email);
            if (user == null)
                return NotFound();
            await userManager.AddToRoleAsync(user, "Administrator");
            return NoContent();
        }

        [HttpPost("removeAdmin")]
        public async Task<ActionResult> removeAdmin(emailDto email)
        {
            IdentityUser user = await userManager.FindByEmailAsync(email.email);
            if (user == null)
                return NotFound();
            await userManager.RemoveClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }

        private async Task<authenticationDto> createToken(credentialsDto credentials)
        {
            IdentityUser user = await userManager.FindByEmailAsync(credentials.email);
            IList<Claim> claimUser = await userManager.GetClaimsAsync(user);
            IList<string> roles = await userManager.GetRolesAsync(user);
            foreach (string rol in roles)
            {
                claimUser.Add(new Claim(ClaimTypes.Role, rol));
            }
            claimUser.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimUser.Add(new Claim(ClaimTypes.Name, user.UserName)); // Agrega el nombre de usuario como un claim


            // Estos son los parametros que guardara el webToken
            List<Claim> claims = new List<Claim>(){
                new Claim("email", credentials.email),
            };
            claims.AddRange(claimUser);

            // Creamos nuestra sensual llave
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyJwt"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Creamos la fecha de expiracion
            DateTime expiration = DateTime.UtcNow.AddDays(1);

            // Creamos nuestro token
            JwtSecurityToken securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new authenticationDto()
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expiration = expiration
            };
        }
    }
}