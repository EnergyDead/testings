var triangleType = TriangleType.NotTriangle;

try
{
    if (args.Length != 3) throw new ArgumentException();
    var sidesOfTriangle = args.Select(x => double.Parse(x)).ToList();

    var a = sidesOfTriangle[0];
    var b = sidesOfTriangle[1];
    var c = sidesOfTriangle[2];

    if (!IsValidTriangle(a, b, c)) throw new ArgumentException();
    triangleType = TriangleType.Triangle;

    if (IsEquilateralTriangle(a, b, c))
    {
        triangleType = TriangleType.Equilateral;
    }

    if (IsIsoscelesTriangle(a, b, c))
    {
        triangleType = TriangleType.Isosceles;
    }

    Console.WriteLine(triangleType.ToString());
}
catch (Exception)
{
    Console.WriteLine("UnknownError");
    return;
}

#region utils
bool IsValidTriangle(double a, double b, double c)
{
    if (a + b > c && a + c > b && b + c > a)
    {
        return true;
    }

    return false;
}

bool IsEquilateralTriangle(double a, double b, double c)
{
    return (a == b && a == c);
}

bool IsIsoscelesTriangle(double a, double b, double c)
{
    return (a == b && a != c || a == c && a != b || b == c && b != a);
}
#endregion

enum TriangleType
{
    NotTriangle,
    Triangle,
    Equilateral,
    Isosceles,
}