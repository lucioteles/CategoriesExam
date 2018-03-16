using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }
    }
}
