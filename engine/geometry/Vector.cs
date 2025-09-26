
public struct Vector
{

    public float x;
    public float y;
    

    public static Vector[] adjacente = new Vector[]{
        new(0, -1),
        new(1, 0),
        new(0, 1),
        new(-1, 0)
    };


    public Vector(float x = 0, float y = 0)
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
    // do the reverce process of a lerp.
    public static float reverceLerpF(float start, float end, float between)
    {
        float dif = between - start;
        float maxDif = end - start;
        return dif / maxDif;
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


    public static Vector rotateAVector(Vector vectorBase, float angleRotate)
    {
		double eulerAngle = Vector.angleToEuler(angleRotate);
		return new Vector(
			(float) (Math.Cos(eulerAngle) * vectorBase.x + (-Math.Sin(eulerAngle)) * vectorBase.y),
			(float) (Math.Sin(eulerAngle) * vectorBase.x + Math.Cos(eulerAngle) * vectorBase.y)
		);
    }


    // loop on every cel doing a spiral extending from the center.
    public static void foreachCel(Vector posCenter, Vector distR, Action<Vector> iteration)
    {
        int distMin = (int)distR.x;
        int distMax = (int)distR.y;
        if (distMin == 0) // do iteration for the center.
        {
            iteration(posCenter);
            distMin++;
        }

        Vector[] orlogeWalk = new Vector[]{
            new(1, 1),
            new(-1, 1),
            new(-1, -1),
            new(1, -1)
        };
        
        Vector currentPos;
        for (int d = distMin; d <= distMax; d++)
        {
            for (int o = 0; o < 4; o++) // loop orloge axes.
            {
                currentPos = posCenter + Vector.adjacente[o] * d;

                do // loop on side of the current orloge loop.
                {
                    iteration(currentPos);

                    currentPos += orlogeWalk[o];

                    if (currentPos.x == posCenter.x || currentPos.y == posCenter.y)
                        break;

                } while (true);
            }
        }
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