using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Friends.Models
{
    public class FriendsContext : DbContext
    {
        public DbSet<FriendModel> Friends { get; set; }
    }
}