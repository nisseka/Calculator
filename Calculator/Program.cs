using System;

/*
 * Assignment #1, Calculator 
 * 
 * by
 * 
 * Stefan Sundbeck
*/

namespace Calculator
{
    enum MenuItemTypes { Exit, Addition, Subtraction, Multiplication, Division, SquareRoot, Pow, Pow10, Sin, Cos, Log, ClearResult };

    class Program
    {
        static string[] ValueStr = { "Value 1", "Value 2" };

        static string[] FunctionTitles = { 
                                           "Addition", 
                                           "Subtraction",
                                           "Multiplication",
                                           "Division", 
                                           "Square root",
                                           "Pow",
                                           "10^",
                                           "Sinus",
                                           "Cosinus",
                                           "Log",
                                           "Clear Result" };
        static string[] FunctionDescriptions = {
                                             $"{ValueStr[0]} + {ValueStr[1]}",
                                             $"{ValueStr[0]} - {ValueStr[1]}",
                                             $"{ValueStr[0]} * {ValueStr[1]}",
                                             $"{ValueStr[0]} / {ValueStr[1]}", 
                                             $"Sqrt({ValueStr[0]})",
                                             $"{ValueStr[0]} ^ {ValueStr[1]}",
                                             $"10^{ValueStr[0]}",
                                             $"Sin({ValueStr[0]}) {ValueStr[0]} in degrees",
                                             $"Cos({ValueStr[0]}) {ValueStr[0]} in degrees",
                                             $"Log({ValueStr[0]})",
                                             ""
        };

        static string[] MenuItemStrs = { "Exit", "+", "-", "*", "/", "Sqrt", "Pow", "10^", "Sin","Cos","Log","Clear"};
        static string empty_string = "";

        static void Main(string[] args)
        {
            bool exit = false;
            int MenuItemsCount,i;
            MenuItemTypes SelectedMenuItem;
            double result=0, value1=0, value2=0;
            string result_title=empty_string;

            do
            {
                Console.Clear();

                Console.WriteLine("Calculator\n");

                Console.WriteLine("{0,20}\n",result);                                                   // Print previous result

                MenuItemsCount = DrawMenu(MenuItemStrs);                                                // Display the menu

                i=RequestNumberFromUser_Int("\nEnter selection:");                                      // The user must enter a value which menu item it wants
                SelectedMenuItem = (MenuItemTypes) i;

                if (SelectedMenuItem > MenuItemTypes.Exit && SelectedMenuItem <= MenuItemTypes.ClearResult)
                {
                    i--;
                    Console.WriteLine(FunctionTitles[i]);                                               // Display a title
                    Console.WriteLine("\nUsage: {0}\n",FunctionDescriptions[i]);                        // Display a description

                    if (SelectedMenuItem != MenuItemTypes.ClearResult)                                  // If selected menu item is Clear, dont input any values 
                    {
                        value1 = RequestNumberFromUser_Double($"{ValueStr[0]}?", result);
                        if (SelectedMenuItem != MenuItemTypes.SquareRoot &&
                            SelectedMenuItem != MenuItemTypes.Cos &&
                            SelectedMenuItem != MenuItemTypes.Pow10 &&
                            SelectedMenuItem != MenuItemTypes.Log &&
                            SelectedMenuItem != MenuItemTypes.Sin)                                      // If selected menu item is Square root or Sin or Cos or Log or Pow10
                        {                                                                               // only 1 value to get from the user
                            value2 = RequestNumberFromUser_Double($"{ValueStr[1]}?");
                        }
                    }

                    switch (SelectedMenuItem)                                                           // Act on the selected menu item
                    {
                        case MenuItemTypes.Addition:                                                    // Addition
                            result = Addition(value1, value2,out result_title);
                            break;
                        case MenuItemTypes.Subtraction:                                                 // Subtraction
                            result = Subtraction(value1, value2, out result_title);
                            break;
                        case MenuItemTypes.Multiplication:                                              // Multiplication
                            result = Multiplication(value1, value2, out result_title);
                            break;
                        case MenuItemTypes.Division:                                                    // Division
                            Division(value1, value2, ref result, ref result_title);
                            break;
                        case MenuItemTypes.SquareRoot:                                                  // Square root
                            SquareRoot(value1, ref result,ref result_title);
                            break;
                        case MenuItemTypes.Pow10:                                                       // 10^
                            result = Pow(10, value1, out result_title);
                            break;
                        case MenuItemTypes.Pow:                                                         // Pow
                            result = Pow(value1, value2, out result_title);
                            break;
                        case MenuItemTypes.Sin:                                                         // Sinus
                            result = Sinus(value1, out result_title);
                            break;
                        case MenuItemTypes.Cos:                                                         // Cosinus
                            result = Cosinus(value1, out result_title);
                            break;
                        case MenuItemTypes.Log:                                                         // Log
                            result = Log(value1, out result_title);
                            break;
                        case MenuItemTypes.ClearResult:                                                 // Clear result
                        default:
                            result = 0;
                            result_title = empty_string;
                            break;
                    }

                    if (result_title.Length > 0)
                        Console.WriteLine("{0} = {1}",result_title,result);                             // Print the result if a valid result exists
                    
                    if (SelectedMenuItem != MenuItemTypes.ClearResult)                                  // Only wait for a keypress if the selected menu item 
                    {                                                                                   // isn't ClearResult
                        WaitForUserPressedAKey();
                    }
               } else if (SelectedMenuItem == MenuItemTypes.Exit)                                       // User selected menu item Exit (0), exit the program
                    exit = true;


            } while (!exit);
        }

        static double Addition(double value1, double value2, out string result_title)
        {
            double r;

            result_title = String.Format("{0} + {1}",value1, value2);
            r = value1 + value2;
            return r;
        }

        static double Subtraction(double value1, double value2, out string result_title)
        {
            double r;

            result_title = String.Format("{0} - {1}", value1, value2);
            r = value1 - value2;
            return r;
        }

        static double Multiplication(double value1, double value2, out string result_title)
        {
            double r;

            result_title = String.Format("{0} * {1}", value1, value2);
            r = value1 * value2;
            return r;
        }

        static bool Division(double numerator, double denominator, ref double result, ref string result_title)
        {
            bool r;

            if (denominator != 0)
            {
                result_title = String.Format("{0} / {1}", numerator, denominator);
                result = numerator / denominator;
                r = true;
            }
            else
            {
                result_title = empty_string;
                Console.WriteLine("Error! The denominator is 0! Can't divide with 0\n");
                r = false;
            }
            return r;
        }

        static bool SquareRoot(double value, ref double result,ref string result_title)
        {
            bool r;

            if (value >= 0)
            {
                result_title = String.Format("Sqrt({0})", value);
                result = Math.Sqrt(value);
                r = true;
            } else
	    {
                result_title = empty_string;
                Console.WriteLine("Error! Can't take the square root of a negative number!\n");
                r = false;
            }
            return r;
        }

        static double Pow(double value1, double value2, out string result_title)
        {
            double r;

            result_title = String.Format("{0} ^ {1}", value1, value2);
            r = Math.Pow(value1,value2);
            return r;
        }

        static double Sinus(double value, out string result_title)
	{
            double r;

            result_title = String.Format("Sin({0})", value);

            r = Math.Sin(deg2rad(value));                       // Use deg2rad to convert the angle in value to radians
            return r;
        }

        static double Cosinus(double value, out string result_title)
        {
            double r;

            result_title = String.Format("Cos({0})", value);

            r = Math.Cos(deg2rad(value));                       // Use deg2rad to convert the angle in value to radians
            return r;
        }

        static double Log(double value, out string result_title)
        {
            double r;

            result_title = String.Format("Log({0})", value);

            r = Math.Log10(value);
            return r;
        }


/*
    * Function:    RequestStringFromUser
    * 
    * Outputs a title text specified by DisplayText in the console and records the users keypresses until return key is pressed
    * 
    * returns:    The recorded text string
*/
        static string RequestStringFromUser(string DisplayText)
        {
            string r;
            Console.Write("{0} ", DisplayText);
            r = Console.ReadLine();
            return r;
        }

/*
    * Function:    RequestNumberFromUser_Int
    * 
    * Outputs a title text specified by DisplayText in the console and waits for the user to enter a number. If DefaultValue isn't 0, display it to 
    * the user in parentheses, for example (56). Then if the user doesn't enter anything (only presses return), use the value specified in DefaultValue
    * 
    * returns:    The number in Integer format
    * 
*/
        static int RequestNumberFromUser_Int(string DisplayText, int DefaultValue = 0)
        {
            int r = 0;
            string s;
            bool exit;

            if (DefaultValue != 0)
            {
                DisplayText += String.Format(" (Press only enter to use {0})", DefaultValue);
            }

            do
            {
                s = RequestStringFromUser(DisplayText);

                if (s.Length == 0 && DefaultValue != 0)
                {
                    r = DefaultValue;
                    exit = true;
                }
                else
                    exit = int.TryParse(s, out r);
 
            } while (!exit);
            return r;
        }
       
/*
    * Function:    RequestNumberFromUser_Double
    * 
    * Outputs a title text specified by DisplayText in the console and waits for the user to enter a number. If DefaultValue isn't 0, display it to 
    * the user in parentheses, for example (56). Then if the user doesn't enter anything (only presses return), use the value specified in DefaultValue
    * 
    * returns:    The number in Double format

*/
        static double RequestNumberFromUser_Double(string DisplayText, double DefaultValue = 0)
        {
            double r = 0;
            string s;
            bool exit;

	    if (DefaultValue != 0)
	    {
                DisplayText += String.Format(" (Press only enter to use {0})", DefaultValue);
            }

            do
            {
                s = RequestStringFromUser(DisplayText);
                if (s.Length == 0 && DefaultValue != 0)
                {
                    r = DefaultValue;
                    exit = true;
                } else
                    exit = double.TryParse(s, out r);
 
            } while (!exit);
            return r;
        }



/*
    * Function:    WaitForUserPressedAKey
    * 
    * Outputs an information text and waits for the user to press a key to continue
*/
        static void WaitForUserPressedAKey()
        {
            Console.Write("\nPress any key to continue..");
            Console.ReadKey();
        }

/*
    *  Function:    DrawMenu
    *  
    *  Outputs a menu specified by MenuItems to the console
    *  
    *  returns:   The number of menuitems drawn  
*/
        static int DrawMenu(string[] MenuItems)
        {
            int i;
            Console.WriteLine("Menu:");

            i = 0;
            foreach (string menuitem in MenuItems)
            {
                Console.WriteLine("{0,5}: {1}", i, menuitem);
                i++;
            }
            return i;   // Return the number of menuitems drawn;
        }

/*
    *   Function:    deg2rad
    *
    *   Converts an angle in degrees to radians
    *   
    *   returns:    angle in radians
    *
*/
        static double deg2rad(double AngleInDegrees)
        {
            return (AngleInDegrees / 180) * Math.PI; 
        }
    }

}
