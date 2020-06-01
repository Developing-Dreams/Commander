using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetCommands()
        {
            var commands=new List<Command>
            {
                   new Command  { Id = 1, HowTo = "Get the how to 1", Line = "Line", Platform = "Platform1" },
                   new Command  { Id = 2, HowTo = "Get the how to 2", Line = "Line2", Platform = "Platform2" },
            };
            return commands;
        }
        public Command GetCommandById(int id)
        {
            return new Command() { Id = 1, HowTo = "Get the how to ", Line = "Line", Platform = "Platform1" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }

}