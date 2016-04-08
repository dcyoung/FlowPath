using UnityEngine;
using System.Collections;

public static class UtilityPrinter
{
    private static string printString;

    public static void print(int i)
    {
        printString = string.Format("{0}", i) + "\n";
        Debug.Log(printString);
    }

    public static void print(double f)
    {
        printString = string.Format("{0}", f) + "\n";
        Debug.Log(printString);
    }

    public static void print(string s)
    {
        printString = s + "\n";
        Debug.Log(printString);
    }

    public static void print(string msg, int i)
    {
        printString = msg + string.Format(" {0}", i) + "\n";
        Debug.Log(printString);
      }

    public static void print(string msg, double f)
    {
        printString = msg + string.Format(" {0}", f) + "\n";
        Debug.Log(printString);
    }

    public static void print(string msg, bool b)
    {
        printString = msg + string.Format(" {0}", b) + "\n";
        Debug.Log(printString);
    }

}
