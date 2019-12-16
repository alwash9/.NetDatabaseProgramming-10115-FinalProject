using NorthwindDB_Console_Final.Logging;
using NorthwindDB_Console_Final.Models;
using NorthwindDB_Console_Final.Utility;
using System;
using System.Linq;


namespace NorthwindDB_Console_Final.Menus
{
    class EditCategoryMenu : ValidationOptions, IMenu
    {
        public void Start()
        {
            do
            {
                Console.WriteLine("\tEDIT CATEGORY\n");

                Console.WriteLine("(1) Display all Categories");
                Console.WriteLine("(2) Display a specific Category and its related Products");
                Console.WriteLine("(3) Search for a specific category to edit (By the Category Name)");
                Console.WriteLine("(4) Search for a specific category to edit (By the Category ID)");
                Console.WriteLine("(5) Search for a specific category to edit (By the Category Description)");

                Console.WriteLine("Press ESC to go back");

                ConsoleKeyInfo keypress = Console.ReadKey();
                Console.WriteLine();

                NLogger logging = new NLogger();
                DisplayOptions disOp = new DisplayOptions();

                //Display all categories
                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    disOp.DisplayAllCategories();

                }

                //Display specific category and products
                else if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("Which category are you looking for (Name)?");

                    var results = db.SearchCategory(Console.ReadLine(), true); //Return List?
                    var rList = results.ToList();



                    if (results.Count() == 0)
                    {
                        logging.Log("WARN", "No results found. Try Again.");
                    }
                    else
                    {
                        disOp.DisplayCategories(rList);

                        disOp.DisplayCategoryAndProducts(rList.FirstOrDefault());
                    }
                }

                //Edit by Name
                else if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("What is the name of the category that you are looking for?");
                    string toFind = Console.ReadLine();


                    var results = db.SearchCategory(toFind, true);
                    var rList = results.ToList();

                    if (results.Count() == 0)
                    {
                        //logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        disOp.DisplayCategories(rList);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {

                                EditCategory(rList[0]);
                                break;
                            }
                            else if (keypress2.Key == ConsoleKey.N)
                            {

                                break;
                            }
                            else
                            {
                                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                            }

                        } while (true);
                    }
                    else
                    {
                        int row = 0;

                        foreach (var category in rList)
                        {
                            Console.WriteLine("Row: {0}", row++);
                            disOp.DisplayCategory(category);
                            Console.WriteLine("");
                        }


                        Console.WriteLine("Which Category would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = IntValidation(choice);


                        EditCategory(rList[vInput - 1]);

                    }

                }

                //Edit by ID
                else if (keypress.Key == ConsoleKey.D4 || keypress.Key == ConsoleKey.NumPad4)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("What is the ID of the category that you are looking for?");
                    int toFind = IntValidation(Console.ReadLine());
                      


                    var results = db.SearchCategory(toFind);
                    var rList = results.ToList();

                    if (results.Count() == 0)
                    {
                        //logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        disOp.DisplayCategories(rList);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {

                                EditCategory(rList[0]);
                                break;
                            }
                            else if (keypress2.Key == ConsoleKey.N)
                            {

                                break;
                            }
                            else
                            {
                                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                            }

                        } while (true);
                    }
                    //Should never get used because ID is unique. If it does, something is terribly wrong with the database.
                    else
                    {
                        int row = 0;

                        foreach (var category in rList)
                        {
                            Console.WriteLine("Row: {0}", row++);
                            disOp.DisplayCategory(category);
                            Console.WriteLine("");
                        }


                        Console.WriteLine("Which Category would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = IntValidation(choice);


                        EditCategory(rList[vInput - 1]);

                    }

                }

                //Edit by description
                else if (keypress.Key == ConsoleKey.D5 || keypress.Key == ConsoleKey.NumPad5)
                {
                    NorthwindContext db = new NorthwindContext();
                    Console.WriteLine("What is the Description of the category that you are looking for?");
                    string toFind = Console.ReadLine();



                    var results = db.SearchCategory(toFind, false);
                    var rList = results.ToList();

                    if (results.Count() == 0)
                    {
                        //logging.Log("WARN", "No results found. Try Again.");
                    }
                    else if (results.Count() == 1)
                    {
                        disOp.DisplayCategories(rList);

                        Console.WriteLine("Is this the correct Product?");
                        Console.WriteLine("If yes, Press Y. If No, Press N.");

                        do
                        {

                            ConsoleKeyInfo keypress2 = Console.ReadKey();
                            Console.WriteLine();

                            if (keypress2.Key == ConsoleKey.Y)
                            {

                                EditCategory(rList[0]);
                                break;
                            }
                            else if (keypress2.Key == ConsoleKey.N)
                            {

                                break;
                            }
                            else
                            {
                                logging.Log("ERROR", "A valid key was not pressed. Please press (Y)es or (N)o.");
                            }

                        } while (true);
                    }
                    //Should never get used because ID is unique. If it does, something is terribly wrong with the database.
                    else
                    {
                        int row = 0;

                        foreach (var category in rList)
                        {
                            Console.WriteLine("Row: {0}", row++);
                            disOp.DisplayCategory(category);
                            Console.WriteLine("");
                        }


                        Console.WriteLine("Which Category would you like to edit?");
                        Console.Write("Enter the Row number: \t");

                        string choice = Console.ReadLine();
                        int vInput = IntValidation(choice);


                        EditCategory(rList[vInput - 1]);

                    }

                }
                else if (keypress.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    logging.Log("WARN", "Please press a valid option. Try again.");
                }
            } while (true);


        }

        private void EditCategory(Category category)
        {
            NorthwindContext db = new NorthwindContext();
            ModelInputValidation miv = new ModelInputValidation();
            ConsoleKeyInfo keyPress;
            var change = db.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);

            do
            {

                Console.WriteLine("Which part of the category would you like to edit?");

                Console.WriteLine("(1) Name");
                Console.WriteLine("(2) Description");

                Console.WriteLine("When you are finish editing the product press (0) to save and go back.");
                //Cancel changes?


                keyPress = Console.ReadKey();
                Console.WriteLine();

                if (keyPress.Key == ConsoleKey.D0 || keyPress.Key == ConsoleKey.NumPad0)
                {

                    db.Save();
                    break;
                }

                if (keyPress.Key == ConsoleKey.D1 || keyPress.Key == ConsoleKey.NumPad1)
                {
                    change.CategoryName = miv.CategoryName_Input();
                }

                if (keyPress.Key == ConsoleKey.D2 || keyPress.Key == ConsoleKey.NumPad2)
                {
                    change.Description = miv.CategoryDescription_Input();
                }

            } while (true);

        }

    }
}
