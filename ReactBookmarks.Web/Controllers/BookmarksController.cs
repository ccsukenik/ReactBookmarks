using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactBookmarks.Data;

namespace ReactBookmarks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private string _connectionString;

        public BookmarksController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("addbookmark")]
        public void AddBookmark(Bookmark b)
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            b.UserID = user.ID;
            var repo = new BookmarksRepository(_connectionString);
            repo.AddBookmark(b);
        }

        [HttpGet]
        [Route("getbookmarks")]
        public List<Bookmark> GetBookmarks(int id)
        {
            var repo = new BookmarksRepository(_connectionString);
            return repo.GetByUserID(id);
        }

        [HttpPost]
        [Route("deletebookmark")]
        public void DeleteBookmark(int id)
        {
            var repo = new BookmarksRepository(_connectionString);
            repo.DeleteBookmark(id);
        }

        [HttpPost]
        [Route("updatebookmark")]
        public void UpdateBookmark(Bookmark b)
        {
            var repo = new BookmarksRepository(_connectionString);
            repo.UpdateBookmark(b);
        }

        [HttpGet]
        [Route("gettop")]
        public List<TopBookmark> GetTop()
        {
            var repo = new BookmarksRepository(_connectionString);
            return repo.GetTopBookmarks();
        }
    }
}
