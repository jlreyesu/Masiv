using Masiv.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Masiv.Infraestructure.Repositories
{
    public class UserSessionRepository
    {
        public IEnumerable<UserSession> ListUserSession()
        {
            var post = Enumerable.Range(1, 10).Select(x => new UserSession
            {
                UserId = x,
                Name = $"xxxxxx {x}",
                State = "Active",
                Active = true
            });
            return post;
        }
    }
}
