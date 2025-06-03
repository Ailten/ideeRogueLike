
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
    public static float lerpF(float a, float b, float i)
    {
        return a * (1 - i) + b * i;
    }


    //rotate a vector of an angle specifid.
    public static Vector rotate(Vector a, float eulerAngle)
    {
        return new(
            (float)(Math.Cos(eulerAngle) * a.x + (-Math.Sin(eulerAngle)) * a.y),
            (float)(Math.Sin(eulerAngle) * a.x + Math.Cos(eulerAngle) * a.y)
        );
    }


    //cast euler to angle.
    public static float eulerToAngle(float euler){
        return euler * 57.29577951308232f;
    }
    public static float angleToEuler(float angle){ //this one not verify.
        return angle / 57.29577951308232f;
    }


    public static float distance(Vector a, Vector b)
    {
        a = (a - b);
        a *= a;
        return (float) Math.Sqrt(a.x + a.y);
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