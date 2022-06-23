using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactBookmarks.Data;
using ReactBookmarks.Web.Models;

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
        public List<Bookmark> GetBookmarks()
        {
            var user = GetCurrentUser();
            var repo = new BookmarksRepository(_connectionString);
            return repo.GetByUserID(user.ID);
        }

        [HttpPost]
        [Route("deletebookmark")]
        public void DeleteBookmark(DeleteViewModel vm)
        {
            var repo = new BookmarksRepository(_connectionString);
            repo.DeleteBookmark(vm.ID);
        }

        [HttpPost]
        [Route("updatebookmark")]
        public void UpdateBookmark(UpdateTitleViewModel vm)
        {
            var repo = new BookmarksRepository(_connectionString);
            repo.UpdateBookmark(vm.BookmarkID, vm.Title);
        }

        [HttpGet]
        [Route("gettop")]
        public List<TopBookmark> GetTop()
        {
            var repo = new BookmarksRepository(_connectionString);
            return repo.GetTopBookmarks();
        }

 	private User GetCurrentUser()
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            return user;
        }
    }
}
