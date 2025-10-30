using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     */

    public static List<string> bomberMan(int n, List<string> grid)
    {
         int r = grid.Count;
        int c = grid[0].Length;

        // Convert grid to char array for easier manipulation
        char[,] g = new char[r, c];
        for (int i = 0; i < r; i++)
            for (int j = 0; j < c; j++)
                g[i, j] = grid[i][j];

        // If n = 1, return the initial state
        if (n == 1)
            return grid;

        // If n is even, the grid is fully filled with bombs
        if (n % 2 == 0)
        {
            List<string> fullBomb = new List<string>();
            for (int i = 0; i < r; i++)
                fullBomb.Add(new string('O', c));
            return fullBomb;
        }

        // Helper to detonate bombs
        char[,] Detonate(char[,] g0)
        {
            char[,] result = new char[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    result[i, j] = 'O'; // fill with bombs

            int[] dr = new int[] { -1, 1, 0, 0 };
            int[] dc = new int[] { 0, 0, -1, 1 };

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (g0[i, j] == 'O')
                    {
                        result[i, j] = '.';
                        for (int d = 0; d < 4; d++)
                        {
                            int ni = i + dr[d];
                            int nj = j + dc[d];
                            if (ni >= 0 && ni < r && nj >= 0 && nj < c)
                                result[ni, nj] = '.';
                        }
                    }
                }
            }
            return result;
        }

        // The pattern repeats every 4 cycles after second 3
        char[,] g1 = Detonate(g);        // state at t=3
        char[,] g2 = Detonate(g1);       // state at t=5

        char[,] final;
        if ((n - 3) % 4 == 0)
            final = g1;
        else
            final = g2;

        // Convert final grid back to list of strings
        List<string> ans = new List<string>();
        for (int i = 0; i < r; i++)
        {
            char[] row = new char[c];
            for (int j = 0; j < c; j++)
                row[j] = final[i, j];
            ans.Add(new string(row));
        }

        return ans;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
