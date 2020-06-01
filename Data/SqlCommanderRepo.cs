using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if(command==null){
                throw new ArgumentNullException(nameof(command));   
            }
            _context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if(command==null){
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Remove(command);
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(x=>x.Id.Equals(id));
        }

        public IEnumerable<Command> GetCommands()
        {
            return _context.Commands.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateCommand(Command command)
        {
           //No Code required
        }
    }
}