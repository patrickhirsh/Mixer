using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFormatter
{
    /// <summary>
    /// given a keycode, returns a clean string that's formatted for front-end use
    /// </summary>
    public static string getStringFromKeyCode(KeyCode key)
    {
        string clean = key.ToString();
        if (clean.StartsWith("ALPHA", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(5, clean.Length - 5); }
        if (clean.StartsWith("KEYPAD", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(6, clean.Length - 6); }
        return clean;
    }


    public static string newLineOnSpace(string source)
    {
        string[] words = source.Split(' ');
        string output = "";

        for (int i = 0; i < words.Length - 1; i++)
            output += words[i] + "\n";

        output += words[words.Length - 1];

        return output;
    }
}
