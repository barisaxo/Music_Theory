using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StringSystems
{
    public static string SpaceAfterCap(this string s)
    {
        string temp = s[0].ToString();
        for (int i = 1; i < s.Length; i++)
        {
            if (Char.IsUpper(s[i])) temp += " ";
            temp += s[i];
        }
        return temp;
    }
}
