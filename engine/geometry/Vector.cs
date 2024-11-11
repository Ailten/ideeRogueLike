
public struct Vector
{

    public float x;
    public float y;


    public Vector(float x=0, float y=0)
    {
        this.x = x;
        this.y = y;
    }


    //lerp pos a to pos b at interpolation i (0 to 1).
    public static Vector lerp(Vector a, Vector b, float i)
    {
        return new(
            a.x*(1-i) + b.x*i,
            a.y*(1-i) + b.y*i
        );
    }


    public override string ToString() => $"[x:{x}, y:{y}]";

    //public static implicit operator string(Vector a) => a.ToString();

    public static Vector operator +(Vector a, Vector b) => new(a.x+b.x, a.y+b.y);

    public static Vector operator -(Vector a, Vector b) => new(a.x-b.x, a.y-b.y);

    public static Vector operator *(Vector a, Vector b) => new(a.x*b.x, a.y*b.y);

    public static Vector operator /(Vector a, Vector b) => new(a.x/b.x, a.y/b.y);

    public static Vector operator +(Vector a, float b) => new(a.x+b, a.y+b);

    public static Vector operator -(Vector a, float b) => new(a.x-b, a.y-b);

    public static Vector operator *(Vector a, float b) => new(a.x*b, a.y*b);

    public static Vector operator /(Vector a, float b) => new(a.x/b, a.y/b);

    public static Vector operator +(Vector a, int b) => new(a.x+b, a.y+b);

    public static Vector operator -(Vector a, int b) => new(a.x-b, a.y-b);

    public static Vector operator *(Vector a, int b) => new(a.x*b, a.y*b);

    public static Vector operator /(Vector a, int b) => new(a.x/b, a.y/b);

    public static implicit operator System.Numerics.Vector2(Vector a) => new(a.x, a.y);

    public static implicit operator Vector(System.Numerics.Vector2 a) => new(a.X, a.Y);
}