using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{

    [ApiController]
    [Route("api/commands")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _commanderRepo;
        private IMapper _mapper;

        //public readonly MockCommanderRepo _mockCommanderRepo = new MockCommanderRepo();
        public CommandsController(ICommanderRepo commanderRepo, IMapper mapper)
        {
            _commanderRepo = commanderRepo;
            _mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commands = _commanderRepo.GetCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<Command> GetCommandById(int id)
        {

            var command = _commanderRepo.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            _commanderRepo.CreateCommand(command);
            _commanderRepo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtAction(nameof(GetCommandById), new { id = command.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandFromRepo);
            _commanderRepo.UpdateCommand(commandFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandFromRepo);
            _commanderRepo.UpdateCommand(commandFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }
            _commanderRepo.DeleteCommand(commandFromRepo);
            _commanderRepo.SaveChanges();
            return NoContent();

        }
    }

}