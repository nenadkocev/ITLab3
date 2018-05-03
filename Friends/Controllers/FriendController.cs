using Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Friends.Controllers
{
    public class FriendController : Controller
    {
        private FriendsContext friends = new FriendsContext();

        private static int friendToEdit;

        // GET: Friend
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowFriends(string searchString)
        {

            var model = friends.Friends
                        .Where(friend => String.IsNullOrEmpty(searchString) || friend.Name.Contains(searchString))
                        .OrderBy(friend => friend.Name);

            return View(model.ToList());
        }

        public ActionResult DeleteFriend(int id)
        {
            FriendModel m = friends.Friends.FirstOrDefault(f => f.Id == id);
            if(m != null)
                friends.Friends.Remove(m);
            friends.SaveChanges();
            return View("ShowFriends", friends.Friends.ToList());
        }

        public ActionResult EditFriend(int id)
        {
            FriendModel m = friends.Friends.FirstOrDefault(f => f.Id == id);
            friendToEdit = id;
            return View(m);
        }

        public ActionResult AddFriend()
        {
            FriendModel model = new FriendModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddNewFriend(FriendModel model)
        {
            if (!ModelState.IsValid)
                return View("AddFriend");
            friends.Friends.Add(model);
            friends.SaveChanges();
            return View("ShowFriends", friends.Friends.ToList());
        }  

        [HttpPost]
        public ActionResult FriendEdited(FriendModel model)
        {
            FriendModel fr = friends.Friends.FirstOrDefault(f => f.Id == friendToEdit);
            if (!ModelState.IsValid) {
                return View("EditFriend", fr);
            }   
            friends.Friends.Remove(fr);
            friends.Friends.Add(model);
            friends.SaveChanges();
            return View("ShowFriends", friends.Friends.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (friends != null)
                friends.Dispose();
            base.Dispose(disposing);
        }
    }
}