string[] vowels = new string[] { "a", "e", "i", "o", "u" };

Console.WriteLine("Welcome to the Pig Latin translator!");
bool runProgram = true;
while (runProgram)
{
    //get word or line from user
    Console.WriteLine();
    Console.WriteLine("Please enter the line you would like to translate");
    string input = Console.ReadLine().Trim();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Nothing was entered. Please try again.");
        continue;
    }

    //split string into array and keep as is for comparison
    string[] line = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    //Copy array and remove punctuation for translating
    string[] translated = new string[line.Length];
    line.CopyTo(translated, 0);
    removePunc(ref translated);
    //new comparison array for casing, in case of punctuation at beginning of a word
    string[] casing = new string[line.Length];
    line.CopyTo(casing, 0);
    removePunc(ref casing);
    for (int i = 0; i < line.Length; i++)
    {
        if (hasSymbol(translated[i]) || hasDigit(translated[i]))
        {
            continue;
        }
        else
        {
            string word = translated[i].ToLower();
            //use substring to get index 0 with a length of 1 and save it
            string firstLetter = word.Substring(0, 1);

            //if that new string is a vowel add way to the end
            if (vowels.Contains(firstLetter.ToLower()))
            {
                word += "way";
                keepCase(casing[i], ref word);
                translated[i] = word;
            }

            //else loop through letters of word
            else
            {
                for (int j = 0; j < word.Length; j++)
                {
                    //substring using current loop number
                    //if it is a vowel save that number
                    string letter = word.Substring(j, 1);
                    if (vowels.Contains(letter.ToLower()) || (yIsVowel(word) && letter == "y"))
                    {
                        //substring from 0 to that number
                        string moveToEnd = word.Substring(0, j);
                        //substring from that number to the end
                        string moveToBeginning = word.Substring(j);
                        //take those 2, rearrange them with ay at the end
                        translated[i] = moveToBeginning + moveToEnd + "ay";
                        keepCase(casing[i], ref translated[i]);
                        break;
                    }
                }
            }
        }
    }
    CopyPunc(line, ref translated);
    writeArray(translated);

    //ask if user wants to continue
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Would you like to translate something else? (yes/y, no/n)");
        string restart = Console.ReadLine();
        if (restart.ToLower().Trim() == "yes" || restart.ToLower().Trim() == "y")
        {
            break;
        }
        else if (restart.ToLower().Trim() == "no" || restart.ToLower().Trim() == "n")
        {
            runProgram = false;
            break;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Please enter yes/y or no/n");
            continue;
        }
    }
}


///////////////////////methods

//keepCase() method
static void keepCase(string word1, ref string word2)
{
    //title case 
    if (Char.IsUpper(word1[0]) && word1.Substring(1).ToLower() == word1.Substring(1))
    {
        word2 = Char.ToUpper(word2[0]) + word2.Substring(1).ToLower();
    }
    //uppercase
    else if (word1 == word1.ToUpper())
    {
        word2 = word2.ToUpper();
    }
    //lowercase
    else if (word1 == word1.ToLower())
    {
       word2 = word2.ToLower();
    }
}

//hasSymbol() method 
static bool hasSymbol(string s)
{
    bool contains = false;
    for (int i = 0; i < s.Length; i++)
    {
        if (!Char.IsLetterOrDigit(s[i]) && !(s[i] == '\''))
        {
            contains = true;
            break;
        }
    }
    return contains;
}

//hasDigit() method
static bool hasDigit(string s)
{
    bool contains = false;
    for (int i = 0; i < s.Length; i++)
    {
        if (Char.IsDigit(s[i]))
        {
            contains = true;
            break;
        }
    }
    return contains;
}

//writeArray() method
static void writeArray(string[] strArr)
{
    for (int i = 0; i < strArr.Length; i++)
    {
        Console.Write(strArr[i] + " ");
    }
    Console.WriteLine();
}

//removePunc() method
static void removePunc(ref string[] strArr)
{
    char[] notPunctuation = new char[] { '@', '&', '%', '#' };
    for (int i = 0; i < strArr.Length; i++)
    {
        while (true)
        {
            if (Char.IsPunctuation(strArr[i][strArr[i].Length - 1]) && !notPunctuation.Contains(strArr[i][strArr[i].Length - 1]))
            {
                strArr[i] = strArr[i].Substring(0, strArr[i].Length - 1);
            }
            else
            {
                break;
            }
        }
        while (true)
        {
            if (Char.IsPunctuation(strArr[i][0]) && !notPunctuation.Contains(strArr[i][0]))
            {
                strArr[i] = strArr[i].Substring(1, strArr[i].Length - 1);
            }
            else
            {
                break;
            }
        }
    }
}

//copyPunc() method
static void CopyPunc(string[] strArr1, ref string[] strArr2)
{
    char[] notPunctuation = new char[] { '@', '&', '%', '#' };
   
    for (int i = 0; i < strArr1.Length; i++)
    {
        string beginning = "";
        string ending = "";
        while (true)
        {
            if (Char.IsPunctuation(strArr1[i][strArr1[i].Length - 1]) && !notPunctuation.Contains(strArr1[i][strArr1[i].Length - 1]))
            {
                ending += strArr1[i][strArr1[i].Length - 1];
                strArr1[i] = strArr1[i].Substring(0, strArr1[i].Length - 1);
            }
            else
            {
                break;
            }
        }
        while (true)
        {
            if (Char.IsPunctuation(strArr1[i][0]) && !notPunctuation.Contains(strArr1[i][0]))
            {
                beginning += strArr1[i][0];
                strArr1[i] = strArr1[i].Substring(1, strArr1[i].Length - 1);
            }
            else
            {
                break;
            }
        }
        char[] endPunc = ending.ToCharArray();
        Array.Reverse(endPunc);
        ending = new string(endPunc);
        strArr2[i] = beginning + strArr2[i] + ending;
    }
}

//yIsVowel() method
static bool yIsVowel(string word)
{
    char[] charVowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
    char[] charWord = word.ToCharArray();
    bool isVowel = false;
    int vowelCount = 0;
    for (int i = 0; i < word.Length; i++)
    {
        if (charVowels.Contains(word[i]))
        {
            vowelCount++;
            break;
        }
    }
    if ((charWord.Contains('y') || charWord.Contains('Y')) && vowelCount == 0)
    {
        isVowel = true;
    }
    return isVowel;
}