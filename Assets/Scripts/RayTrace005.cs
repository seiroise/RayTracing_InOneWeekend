using UnityEngine;
using UnityEditor;

public class RayTrace005
{
	public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
	public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

	public static Vec3 BGColor(Ray ray, HitableList world)
	{
		HitRecord rec = null;
		if (world.Hit(ray, 0f, float.MaxValue, ref rec))
		{
			return 0.5f * (rec.normal + 1f);
		}
		else
		{
			Vec3 uDir = ray.direction.normalized;
			var t = 0.5f * (uDir.y + 1f);
			return (1f - t) * BG_BOTTOM + t * BG_TOP;
		}
	}

	public static float HitSphere(Vec3 center, float radius, Ray ray)
	{
		var oc = ray.origin - center;
		var dir = ray.direction;

		var a = Vec3.Dot(dir, dir);
		var b = 2f * Vec3.Dot(dir, oc);
		var c = Vec3.Dot(oc, oc) - radius * radius;

		var d = b * b - 4 * a * c;
		return d < 0 ? -1f : (-b - Mathf.Sqrt(d)) / 2f * a;
	}

	[MenuItem("ImageBuilder/RayTrace005")]
	public static void Test001()
	{
		var w = 512;
		var h = 256;

		var ns = 10;

		var cam = new Camera();
		var world = new HitableList();
		world.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f));
		world.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f));

		var c = new Vec3[w * h];

		for (int x = 0; x < w; ++x)
		{
			for (int y = 0; y < h; ++y)
			{
				// �������T���v�����O���ĕ��ς��v�Z
				Vec3 col = new Vec3(0f, 0f, 0f);
				for (int s = 0; s < ns; ++s)
				{
					float u = (x + Random.value) / w;
					float v = (y + Random.value) / h;
					Ray r = cam.GetRay(u, v);
					// Vec3 p = r.PointAtParameter(2f);
					col += BGColor(r, world);
				}

				col /= ns;
				c[y * w + x] = col;
				// float u = (float)x / w;
				// float v = (float)y / h;
				// c[y * w + x] = BGColor(r, world);
			}
		}

		ImageBuilder.SaveImage(c, w, h, "output_aa");
	}
}