using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandReadProfiles:Profile 
    {
        public CommandReadProfiles()
        {
            //Source to Target
            CreateMap<Command,CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<CommandUpdateDto,Command>();
            CreateMap<Command,CommandUpdateDto>();

        }
        
    }
    
}