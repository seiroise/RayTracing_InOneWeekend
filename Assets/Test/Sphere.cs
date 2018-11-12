using UnityEngine;

namespace Test
{
	public class Sphere : Hitable
	{
		Vec3 pos;
		float radius;
		Material mat;

		public Sphere(Vec3 pos, float radius)
		{
			this.pos = pos;
			this.radius = radius;
			this.mat = null;
		}
		public Sphere(Vec3 pos, float radius, Material mat) : this(pos, radius)
		{
			this.mat = mat;
		}

		public override bool Hit(Ray ray, float tMin, float tMax, ref HitRecord rec)
		{
			var dir = ray.direction;
			var pc = ray.origin - pos;

			var a = Vec3.Dot(dir, dir);
			var b = Vec3.Dot(dir, pc);
			var c = Vec3.Dot(pc, pc) - radius * radius;

			var d = b * b - a * c;
			if (d > 0f)
			{
				var sqrD = Mathf.Sqrt(d);
				var temp = (-b - sqrD) / a;
				if (!(tMin < temp && temp < tMax))
				{
					temp = (-b + sqrD) / a;
					if (!(tMin < temp && temp < tMax))
					{
						return false;
					}
				}
				rec.t = temp;
				rec.p = ray.Point(temp);
				rec.n = (rec.p - pos) / radius; // 正規化は半径で割るだけでいいので楽
				rec.mat = mat;
				return true;
			}
			return false;
		}
	}
}
