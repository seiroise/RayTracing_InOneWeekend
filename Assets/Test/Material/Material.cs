namespace Test
{
	public class ScatterRecord
	{
		public Ray ray;
		public Vec3 attenuation;
	}

	public abstract class Material
	{
		public abstract bool Scatter(Ray ray, ref HitRecord hitRec, ref ScatterRecord sctRec);
	}
}