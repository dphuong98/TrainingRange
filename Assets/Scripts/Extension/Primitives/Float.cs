using System;

public static class Float
{
    private static float TOLERANCE = 1E-6F;
    public static bool NearlyEqual(this float a, float b)
    {
        //Not safe
        //TODO https://www.google.com/search?client=firefox-b-d&q=John+Shewchuk+Fast+Robust+Predicates+for+Computational+Geometry&spell=1&sa=X&ved=2ahUKEwjbpK6z1fPxAhWIPJQKHQd9CpgQBSgAegQIARAz&biw=1920&bih=927
        return Math.Abs(a - b) < TOLERANCE;
    }
}
