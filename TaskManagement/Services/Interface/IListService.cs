using TaskManagement.Models;

namespace TaskManagement.Services.Interface
{
    public interface IListService
    {
        bool CreateDefaultLists(int boardid);
    }

}
