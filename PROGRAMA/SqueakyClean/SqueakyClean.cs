using System;
using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        StringBuilder sb = new StringBuilder();
        bool isAfterHyphen = false;

        foreach (char c in identifier)
        {
            if (char.IsWhiteSpace(c))
            {
                sb.Append('_');
                isAfterHyphen = false;
            }
            else if (char.IsControl(c))
            {
                sb.Append("CTRL");
                isAfterHyphen = false;
            }
            else if (c == '-')
            {
                isAfterHyphen = true;
            }
            else if (c >= 'α' && c <= 'ω')
            {
                isAfterHyphen = false;
            }
            else if (char.IsLetter(c) || c == '_')
            {
                if (isAfterHyphen)
                {
                    sb.Append(char.ToUpper(c));
                    isAfterHyphen = false;
                }
                else
                {
                    sb.Append(c);
                }
            }
        }

        return sb.ToString();
    }
}

class Program
{
    static void Main()
    {
        string identifier = "my identifier";

        string resultado = Identifier.Clean(identifier);

        Console.WriteLine(resultado);
    }
}
