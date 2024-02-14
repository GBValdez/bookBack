using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("rol")]
    public class rolController : controllerCommons<rolEntity, rolCreationDto, rolDto, rolQueryDto, object, string>
    {
        private RoleManager<rolEntity> rolManager;
        public rolController(RoleManager<rolEntity> rolManager, AplicationDBContex contex, IMapper mapper) : base(contex, mapper)
        {
            this.rolManager = rolManager;
        }


        public override async Task<ActionResult<rolDto>> post(rolCreationDto newRegister, [FromQuery] object queryParams)
        {

            rolEntity newRol = new rolEntity
            {
                Name = newRegister.name
            };
            await rolManager.CreateAsync(newRol);
            return Ok();
        }

    }
}