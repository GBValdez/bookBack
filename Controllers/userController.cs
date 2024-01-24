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
using Microsoft.IdentityModel.Tokens;
using prueba.DTOS;

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
        public userController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signManager = signManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<authenticationDto>> register(credentialsDto credentials)
        {

            IdentityUser user = new IdentityUser() { UserName = credentials.email, Email = credentials.email };
            IdentityResult result = await userManager.CreateAsync(user, credentials.password);
            if (result.Succeeded)
            {
                return await createToken(credentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]

        public async Task<ActionResult<authenticationDto>> login(credentialsDto credentials)
        {
            var result = await signManager.PasswordSignInAsync(credentials.email, credentials.password, false, false);
            if (result.Succeeded)
                return await createToken(credentials);
            else
                return BadRequest("Login Incorrecto");
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