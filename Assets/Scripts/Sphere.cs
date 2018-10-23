using UnityEngine;

public class Sphere : Hitable {

	public readonly Vec3 center;
	public readonly float radius;
	public readonly Material material;

	public Sphere(Vec3 center, float radius, Material material = null)
	{
		this.center = center;
		this.radius = radius;
		this.material = material;
	}

	public override bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
	{
		var oc = r.origin - center;
		var dir = r.direction;

		var a = Vec3.Dot(dir, dir);
		var b = Vec3.Dot(oc, dir);
		var c = Vec3.Dot(oc, oc) - radius * radius;
		var d = b * b - a * c;

		if (d > 0)
		{
			var t = (-b - Mathf.Sqrt(d)) / a;
			if (tMin < t && t < tMax)
			{
				rec.t = t;
				rec.point = r.PointAtParameter(t);
				rec.normal = (rec.point - center) / radius;
				rec.mat = material;
				return true;
			}
			t = (-b + Mathf.Sqrt(d)) / a;
			if (tMin < t && t < tMax)
			{
				rec.t = t;
				rec.point = r.PointAtParameter(t);
				rec.normal = (rec.point - center) / radius;
				rec.mat = material;
				return true;
			}
		}
		return false;
	}
}