using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Core;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using CodeEvents.Api.Core.DTOs;
using AutoMapper;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly IMapper mapper;
        private UnitOfWork uow;

        public CodeEventsController(CodeEventsApiContext db, IMapper mapper)
        {
            uow = new UnitOfWork(db);
            this.mapper = mapper;
        }
        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEventDto>>> GetCodeEvent(bool includeLectures = false)
        {
            var codeEvent = await uow.CodeEventRepository.GetAsync(includeLectures);
            var dto = mapper.Map<IEnumerable<CodeEventDto>>(codeEvent);
            return Ok(dto);
        }
        
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<CodeEventDto>> GetCodeEvent(string name, bool includeLectures = false)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest();

            var codeEvent = await uow.CodeEventRepository.GetAsync(name, includeLectures);

            if (codeEvent == null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeEvent);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CodeEventDto>> CreateCodeEvent(CreateCodeEventDto dto)
        {
            if(await uow.CodeEventRepository.GetAsync(dto.Name) != null)
            {
                ModelState.AddModelError("Name", "Name exists");
                return BadRequest(ModelState);
            }

            var codeEvent = mapper.Map<CodeEvent>(dto);
            await uow.CodeEventRepository.AddAsync(codeEvent);
            await uow.CompleteAsync();

            return CreatedAtAction(nameof(GetCodeEvent), new {name = codeEvent.Name}, mapper.Map<CodeEventDto>(dto));
        }
    }
}
