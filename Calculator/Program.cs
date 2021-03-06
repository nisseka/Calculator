using System;
using System.Text;

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

    public class Program
    {
        static string[] ValueStr = { "Value 1", "Value 2" };

        static string[] MethodTitles = { 
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
        static string[] MethodDescriptions = {
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
            string resultTitleString=empty_string;

            do
            {
                Console.Clear();

                Console.WriteLine("Calculator\n");

                Console.WriteLine("{0,20}\n",result);                                                   // Print previous result

                MenuItemsCount = DrawMenu(MenuItemStrs);                                                // Display the menu

                i=PrintStringAndRequestNumberFromUser_Int("\nEnter selection:");                                      // The user must enter a value which menu item it wants
                SelectedMenuItem = (MenuItemTypes) i;

                if (SelectedMenuItem > MenuItemTypes.Exit && SelectedMenuItem <= MenuItemTypes.ClearResult)
                {
                    i--;
                    Console.WriteLine(MethodTitles[i]);                                               // Display a title
                    Console.WriteLine("\nUsage: {0}\n",MethodDescriptions[i]);                        // Display a description

                    if (SelectedMenuItem != MenuItemTypes.ClearResult)                                  // If selected menu item is Clear, dont input any values 
                    {
                        value1 = PrintStringAndRequestNumberFromUser_Double($"{ValueStr[0]}?", result);
                        if (SelectedMenuItem != MenuItemTypes.SquareRoot &&
                            SelectedMenuItem != MenuItemTypes.Cos &&
                            SelectedMenuItem != MenuItemTypes.Pow10 &&
                            SelectedMenuItem != MenuItemTypes.Log &&
                            SelectedMenuItem != MenuItemTypes.Sin)                                      // If selected menu item is Square root or Sin or Cos or Log or Pow10
                        {                                                                               // only 1 value to get from the user
                            value2 = PrintStringAndRequestNumberFromUser_Double($"{ValueStr[1]}?");
                        }
                    }

                    switch (SelectedMenuItem)                                                           // Act on the selected menu item
                    {
                        case MenuItemTypes.Addition:                                                    // Addition
                            result = Addition(value1, value2,out resultTitleString);
                            break;
                        case MenuItemTypes.Subtraction:                                                 // Subtraction
                            result = Subtraction(value1, value2, out resultTitleString);
                            break;
                        case MenuItemTypes.Multiplication:                                              // Multiplication
                            result = Multiplication(value1, value2, out resultTitleString);
                            break;
                        case MenuItemTypes.Division:                                                    // Division
                            if (Division(value1, value2, ref result, ref resultTitleString) == false)
			    {
                                Console.WriteLine("Error! The denominator is 0! Can't divide with 0\n");
                            }
                            break;
                        case MenuItemTypes.SquareRoot:                                                  // Square root
                            if (SquareRoot(value1, ref result,ref resultTitleString) == false)
			    {
                                Console.WriteLine("Error! Can't take the square root of a negative number!\n");
                            }
                            break;
                        case MenuItemTypes.Pow10:                                                       // 10^
                            result = Pow(10, value1, out resultTitleString);
                            break;
                        case MenuItemTypes.Pow:                                                         // Pow
                            result = Pow(value1, value2, out resultTitleString);
                            break;
                        case MenuItemTypes.Sin:                                                         // Sinus
                            result = Sinus(value1, out resultTitleString);
                            break;
                        case MenuItemTypes.Cos:                                                         // Cosinus
                            result = Cosinus(value1, out resultTitleString);
                            break;
                        case MenuItemTypes.Log:                                                         // Log
                            result = Log(value1, out resultTitleString);
                            break;
                        case MenuItemTypes.ClearResult:                                                 // Clear result
                        default:
                            result = 0;
                            resultTitleString = empty_string;
                            break;
                    }

                    if (resultTitleString.Length > 0)
                        Console.WriteLine("{0} = {1}",resultTitleString,result);                        // Print the result if a valid result exists
                    
                    if (SelectedMenuItem != MenuItemTypes.ClearResult)                                  // Only wait for a keypress if the selected menu item 
                    {                                                                                   // isn't ClearResult
                        WaitForUserPressedAKey();
                    }
               } else if (SelectedMenuItem == MenuItemTypes.Exit)                                       // User selected menu item Exit (0), exit the program
                    exit = true;


            } while (!exit);
        }

/*
    * Method:   Addition
    * 
    * Performs an addition.
    * 
    * returns:  value1 + value2
    * 
 */
        public static double Addition(double value1, double value2, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("{0} + {1}",value1, value2);
            result = value1 + value2;
            return result;
        }

/*
    * Method:   Addition
    * 
    * Performs an addition with multiple values in the values array
    * 
    * returns:  values[0] + ... values[N]
    * 
 */        
        public static double Addition(double[] values, out string resultTitleString)
	{
            if (values.Length < 1)
            {
                throw new ArgumentOutOfRangeException("values", "The values array should have atleast 1 element");
            }

            int i;
            int countm1=values.Length - 1;
            double result = 0;
            double value;
            StringBuilder sb = new StringBuilder();

	    for (i = 0; i < values.Length; i++)
	    {
                value = values[i];
                result += value;
                sb.Append(value);

		if (i<countm1)
		{
                    sb.Append(" + ");
		}
	    }

            resultTitleString = sb.ToString();
            return result;
        }

/*
    * Method:   Subtraction
    * 
    * Performs a subtraction.
    * 
    * returns:  value1 - value2
    * 
 */        
        public static double Subtraction(double value1, double value2, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("{0} - {1}", value1, value2);
            result = value1 - value2;
            return result;
        }

/*
    * Method:   Subtraction
    * 
    * Performs a subtraction with multiple values in the values array
    * 
    * returns:  values[0] - ... values[N]
    * 
*/
        public static double Subtraction(double[] values, out string resultTitleString)
        {
            if (values.Length < 2)
            {
                throw new ArgumentOutOfRangeException("values", "The values array should have atleast 2 elements");
            }

            int i;
            int countm1 = values.Length - 1;
            double result = values[0];
            double value;
            StringBuilder sb = new StringBuilder();

            for (i = 0; i < values.Length; i++)
            {
                value = values[i];
                if  (i > 0)
                    result -= value;

                sb.Append(value);

                if (i < countm1)
                {
                    sb.Append(" - ");
                }
            }

            resultTitleString = sb.ToString();
            return result;
        }

        public static double Multiplication(double value1, double value2, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("{0} * {1}", value1, value2);
            result = value1 * value2;
            return result;
        }
/*
    * Method:   Division
    * 
    * Performs a division, numerator / denominator
    * 
    * returns:    
    *           True:   Division succeded; the variable referenced bv parameter result will contain the result
    *           False:  Division failed; denominator is 0, divison with 0
    * 
*/
        public static bool Division(double numerator, double denominator, ref double result, ref string resultTitleString)
        {
            bool success;

            if (denominator != 0)
            {
                resultTitleString = String.Format("{0} / {1}", numerator, denominator);
                result = numerator / denominator;
                success = true;
            }
            else
            {
                resultTitleString = empty_string;
                success = false;
            }
            return success;
        }

/*
    * Method:   SquareRoot
    * 
    * Takes the SquareRoot of number value
    * 
    * returns:    
    *           True:   SquareRoot succeded; the variable referenced bv parameter result will contain the result
    *           False:  SquareRoot of a negative number; not allowed
    * 
*/
        public static bool SquareRoot(double value, ref double result,ref string resultTitleString)
        {
            bool success;

            if (value >= 0)
            {
                resultTitleString = String.Format("Sqrt({0})", value);
                result = Math.Sqrt(value);
                success = true;
            } else
	    {
                resultTitleString = empty_string;
                success = false;
            }
            return success;
        }

/*
    * Method:   Pow
    * 
    * returns:  The number value1 raised to the power value2.
*/
        public static double Pow(double value1, double value2, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("{0} ^ {1}", value1, value2);
            result = Math.Pow(value1,value2);
            return result;
        }

/*
    * Method:   Sinus
    * 
    * returns:  The sine of the specified angle in degrees
*/
        public static double Sinus(double value, out string resultTitleString)
	{
            double result;

            resultTitleString = String.Format("Sin({0})", value);

            result = Math.Sin(Deg2Rad(value));                       // Use Deg2Rad to convert the angle in value to radians
            return result;
        }

/*
    * Method:   Cosinus
    * 
    * returns:  The cosine of the specified angle in degrees
*/
        public static double Cosinus(double value, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("Cos({0})", value);

            result = Math.Cos(Deg2Rad(value));                       // Use Deg2Rad to convert the angle in value to radians
            return result;
        }

/*
    * Method:   Log
    * 
    * returns:  The base 10 logarithm of the number value
*/
        public static double Log(double value, out string resultTitleString)
        {
            double result;

            resultTitleString = String.Format("Log({0})", value);

            result = Math.Log10(value);
            return result;
        }


/*
    * Method:   PrintStringAndRequestStringFromUser
    * 
    * Outputs a title text specified by displayText in the console and records the users keypresses until return key is pressed
    * 
    * returns:    The recorded text string
*/
        static string PrintStringAndRequestStringFromUser(string displayText)
        {
            Console.Write("{0} ", displayText);
            return Console.ReadLine(); ;
        }

/*
    * Metod:    PrintStringAndRequestNumberFromUser_Int
    * 
    * Outputs a title text specified by displayText in the console and waits for the user to enter a number. If defaultValue isn't 0, display it to 
    * the user in parentheses, for example (56). Then if the user doesn't enter anything (only presses return), use the value specified in defaultValue
    * 
    * returns:    The number in Integer format
    * 
*/
        static int PrintStringAndRequestNumberFromUser_Int(string displayText, int defaultValue = 0)
        {
            int r = 0;
            string str;
            bool exit;

            if (defaultValue != 0)
            {
                displayText += String.Format(" (Press only enter to use {0})", defaultValue);
            }

            do
            {
                str = PrintStringAndRequestStringFromUser(displayText);

                if (str.Length == 0 && defaultValue != 0)
                {
                    r = defaultValue;
                    exit = true;
                }
                else
                    exit = int.TryParse(str, out r);
 
            } while (!exit);
            return r;
        }
       
/*
    * Method:    PrintStringAndRequestNumberFromUser_Double
    * 
    * Outputs a title text specified by displayText in the console and waits for the user to enter a number. If defaultValue isn't 0, display it to 
    * the user in parentheses, for example (56). Then if the user doesn't enter anything (only presses return), use the value specified in defaultValue
    * 
    * returns:    The number in Double format

*/
        static double PrintStringAndRequestNumberFromUser_Double(string displayText, double defaultValue = 0)
        {
            double r = 0;
            string str;
            bool exit;

	    if (defaultValue != 0)
	    {
                displayText += String.Format(" (Press only enter to use {0})", defaultValue);
            }

            do
            {
                str = PrintStringAndRequestStringFromUser(displayText);
                if (str.Length == 0 && defaultValue != 0)
                {
                    r = defaultValue;
                    exit = true;
                } else
                    exit = double.TryParse(str, out r);
 
            } while (!exit);
            return r;
        }



/*
    * Method:    WaitForUserPressedAKey
    * 
    * Outputs an information text and waits for the user to press a key to continue
*/
        static void WaitForUserPressedAKey()
        {
            Console.Write("\nPress any key to continue..");
            Console.ReadKey();
        }

/*
    *  Method:    DrawMenu
    *  
    *  Outputs a menu specified by menuItems to the console
    *  
    *  returns:   The number of menuitems drawn  
*/
        static int DrawMenu(string[] menuItems)
        {
            int i;
            Console.WriteLine("Menu:");

            i = 0;
            foreach (string menuitem in menuItems)
            {
                Console.WriteLine("{0,5}: {1}", i, menuitem);
                i++;
            }
            return i;   // Return the number of menuitems drawn;
        }

/*
    *   Method:    Deg2Rad
    *
    *   Converts an angle in degrees to radians
    *   
    *   returns:    angle in radians
    *
*/
        public static double Deg2Rad(double angleInDegrees)
        {
            return (angleInDegrees / 180) * Math.PI; 
        }
    }

}
