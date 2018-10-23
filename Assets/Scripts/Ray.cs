public class Ray
{

    public readonly Vec3 a;
    public readonly Vec3 b;

    public Ray(Vec3 a, Vec3 b)
    {
        this.a = a;
        this.b = b;
    }

    public Vec3 origin { get { return a; } }
    public Vec3 direction { get { return b; } }

    public Vec3 PointAtParameter(float t)
    {
        return a + b * t;
    }
}