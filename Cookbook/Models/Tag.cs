using System.Collections.Generic;

namespace Cookbook.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Recipes = new HashSet<RecipeTag>();
        }

        public int TagId { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<RecipeTag> Recipes { get; }
    }
}