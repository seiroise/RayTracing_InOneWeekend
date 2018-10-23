using UnityEngine;
using UnityEditor;

public class RayTrace007
{
	public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
	public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

	public static Vec3 BGColor(Ray ray, HitableList world)
	{
		HitRecord rec = null;
		if (world.Hit(ray, 0f, float.MaxValue, ref rec))
		{
			Vec3 target = rec.point + rec.normal + Vec3.randomInUnitSphere;
			// 再帰的に繰り返しつつ、得られた反射を半減していく
			return 0.5f * BGColor(new Ray(rec.point, target - rec.point), world);
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

	[MenuItem("ImageBuilder/RayTrace007")]
	public static void Test001()
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();

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
				// いくつかサンプリングして平均を計算
				Vec3 col = new Vec3(0f, 0f, 0f);
				for (int s = 0; s < ns; ++s)
				{
					float u = (x + Random.value) / w;
					float v = (y + Random.value) / h;
					Ray r = cam.GetRay(u, v);
					col += BGColor(r, world);
				}

				col /= ns;
				col = new Vec3(Mathf.Sqrt(col.r), Mathf.Sqrt(col.g), Mathf.Sqrt(col.b));
				c[y * w + x] = col;
			}
		}

		sw.Stop();
		Debug.Log("Elapsed: " + sw.ElapsedMilliseconds);

		ImageBuilder.SaveImage(c, w, h, "output_diffuse_gamma");
	}
}