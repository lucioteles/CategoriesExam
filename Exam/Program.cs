using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exam
{
    class Program
    {
        private static List<Categories> lstCat = new List<Categories>();

        static void Main(string[] args)
        {
            BuildList();

            string ret = "";
            Console.WriteLine("Welcome! Press 'Q' to Quit!");
            do
            {
                Console.WriteLine("Please inform a category ID:");
                ret = Console.ReadLine();
                try
                {
                    Regex regex = new Regex("^[0-9]+$");
                    if (regex.IsMatch(ret))
                    {
                        var cat = FindCategoryById(Convert.ToInt32(ret));
                        Console.WriteLine("ParentCategoryID=" + cat.ParentCategoryId.ToString() + ", Name=" + cat.Name + ", Keywords=" + cat.Keyword);

                        Console.WriteLine("Now, please inform a level: ");
                        ret = Console.ReadLine();
                        if (regex.IsMatch(ret))
                        {
                            //Console.WriteLine("Output: " + FindCategoryByLevel(Convert.ToInt32(ret)));
                            var categ = FindCategoriesByLevel(Convert.ToInt32(ret));
                            string output = "";
                            foreach (var c in categ)
                            {
                                output += c.CategoryId.ToString() + ",";
                            }

                            Console.WriteLine("Output: " + (String.IsNullOrEmpty(output) ? "" : output.Substring(0, output.Length - 1)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (ret.ToUpper() != "Q");
        }

        private static void BuildList()
        {
            Categories cat = new Categories();

            cat.CategoryId = 100;
            cat.ParentCategoryId = -1;
            cat.Name = "Business";
            cat.Keyword = "Money";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 200;
            cat.ParentCategoryId = -1;
            cat.Name = "Tutoring";
            cat.Keyword = "Teaching";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 101;
            cat.ParentCategoryId = 100;
            cat.Name = "Accounting";
            cat.Keyword = "Taxes";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 102;
            cat.ParentCategoryId = 100;
            cat.Name = "Taxation";
            cat.Keyword = "";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 201;
            cat.ParentCategoryId = 200;
            cat.Name = "Computer";
            cat.Keyword = "";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 103;
            cat.ParentCategoryId = 101;
            cat.Name = "Corporate Tax";
            cat.Keyword = "";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 202;
            cat.ParentCategoryId = 201;
            cat.Name = "Operating System";
            cat.Keyword = "";
            lstCat.Add(cat);

            cat = new Categories();
            cat.CategoryId = 109;
            cat.ParentCategoryId = 101;
            cat.Name = "Small Business Tax";
            cat.Keyword = "";
            lstCat.Add(cat);


        }

        private static Categories FindCategoryById(int id)
        {
            var cat = lstCat.Where(x => x.CategoryId == id).FirstOrDefault();
            if (cat == null)
                throw new Exception("Category doesn't exist");

            if (String.IsNullOrWhiteSpace(cat.Keyword))
                cat.Keyword = FindCategoryById(cat.ParentCategoryId).Keyword;

            return cat;
        }

        private static List<Categories> FindCategoriesByLevel(int n)
        {
            List<Categories> lstCategories = new List<Categories>();

            if (n == 1)
                lstCategories = lstCat.Where(x => x.ParentCategoryId == -1).ToList();
            else
                lstCategories.AddRange(FindChilds(FindCategoriesByLevel(n - 1)));


            return lstCategories;
        }

        private static List<Categories> FindChilds(List<Categories> cat)
        {
            var lstChild = new List<Categories>();
            foreach (var c in cat)
            {
                lstChild.AddRange(lstCat.Where(x => x.ParentCategoryId == c.CategoryId));
            }

            return lstChild;
        }
    }
}
