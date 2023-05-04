﻿var  WordsRead= File.ReadAllLines("C:\\Users\\Давід\\RiderProjects\\MaybeUMean2\\MaybeUMean2\\NewFile1.txt");
var Words=new List<string>();
var Sentence = new List<string>();
for (int i = 0; i < WordsRead.Length; i++)
{
    Words.Add(WordsRead[i]);
}

var Input = Console.ReadLine().Split(" ");
foreach (var VARIABLE in Input)
{
    var WordToAdd=VARIABLE.Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Replace("$", "");
        Sentence.Add(WordToAdd);
}

var LightTypo = "U've got a typo in: ";
foreach (var VARIABLE in Sentence)
{
    string firstLetterUpper = VARIABLE.Substring(0, 1).ToUpper() + VARIABLE.Substring(1);
    string firstLetterLower = VARIABLE.Substring(0, 1).ToLower() + VARIABLE.Substring(1);
    if (Words.Contains(firstLetterUpper) || Words.Contains(firstLetterLower))
    {
        Console.WriteLine(VARIABLE); 
    }
    else
    {
        LightTypo+=VARIABLE+" ";
    //   var a= LongestSub(VARIABLE, Words);
    var a = LevinsteinList(VARIABLE, Words);
       foreach (var Wordddd in a)
       {
           Console.WriteLine(Wordddd);
       }
    }

}
Console.WriteLine(LightTypo);



List<string> LevinsteinList(string value, List<string> checker)
{
    var draft = new Dictionary<string, int>();
    var res = new List<string>();
    foreach (var VARIABLE in checker)
    {
        
    
    int[,] matrix = new int[value.Length + 1, VARIABLE.Length + 1];
    for (int i = 0; i <= value.Length; i++)
    {
        matrix[i,0] = i;
    }
    for (int j = 0; j <= VARIABLE.Length; j++)
    {
        matrix[0,j] = j;
    }

    for (int i = 1; i < value.Length+1; i++)
    {
        for (int j = 1; j <VARIABLE.Length+1; j++)
        {
            int v1 = matrix[i - 1, j] + 1;
            int v2 = matrix[i, j - 1] + 1;
            int v3 = 0;
            int v4 = 0;
            int cost = 0;
            
            if (value[i-1]==VARIABLE[j-1])
            {
                v3 = matrix[i - 1, j - 1] ;
                cost = 0;
            }
            else
            {
                v3 = matrix[i - 1, j - 1]+1;
                cost = 1;
            }
            if (i>1 && j>1)
            {
                if (VARIABLE[j-1] == value[i - 2] && VARIABLE[j - 2] == value[i-1])
                {
                    v4 = matrix[i - 2, j - 2]+cost; 
                }
                else
                {
                    v4 = 999999999;
                } 
            } else {
                v4 = 999999999;
            }
            matrix[i,j] = Math.Min(Math.Min(v1, v2), Math.Min(v3,v4));
        }
    }
    var addd = matrix[value.Length, VARIABLE.Length];
   
    draft.Add(VARIABLE, addd); 
    }
    var Sorted_Word_Points = draft.OrderBy(x => x.Value);
    var count = 0;
    foreach (var VARIABLE in Sorted_Word_Points)
    {
        count += 1;
        res.Add(VARIABLE.Key);
        if (count>4)
        {
            break;
        }
    }
    return res;
}

/*
 List<string> LevinsteinListPart(string value, List<string> checker)
{
    var draft = new Dictionary<string, int>();
    var res = new List<string>();

    foreach (var VARIABLE in checker)
    {
        int[,] matrix = new int[value.Length, VARIABLE.Length];
        for (int i = 0; i < value.Length; i++)
        {
            for (int j = 0; j < VARIABLE.Length; j++)
            {
                matrix[i, j] = LevinsteinCalculationPart(i, j, value, VARIABLE);
            }
        }
        draft.Add(VARIABLE,matrix[value.Length-1, VARIABLE.Length-1]);
        
    }
    var Sorted_Word_Points = draft.OrderBy(x => x.Value);
    var count = 0;
    foreach (var VARIABLE in Sorted_Word_Points)
    {
        count += 1;
        res.Add(VARIABLE.Key);
        if (count>4)
        {
            break;
        }
    }
    return res;
}
 
 
 int LevinsteinCalculationPart(int i, int j, string value, string variable)
{
    int cell = 0;
   
    if (Math.Min(i,j)==0)
    {
        cell = Math.Max(i, j);
    }
    else
    {
        int value1 = LevinsteinCalculationPart(i - 1, j, value, variable) + 1;
        int value2=LevinsteinCalculationPart(i , j-1, value, variable) + 1;
        int value3 = 0; 
        if (value[i-1]!=variable[j-1])
        {
             value3=LevinsteinCalculationPart(i-1 , j-1, value, variable) + 1;   
        }
        else
        {
            value3=LevinsteinCalculationPart(i-1 , j-1, value, variable);  
        }
        
        cell = Math.Min(Math.Min(value1, value2), value3); 
    }
    return cell;
}
*/




/*List<string> LongestSub(string value, List<string> checker)
{
    var draft = new Dictionary<string, int>();
    var res = new List<string>();
    foreach (var VARIABLE in checker)
    {
        int[,] matrix = new int[value.Length, VARIABLE.Length];
        for (int i = 0; i < value.Length; i++)
        {
            for (int j = 0; j < VARIABLE.Length; j++)
            {
                if (value[i]==VARIABLE[j])
                {
                    if (i-1<0 || j-1<0)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + 1;
                    }
                }
                else
                {
                    if (i-1<0 || j-1<0)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] =Math.Max( matrix[i - 1, j],matrix[i, j-1]) ;
                    }   
                }
            
            }
        }
        int maxElement = -1;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i,j]>maxElement)
                {
                    maxElement = matrix[i,j];
                }
            }
          
        }
        draft.Add(VARIABLE,maxElement);
 
    }
    var Sorted_Word_Points = draft.OrderByDescending(x => x.Value);
    var count = 0;
    foreach (var VARIABLE in Sorted_Word_Points)
    {
        count += 1;
        res.Add(VARIABLE.Key);
        if (count>4)
        {
            break;
        }
    }
    return res;
}
*/
//Trouble: fosh, forn (other small words) 43378
