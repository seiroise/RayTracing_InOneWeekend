public class Lambertian : Material
{
	public Vec3 albedo;

	public Lambertian(Vec3 albedo)
	{
		this.albedo = albedo;
	}

	public override bool Scatter(Ray r_in, HitRecord rec, out Vec3 attenuation, out Ray scattered)
	{
		var target = rec.point + rec.normal + Vec3.randomInUnitSphere;
		scattered = new Ray(rec.point, target - rec.point);
		attenuation = albedo;
		return true;
	}
}