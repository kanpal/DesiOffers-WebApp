using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class CategoryViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long? ParentID { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category)
        {
            ID = category.ID;
            Name = category.Name;
            ParentID = category.ParentID;
        }
    }
}
