namespace Test
{
	public class Lambertian : Material
	{
		Vec3 albedo;

		public Lambertian()
		{
			albedo = new Vec3(1f);
		}

		public Lambertian(Vec3 a)
		{
			albedo = a;
		}

		public override bool Scatter(Ray ray, ref HitRecord hitRec, ref ScatterRecord sctRec)
		{
			// 単位球内の適当な点に対して反射する
			var target = hitRec.p + hitRec.n + Vec3.randomInUnitSphere;
			sctRec.ray = new Ray(hitRec.p, target - hitRec.p);
			sctRec.attenuation = albedo;

			return true;
		}
	}
}