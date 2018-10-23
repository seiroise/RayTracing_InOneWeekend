public class HitRecord
{
	public float t;
	public Vec3 point;
	public Vec3 normal;
	public Material mat;
}

public class Hitable
{
	public virtual bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec) { return false; }
}