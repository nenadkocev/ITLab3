using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Friends.Models;

namespace Friends.Controllers
{
    public class FriendApiController : ApiController
    {
        private FriendsContext db = new FriendsContext();

        // GET: api/FriendApi
        public IQueryable<FriendModel> GetFriends()
        {
            return db.Friends;
        }

        // GET: api/FriendApi/5
        [ResponseType(typeof(FriendModel))]
        public async Task<IHttpActionResult> GetFriendModel(int id)
        {
            FriendModel friendModel = await db.Friends.FindAsync(id);
            if (friendModel == null)
            {
                return NotFound();
            }

            return Ok(friendModel);
        }

        // PUT: api/FriendApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFriendModel(int id, FriendModel friendModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendModel.Id)
            {
                return BadRequest();
            }

            db.Entry(friendModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FriendApi
        [ResponseType(typeof(FriendModel))]
        public async Task<IHttpActionResult> PostFriendModel(FriendModel friendModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friends.Add(friendModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = friendModel.Id }, friendModel);
        }

        // DELETE: api/FriendApi/5
        [ResponseType(typeof(FriendModel))]
        public async Task<IHttpActionResult> DeleteFriendModel(int id)
        {
            FriendModel friendModel = await db.Friends.FindAsync(id);
            if (friendModel == null)
            {
                return NotFound();
            }

            db.Friends.Remove(friendModel);
            await db.SaveChangesAsync();

            return Ok(friendModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendModelExists(int id)
        {
            return db.Friends.Count(e => e.Id == id) > 0;
        }
    }
}