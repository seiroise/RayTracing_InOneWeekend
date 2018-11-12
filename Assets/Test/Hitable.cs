namespace Test
{
	public struct HitRecord
	{
		public float t;
		public float u;
		public float v;
		public Vec3 p;
		public Vec3 n;
		public Material mat;
	}

	public abstract class Hitable
	{
		public abstract bool Hit(Ray ray, float tMin, float tMax, ref HitRecord rec);
	}
}