using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Models.ViewModels
{
    public class BaseViewModel
    {
        public Project Project { get; set; }
    }

    public class AllInfoModel : BaseViewModel
    {
        public Board Board { get; set; }

        public IEnumerable<ListWithIssuesModel> ListsWithIssues { get; set; }

        public IEnumerable<IdentityUser> ProjectMembers { get; set; }
    }

    public class ListWithIssuesModel
    {
        public List List { get; set; }

        public IEnumerable<Issue> Issues { get; set; }
    }

    public class ProjectUsersViewModel : BaseViewModel
    {
        public IEnumerable<IdentityUser> ProjectMembers { get; set; }
    }
}