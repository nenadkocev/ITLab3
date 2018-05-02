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
        private static List<FriendModel> friends = new List<FriendModel>{
            new FriendModel(0, "Ashley", "England"),
            new FriendModel(1, "Robert", "Texas"),
            new FriendModel(2, "Roberta", "Arizona"),
            new FriendModel(3, "Trpe", "Skopje"),
            new FriendModel(4, "Nenad", "Kocani"),
            new FriendModel(5, "Trpe", "Kavadarci")
        };

        private static int friendToEdit;

        // GET: Friend
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowFriends()
        {
            return View(friends);
        }

        public ActionResult DeleteFriend(int id)
        {
            FriendModel m = friends.Find(f => f.Id == id);
            friends.Remove(m);
            return View("ShowFriends", friends);
        }

        public ActionResult EditFriend(int id)
        {
            FriendModel m = friends.Find(f => f.Id == id);
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
            friends.Add(model);
            return View("ShowFriends", friends);
        }  

        [HttpPost]
        public ActionResult FriendEdited(FriendModel model)
        {
            FriendModel fr = friends.Find(f => f.Id == friendToEdit);
            if (!ModelState.IsValid) {
                return View("EditFriend", fr);
            }   
            friends.Remove(fr);
            friends.Add(model);
            return View("ShowFriends", friends);
        }
    }
}