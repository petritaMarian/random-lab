using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace slay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); // read input as a string
            //pb1(input);
            //pb2(input);
            //pb3(input);
            string[] stringNumbers = input.Split(' ');      //this whole paragraph is 
            int[] numbers = new int[stringNumbers.Length];  //for reading a string array
            for (int i = 0; i < numbers.Length; i += 1)     // and convert to an int one
                numbers[i] = Convert.ToInt32(stringNumbers[i]); //could've read it as int
            Console.WriteLine("Cel mai mic numar din sir este " + //from the start but
                pb4(1, numbers, numbers[0], numbers.Length - 1)); // I forgot :sob:
            Console.ReadLine(); 
        }
        // cititi un sir de caractere (incepe cu litera si se temina cu litera,
        // separator de cuvinte e spatiu(1)). numarati cate cuvinte sunt in sir,
        // transformati fiecare cuvant incat sa inceapa cu litera mare si restul mici
        // afisati si numarul de cuvinte
        public static void pb1(string input)
        {
            int words = 0; // variabila pentru cate cuvinte sunt
            if (input[0] >= 'a' && input[0] <= 'z') // verifici daca prima litera/caracter
            {                                       // din sir e litera mica
                words += 1;
                char character = input[0];
                Console.Write((char)(character - 32)); // char(litera mica) - 32 o face mare
            }                                          // ex: ascii a = 97 & A = 65
            else
            {
                words += 1; // also added a word when I had a first letter
                Console.Write(input[0]); // oh and btw without using another built string I 
            }                            // just wrote each letter 
            for (int i = 1; i < input.Length; i += 1)
            {
                char character = input[i]; // u don't need these cause u could write input[i]
                char previousCharacter = input[i - 1]; // wherever I used the character var
                if (char.IsLetter(character)) // verify if character/input[i] is a letter
                {
                    if (character >= 'A' && character <= 'Z' &&
                        previousCharacter != ' ') // daca e mare si nu are spatiu inainte
                        Console.Write((char)(character + 32)); // inseamna ca e in cuvant
                    else if (character >= 'a' && character <= 'z' && // daca e mica si are 
                        previousCharacter == ' ') // spatiu inainte inseamnca ca e prima
                        Console.Write((char)(character - 32));
                    else Console.Write(character); // else u don't care bc it's a letter
                }                                  // which is already right
                else if (character == ' ') // if it's a space I add to the words and write
                {                          // the space too
                    words += 1;
                    Console.Write(" ");
                }
                else Console.Write(character); // write in case the char was a number
            }
            Console.WriteLine("\n" + words + " cuvinte"); // "\n" scrie un rand
            Console.ReadLine();// this is only for the run to not instantly close the cmd
        }
        //(pb1)(cuvinte formate doar din lit & cifre)
        //orice altceva e separator de cuvinte, poate incepe/termina
        //cu separator

        public static void pb2(string input) // honestly a better version of pb1 
        {
            int words = 0;
            bool inWord = false; // bool variable to verify whether you're in a word

            for (int i = 0; i < input.Length; i += 1)
            {
                if (!inWord && (char.IsLetter(input[i]) || char.IsDigit(input[i])))
                { // when you're not in a word and find a letter or number/digit
                    words += 1; // add a word since u found one
                    if (char.IsLetter(input[i])) Console.Write(char.ToUpper(input[i]));
                    else if (char.IsDigit(input[i])) Console.Write(input[i]);
                    inWord = true; // if it's a letter make it upper and if it's a number
                }                  // just write it, also update inWord to true
                else if (inWord && char.IsLetter(input[i])) // if u found a letter in word
                    Console.Write(char.ToLower(input[i]));  // u write it in lower case
                else if (char.IsDigit(input[i])) Console.Write(input[i]); // if digit write
                if (!(char.IsLetter(input[i]) || char.IsDigit(input[i])))
                { // if it's not a letter or number/digit it means you exited the word
                    inWord = false; // update whether you're in a word or not
                    Console.Write(input[i]); // and write the character since it stays as 
                }                            // word separator
            }
            Console.WriteLine("\n" + words + " cuvinte"); 
            Console.ReadLine(); 
        }
        //cititi un text, afisati de cate ori apare in text toate
        //literele alfabetului eng.(26)
        public static void pb3(string text)
        {
            int[] lettersAppearance = new int[26]; // faci un vector de frecventa de 26
            for (int i = 0; i < lettersAppearance.Length; i += 1) // these 2 lines are
                lettersAppearance[i] = 0;                         // pointless btw
                    // c# has the default values for an array 0 the moment u create it
            for (int i = 0; i < text.Length; i += 1)
                if (char.IsLetter(text[i])) // if it's a letter
                {                                        // make it lower 
                    char letter = char.ToLower(text[i]); // lower to lower is this lower
                    lettersAppearance[letter - 'a'] += 1; // letter as char in operations
                }               // behaves as its ascii code, ex: letter d(100)-a(97)=3
            for (int i = 0; i < lettersAppearance.Length; i += 1)// 3 is d's pos in array
            {
                if (lettersAppearance[i] == 1) // this is just for detail u don't need it
                    Console.WriteLine("Litera " + Convert.ToChar(i + 'a')
                    + " apare o data");             // u need this if tho ca sa scrii cate
                else if (lettersAppearance[i] > 0)  // aparitii au literle care apar > 0
                    Console.WriteLine("Litera " + Convert.ToChar(i + 'a')
                    + " apare de " + lettersAppearance[i] + " ori");
            } // (i + 'a') is like before, if u take i=3(d's position) 3 + 97(a) = 100(d)
            Console.ReadLine(); // Convert.ToChar() which writes the letter from ascii code
        }

        //func recurs care sa returneze cea mai mica val dintr un sir de nr
        public static int pb4(int index, int[] input, int minValue, int arrayLength)
        { // could be optimized but I was lazy 
            if (arrayLength == 0) // verify if there's any length left
                return minValue; // this would return the min value in the end of the recurs loop
            if (input[index] < minValue) minValue = input[index]; // update with each next index
            return pb4(++index, input, minValue, --arrayLength); // call again if there's still length
        }
    }
}
