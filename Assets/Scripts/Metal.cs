public class Metal : Material
{

	public Vec3 albedo;
	public float fuzz;

	public Metal(Vec3 albedo, float f)
	{
		this.albedo = albedo;
		this.fuzz = f < 1f ? f : 1f;
	}

	public override bool Scatter(Ray r_in, HitRecord rec, out Vec3 attenuation, out Ray scattered)
	{
		var reflected = Vec3.Reflect(r_in.direction, rec.normal);
		// scattered = new Ray(rec.point, reflected);
		scattered = new Ray(rec.point, reflected + fuzz * Vec3.randomInUnitSphere);
		attenuation = albedo;
		// 90度以上の反射はしないようにしている？
		return Vec3.Dot(scattered.direction, rec.normal) > 0f;
	}
}