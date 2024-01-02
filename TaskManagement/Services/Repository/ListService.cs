using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Services.Interface;

namespace TaskManagement.Services.Repository
{
    public class ListService : IListService
    {
        private readonly ApplicationDbContext _context;

        public ListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateDefaultLists(int boardid)
        {
            var todo = new List
            {
                Name = "To Do",
                ColumnLimit = 50,
                isListForFinish = false,
                BoardId = boardid
            };

            var inProgress = new List
            {
                Name = "In Progress",
                ColumnLimit = 50,
                isListForFinish = false,
                BoardId = boardid
            };

            var finish = new List
            {
                Name = "Finished",
                ColumnLimit = 50,
                isListForFinish = true,
                BoardId = boardid
            };

            _context.Lists.Add(todo);
            _context.Lists.Add(inProgress);
            _context.Lists.Add(finish);

            return true;
        }
    }

}
