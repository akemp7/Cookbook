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
    public class RecipesController : Controller
    {
        private readonly CookbookContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(UserManager<ApplicationUser> userManager, CookbookContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id);
            return View(userRecipes);
        }

        public ActionResult Create()
        {
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Description");
            return View();
        }

        //updated Create post method
        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe, int TagId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            recipe.User = currentUser;
            _db.Recipes.Add(recipe);
            if (TagId != 0)
            {
                _db.RecipeTag.Add(new RecipeTag() { RecipeId = recipe.RecipeId, TagId = TagId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}