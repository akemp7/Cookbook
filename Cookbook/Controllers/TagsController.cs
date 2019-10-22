using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Cookbook.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly CookbookContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TagsController(UserManager<ApplicationUser> userManager, CookbookContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userTags = _db.Tags.Where(entry => entry.User.Id == currentUser.Id);
            return View(userTags);
        }

        public ActionResult Create()
        {
            ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
            return View();
        }

        //updated Create post method
        [HttpPost]
        public async Task<ActionResult> Create(Tag tag, int RecipeId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            tag.User = currentUser;
            _db.Tags.Add(tag);
            if (RecipeId != 0)
            {
                _db.RecipeTag.Add(new RecipeTag() { RecipeId = RecipeId, TagId = tag.TagId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        

    }
}