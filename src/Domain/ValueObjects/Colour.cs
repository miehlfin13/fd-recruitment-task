using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_App.Domain.ValueObjects;

public class Colour : ValueObject
{
    static Colour()
    {
    }

    private Colour()
    {
    }

    private Colour(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public static Colour From(string code)
    {
        var colour = new Colour { Code = code };

        if (!SupportedColours.Contains(colour))
        {
            throw new UnsupportedColourException(code);
        }

        return colour;
    }

    public static Colour White => new("White", "#FFFFFF");

    public static Colour Red => new("Red", "#FF5733");

    public static Colour Orange => new("Orange", "#FFC300");

    public static Colour Yellow => new("Yellow", "#FFFF66");

    public static Colour Green => new("Green", "#CCFF99");

    public static Colour Blue => new("Blue", "#6666FF");

    public static Colour Purple => new("Purple", "#9966CC");

    public static Colour Grey => new("Grey", "#999999");

    public string Code { get; private set; } = "#000000";
    
    [NotMapped]
    public string Name { get; private set; } = "";

    public static implicit operator string(Colour colour)
    {
        return colour.ToString();
    }

    public static explicit operator Colour(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }

    public static IEnumerable<Colour> SupportedColours
    {
        get
        {
            yield return White;
            yield return Red;
            yield return Orange;
            yield return Yellow;
            yield return Green;
            yield return Blue;
            yield return Purple;
            yield return Grey;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
