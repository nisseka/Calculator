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
    class Program
    {
        static string[] MenuItemTitles = { "Addition", "Subtraction", "Multiplication", "Division", "Square root", "Pow" };
        static string[] MenuItems = { "Exit", "+", "-", "*", "/", "Sqrt","Pow"};
        static string[] ValueStr = { "Value 1", "Value 2" };

        static void Main(string[] args)
        {
            bool exit = false;
            int MenuItemsCount;
            int i;
            double result=0, value1=0, value2=0;
            string result_title;

            do
            {
                Console.Clear();

                Console.WriteLine("Calculator\n");

                Console.WriteLine("{0,20}\n",result);    // Print previous result

                MenuItemsCount = DrawMenu(MenuItems);                 // Display the menu

                i = RequestNumberFromUser_Int("\nEnter selection:");     // The user must enter a value

                if (i > 0 && i < MenuItemsCount)
                {
//                    Console.Clear();
                    Console.WriteLine(MenuItemTitles[i-1]);                // Display Menu Title
                    Console.WriteLine("");

                    value1 = RequestNumberFromUser_Double($"{ValueStr[0]}?");
                    if (i != 5)      // If the user selected 5, Square root, only 1 value to get from the user
                        value2 = RequestNumberFromUser_Double($"{ValueStr[1]}?");

                    switch (i)          // Act on the selected menu item
                    {
                        case 1:         // Addition
                            result = Addition(value1, value2,out result_title);
                            break;
                        case 2:         // Subtraction
                            result = Subtraction(value1, value2, out result_title);
                            break;
                        case 3:         // Multiplication
                            result = Multiplication(value1, value2, out result_title);
                            break;
                        case 4:         // Division
                            result = Division(value1, value2, out result_title);
                            break;
                        case 5:         // Square root
                            result = SquareRoot(value1, out result_title);
                            break;
                        case 6:         // Pow
                            result = Pow(value1, value2, out result_title);
                            break;
                        default:
                            result = 0;
                            result_title = "";
                            break;
                    }

                    if (result_title.Length > 0)
                        Console.WriteLine("{0} = {1}",result_title,result);    // Print the result if a valid result exists
                    
                    if (!exit)
                    {
                        WaitForUserPressedAKey();
                    }
               }
                else if (i == 0)        // User selected menu item 0, exit the program
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

        static double Division(double numerator, double denominator, out string result_title)
        {
            double r;

            if (denominator != 0)
            {
                result_title = String.Format("{0} / {1}", numerator, denominator);
                r = numerator / denominator;
            } 
            else
            {
                result_title = "";
                r = 0;
                Console.WriteLine("Error! The denominator is 0! Can't divide with 0\n");
            }
            return r;
        }

        static double SquareRoot(double value, out string result_title)
        {
            double r;
            result_title = String.Format("Sqrt({0})", value);

            r = Math.Sqrt(value);
            return r;
        }

        static double Pow(double value1, double value2, out string result_title)
        {
            double r;

            result_title = String.Format("{0} ^ {1}", value1, value2);
            r = Math.Pow(value1,value2);
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
    * Outputs a title text specified by DisplayText in the console and waits for the user to enter a number. If WaitForValidNumber is true, the function 
    * doesn't return until a valid number has been entered.
    * 
    * returns:    The number in Integer format
    * 
*/
        static int RequestNumberFromUser_Int(string DisplayText, bool WaitForValidNumber = true)
        {
            int r = 0;
            string s;
            bool exit;
            do
            {
                s = RequestStringFromUser(DisplayText);
                exit = int.TryParse(s, out r);
 
            } while (!exit && WaitForValidNumber);
            return r;
        }
       
/*
    * Function:    RequestNumberFromUser_Double
    * 
    * Outputs a title text specified by DisplayText in the console and waits for the user to enter a number. If WaitForValidNumber is true, the function 
    * doesn't return until a valid number has been entered.
    * 
    * returns:    The number in Double format

*/
        static double RequestNumberFromUser_Double(string DisplayText, bool WaitForValidNumber = true)
        {
            double r = 0;
            string s;
            bool exit;
            do
            {
                s = RequestStringFromUser(DisplayText);
                exit = double.TryParse(s, out r);
 
            } while (!exit && WaitForValidNumber);
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
    }
}
