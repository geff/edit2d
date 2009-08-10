using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public static class ColorExtension
{
    public static Color ToColor(this string strColor)
    {
        string[] strColors = strColor.Split(new char[] { ' ', '{', '}', 'R', 'G', 'B', 'A', ':' }, StringSplitOptions.RemoveEmptyEntries);
        Color color = new Microsoft.Xna.Framework.Graphics.Color(byte.Parse(strColors[0]), byte.Parse(strColors[1]), byte.Parse(strColors[2]), byte.Parse(strColors[3]));

        return color;
    }

    public static Vector2 ToVector2(this string strVector2)
    {
        string[] strValues = strVector2.Split(new char[] { ' ', '{', '}', 'X', 'Y', ':' }, StringSplitOptions.RemoveEmptyEntries);

        Vector2 vector2 = new Vector2();

        vector2.X = float.Parse(strValues[0]);
        vector2.Y = float.Parse(strValues[1]);

        return vector2;
    }

    public static Boolean[] ToBools(this string strBools)
    {
        string[] boolTabString = strBools.Split(new char[] { ' ', '{', '}', ';' }, StringSplitOptions.RemoveEmptyEntries);
        Boolean[] boolTab = new Boolean[boolTabString.Length];

        for (int i = 0; i < boolTabString.Length; i++)
        {
            boolTab[i] = bool.Parse(boolTabString[i]);
        }

        return boolTab;
    }

    public static string ArrayToString(this bool[] boolTab)
    {
        string boolString = "{";
        for (int i = 0; i < boolTab.Length; i++)
        {
            boolString += ";" + boolTab[i].ToString();
        }
        boolString += "}";

        return boolString;
    }

    public static float GetAngle(this Vector2 vec1, Vector2 vec2)
    {
        float dot = vec1.X * vec2.X + vec1.Y * vec2.Y;
        float pdot = vec1.X * vec2.Y - vec1.Y * vec2.X;
        return (float)Math.Atan2(pdot, dot);
    }
}
