using Microsoft.EntityFrameworkCore;

namespace EntityTreeProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(CatalogContext context = new CatalogContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Catalog books = new Catalog() { Title = "Books" };
                Catalog musik = new Catalog() { Title = "Musik" };
                Catalog video = new Catalog() { Title = "Video" };

                Catalog detectiv = new Catalog() { Title = "Detectiv", Parent = books };
                Catalog history = new Catalog() { Title = "History", Parent = books };

                Catalog historyRussia = new Catalog() { Title = "History Russia", Parent = history };
                Catalog historyForeign = new Catalog() { Title = "History Foreign", Parent = history };

                Catalog rock = new Catalog() { Title = "Rock", Parent = musik };
                Catalog classic = new Catalog() { Title = "Classic", Parent = musik };
                Catalog disco = new Catalog() { Title = "Disco", Parent = musik };

                Catalog action = new Catalog() { Title = "Action", Parent = video };
                Catalog romantic = new Catalog() { Title = "Romantic", Parent = video };
                Catalog horror = new Catalog() { Title = "Horror", Parent = video };
                Catalog comedy = new Catalog() { Title = "Comedy", Parent = video };

                

                context.Catalog.AddRange(books, musik, video, detectiv, history, rock, classic, disco, action, romantic, horror, comedy);
                context.Catalog.AddRange(historyRussia, historyForeign);
                context.SaveChanges();
            }

            using (CatalogContext context = new CatalogContext())
            {
                //var catalog = context.Catalog.ToList();
                //foreach(var item in catalog)
                //    Console.WriteLine($"{item.Id} {item.Title} <- {item.ParentId}");

                var catalog = context.Catalog
                                     .Include(p => p.Children)
                                     //.Where(p => p.ParentId == null)
                                     .ToList();

                foreach (var parent in catalog)
                {
                    //if(parent.ParentId == null)
                    {
                        Console.WriteLine(parent.Title);
                        foreach (var child in parent.Children)
                        {
                            Console.WriteLine("\t" + child.Title);
                            if (child.Children != null)
                                foreach (var childchild in child.Children)
                                    Console.WriteLine("\t\t" + childchild.Title);
                        }
                            
                    }
                }


            }
        }
    }
}