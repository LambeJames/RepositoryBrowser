using RepositoryBrowser.Models;
using RepositoryBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryBrowser.Services.Helpers
{
    public static class UserMapper
    {
        public static UserViewModel ToViewModel(User user)
        {
            return new UserViewModel
            {
                Name = user.Name,
                Location = user.Location,
                Avatar = user.AvatarUrl
            };
        }
    }
}
