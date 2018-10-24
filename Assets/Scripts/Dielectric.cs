using UnityEngine;

public class Dielectric : Material
{

	/// <summary>
	/// 参照する屈折率
	/// </summary>
	public readonly float refIdx;

	public Dielectric(float refIdx)
	{
		this.refIdx = refIdx;
	}

	public override bool Scatter(Ray r_in, HitRecord rec, out Vec3 attenuation, out Ray scattered)
	{
		Vec3 outwardNormal;
		Vec3 reflected = Vec3.Reflect(r_in.direction, rec.normal);
		float niOverNt;
		attenuation = new Vec3(1f, 1f, 1f);
		Vec3 refracted = new Vec3(0f, 0f, 0f);

		float reflectProb;
		float cosine;

		if (Vec3.Dot(r_in.direction, rec.normal) > 0)
		{
			outwardNormal = -rec.normal;
			niOverNt = refIdx;
			// 入射角と法線のcos
			cosine = refIdx * Vec3.Dot(r_in.direction, rec.normal) / r_in.direction.length;
		}
		else
		{
			outwardNormal = rec.normal;
			niOverNt = 1f / refIdx;
			cosine = -Vec3.Dot(r_in.direction, rec.normal) / r_in.direction.length;
		}

		if (Vec3.Refract(r_in.direction, outwardNormal, niOverNt, ref refracted))
		{
			reflectProb = Schlick(cosine, refIdx);
		}
		else
		{
			reflectProb = 1f;
		}

		if (Random.value < reflectProb)
		{
			// 反射
			scattered = new Ray(rec.point, reflected);
		}
		else
		{
			// 屈折
			scattered = new Ray(rec.point, refracted);
		}
		return true;
	}

	float Schlick(float cosine, float refIdx)
	{
		float r0 = (1f - refIdx) / (1 + refIdx);
		r0 = r0 * r0;
		return r0 + (1f - r0) * Mathf.Pow((1f - cosine), 5f);
	}
}