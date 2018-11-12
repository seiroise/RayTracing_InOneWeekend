using UnityEngine;

namespace Test
{
	public class Dielectric : Material
	{
		float refIdx;

		public Dielectric(float refIdx)
		{
			this.refIdx = refIdx;
		}

		public override bool Scatter(Ray ray, ref HitRecord hitRec, ref ScatterRecord sctRec)
		{
			// 法線と衝突点の入射角から外向きの法線を計算する
			Vec3 outwardNormal;
			float n1OverN2;
			float cosine;
			if (Vec3.Dot(ray.direction, hitRec.n) > 0f)
			{
				outwardNormal = -hitRec.n;
				n1OverN2 = refIdx;
				cosine = refIdx * Vec3.Dot(ray.direction, hitRec.n) / ray.direction.length;
			}
			else
			{
				outwardNormal = hitRec.n;
				n1OverN2 = 1f / refIdx;
				cosine = -Vec3.Dot(ray.direction, hitRec.n) / ray.direction.length;
			}

			sctRec.attenuation = new Vec3(1f);

			// フレネル方程式をもとに屈折するか反射するかを計算する
			Vec3 refracted = new Vec3(1f);
			float refrect_prob = 0f;
			if (Vec3.Refract(-ray.direction, outwardNormal, n1OverN2, ref refracted))
			{
				refrect_prob = Schlick(cosine, refIdx);
			}
			else
			{
				refrect_prob = 1f;
			}
			
			if (Random.value < refrect_prob)
			{
				var reflected = Vec3.Reflect(ray.direction, hitRec.n);
				sctRec.ray = new Ray(hitRec.p, reflected);
			}
			else
			{
				sctRec.ray = new Ray(hitRec.p, refracted);
			}

			return true;
		}

		static float Schlick(float cosine, float refIdx)
		{
			// Schlickによるフレネル方程式の近似
			// 相手は空気を想定 屈折率(refIdx nearly equals 1)
			float r0 = Mathf.Pow((1f - refIdx) / (1f + refIdx), 2f);
			return r0 + (1f - r0) * Mathf.Pow(1 - cosine, 5f);
		}
	}
}