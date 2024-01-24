using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using prueba.DTOS;

namespace prueba.Controllers
{
    [ApiController]
    [Route("rol")]
    public class rolController : ControllerBase
    {
        private RoleManager<IdentityRole> rolManager;
        public rolController(RoleManager<IdentityRole> rolManager)
        {
            this.rolManager = rolManager;
        }

        [HttpPost]
        public async Task<ActionResult> createRol(rolCreationDto rol)
        {
            IdentityRole newRol = new IdentityRole
            {
                Name = rol.name
            };
            await rolManager.CreateAsync(newRol);
            return Ok();
        }

    }
}