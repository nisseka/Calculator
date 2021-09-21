﻿using System;

namespace Calculator
{
    class Program
    {
        static string[] MenuItems = { "Exit", "Addition", "Subtraction", "Multiplication", "Division"
        };
        static string[] ValueStr = { "Value 1", "Value 2" };

        static void Main(string[] args)
        {
            bool exit = false;
            int MenuItemsCount;
            int i;
            double result, tal1, tal2;
            do
            {
                Console.Clear();

                Console.WriteLine("Welcome to my calculator\n");

                MenuItemsCount = DrawMenu(MenuItems);                 // Display the menu

                i = RequestNumberFromUser_Int("\nEnter selection:");     // The user must enter a value

                if (i > 0 && i < MenuItemsCount)
                {
                    Console.Clear();
                    Console.WriteLine(MenuItems[i]);                // Display Menu Title
                    Console.WriteLine("");

                    tal1 = RequestNumberFromUser_Double($"{ValueStr[0]}?");
                    tal2 = RequestNumberFromUser_Double($"{ValueStr[1]}?");

                    switch (i)          // Act on the selected menu item
                    {
                        case 1:         // Addition
                            result = Addition(tal1, tal2);
                            break;
                        case 2:         // Addition
                            result = Subtraction(tal1, tal2);
                            break;
                        default:
                            result = 0;
                            break;
                    }

                    Console.WriteLine("Result: {0}",result);
                }
                else if (i == 0)
                    exit = true;

                if (!exit)
                {
                    WaitForUserPressedAKey();
                }

            } while (!exit);
        }

        static double Addition(double tal1, double tal2)
        {
            double r;

            r = tal1 + tal2;
            return r;
        }

        static double Subtraction(double tal1, double tal2)
        {
            double r;

            r = tal1 - tal2;
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
            Console.Write("\nTryck en tangent för att fortsätta..");
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
