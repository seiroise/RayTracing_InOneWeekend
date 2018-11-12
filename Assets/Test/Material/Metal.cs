namespace Test
{
	public class Metal : Material
	{
		Vec3 albedo;
		float fuzz;

		public Metal(float f)
		{
			albedo = new Vec3(1f);
			fuzz = f;
		}

		public Metal(Vec3 a, float f)
		{
			albedo = a;
			fuzz = f;
		}

		public override bool Scatter(Ray ray, ref HitRecord hitRec, ref ScatterRecord sctRec)
		{
			Vec3 reflected = Vec3.Reflect(ray.direction, hitRec.n).normalized;
			sctRec.ray = new Ray(hitRec.p, reflected + Vec3.randomInUnitSphere * fuzz);
			sctRec.attenuation = albedo;
			// 入射角と反射角が0より大きい場合(0 - π/2)の間だけ散乱する
			return (Vec3.Dot(sctRec.ray.direction, hitRec.n) > 0f);
		}
	}
}