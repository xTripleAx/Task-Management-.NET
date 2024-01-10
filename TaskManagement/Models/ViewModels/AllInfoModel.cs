using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Models.ViewModels
{
    public class AllInfoModel
    {
        public Board Board { get; set; }

        public Project Project { get; set; }

        public IEnumerable<ListWithIssuesModel> ListsWithIssues { get; set; }

        public IEnumerable<IdentityUser> ProjectMemebers { get; set; }
        
    }

    public class ListWithIssuesModel
    {
        public List List { get; set; }

        public IEnumerable<Issue> Issues { get; set; }
    }

}