using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CompositePattern
{
    public abstract class MenuComponent
    {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public virtual string Price { get; set; }

        public virtual void PrintMenuList()
        {
            throw new NotSupportedException();
        }

        public virtual void AddItem(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }
    }

    public class MainMenu : MenuComponent
    {
        public sealed override string Name { get; set; }
        public sealed override string Description { get; set; }

        private readonly List<MenuComponent> _menuCollection = new List<MenuComponent>();

        public MainMenu(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override void AddItem(MenuComponent menuComponent)
        {
            _menuCollection.Add(menuComponent);
        }

        public override void PrintMenuList()
        {
            foreach (var menuComponent in _menuCollection)
            {
                var mainMenu = menuComponent as MainMenu;
                if (mainMenu != null)
                {
                    Console.WriteLine($"\n{mainMenu.Name} : {mainMenu.Description}");
                }
               
                menuComponent.PrintMenuList();
            }
        }
    }

    public class SubMenu : MenuComponent
    {
        public sealed override string Name { get; set; }
        public sealed override string Description { get; set; }
        public sealed override string Price { get; set; }

        public SubMenu(string name, string description, string price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public override void PrintMenuList()
        {
            Console.WriteLine($"\t{ Name } : {Description} : {Price}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu("Vishal's Hotel", "Enjoy your meal");

            var vegMainMenu = new MainMenu("Veg", "Veg Menu");
            vegMainMenu.AddItem(new SubMenu("Palak Paneer", "Paneer cooked in fresh palak", "Rs.90"));
            vegMainMenu.AddItem(new SubMenu("Paneer Pasanda", "Paneer cooked in green vegs", "Rs.100"));
            vegMainMenu.AddItem(new SubMenu("Paneer Spicy", "Paneer with deep fried gravy and all the spices", "Rs.120"));
            vegMainMenu.AddItem(new SubMenu("Paneer Mahhni", "Sweet Paneer with makhni and green herbs", "Rs.150"));

            var nonVegMainMenu = new MainMenu("Non Veg", "Non Veg Menu");
            nonVegMainMenu.AddItem(new SubMenu("Chicken Paneer", "Paneer cooked in fresh palak and Chicken", "Rs.190"));
            nonVegMainMenu.AddItem(new SubMenu("Chicken Pasanda", "Chicken cooked in green vegs", "Rs.100"));
            nonVegMainMenu.AddItem(new SubMenu("Chicken Spicy", "Chicken with deep fried gravy", "Rs.120"));
            nonVegMainMenu.AddItem(new SubMenu("Chicken Mahhni", "Sweet Chicken with makhni and green herbs", "Rs.150"));

            var soupMainMenu = new MainMenu("Soup", "All kinds of soups");
            var vegSoupMainMenu = new MainMenu("Veg soup", "Delicious VEG soup");
            vegSoupMainMenu.AddItem(new SubMenu("VegManchow", "Sweet and Salty", "Rs. 120"));

            var nonVegSoupMainMenu = new MainMenu("Non Veg SOUP", "Delicious Non Veg soup");
            nonVegSoupMainMenu.AddItem(new SubMenu("Non Veg Soup", "Sweet and Salty", "Rs. 150"));

            soupMainMenu.AddItem(vegSoupMainMenu);
            soupMainMenu.AddItem(nonVegSoupMainMenu);


            mainMenu.AddItem(vegMainMenu);
            mainMenu.AddItem(nonVegMainMenu);
            mainMenu.AddItem(soupMainMenu);

            mainMenu.PrintMenuList();

        }
    }
}
