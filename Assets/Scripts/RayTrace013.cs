using UnityEngine;
using UnityEditor;

public class RayTrace013
{
	public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
	// public static readonly Vec3 BG_TOP = new Vec3(0f, 0f, 0f);
	public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

	public static Vec3 BGColor(Ray ray, HitableList world, int depth)
	{
		HitRecord rec = null;
		// Shwdow Acne対策でminは0.001fに設定
		if (world.Hit(ray, 0.001f, float.MaxValue, ref rec))
		{
			Ray scattered;
			Vec3 attenuation;
			if (depth < 5 && rec.mat.Scatter(ray, rec, out attenuation, out scattered))
			{
				return attenuation * BGColor(scattered, world, depth + 1);
			}
			else
			{
				return new Vec3(0f, 0f, 0f);
				// return BG_TOP;
			}
		}
		else
		{
			Vec3 uDir = ray.direction.normalized;
			var t = 0.5f * (uDir.y + 1f);
			return (1f - t) * BG_BOTTOM + t * BG_TOP;
		}
	}

	[MenuItem("ImageBuilder/RayTrace013")]
	public static void Test001()
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();

		var w = 512;
		var h = 256;

		// var w = 256;
		// var h = 128;

		var ns = 20;

		// var cam = new Camera();
		// var cam = new Camera(90f, w / h);
		var lookfrom = new Vec3(7f, 4f, -7f);
		var lookat = new Vec3(0f, 0f, 0f);
		var focusDist = (lookfrom - lookat).length;

		var cam = new Camera(lookfrom, lookat, new Vec3(0f, 1f, 0f), 60f, w / h, 0.3f, focusDist);
		var world = new HitableList();
		var rot = Mathf.Cos(Mathf.PI / 4f);
		world.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f, new Lambertian(new Vec3(0.8f, 0.3f, 0.3f))));
		world.list.Add(new Sphere(new Vec3(1f, 0f, 1f), 0.5f, new Metal(new Vec3(0.8f, 0.6f, 0.2f), 1f)));
		world.list.Add(new Sphere(new Vec3(-1f, 0f, 1f), 0.5f, new Metal(new Vec3(0.8f, 0.8f, 0.8f), 0.3f)));

		for (int i = 0; i < 30; i++)
		{
			var random = Random.value;
			var radius = (Random.value * 0.9f + 0.1f);

			if (random > 0.5f)
			{
				world.list.Add(new Sphere(Vec3.randomInUnitDiscXZ * 10f, radius, new Lambertian(new Vec3(Random.value, Random.value, Random.value))));
			}
			else if (random > 0.2f)
			{
				world.list.Add(new Sphere(Vec3.randomInUnitDiscXZ * 10f, radius, new Metal(new Vec3(Random.value, Random.value, Random.value), Random.value * 0.5f)));
			}
			else
			{
				world.list.Add(new Sphere(Vec3.randomInUnitDiscXZ * 10f, radius, new Dielectric(Random.value * 0.5f + 1.2f)));
			}
		}

		world.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f, new Lambertian(new Vec3(0.3f, 0.3f, 0.4f))));

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
					Ray r = cam.GetRayWithOffset(u, v);
					// Ray r = cam.GetRay(u, v);
					col += BGColor(r, world, 0);
				}

				col /= ns;
				col = new Vec3(Mathf.Sqrt(col.r), Mathf.Sqrt(col.g), Mathf.Sqrt(col.b));
				c[y * w + x] = col;
			}
		}

		sw.Stop();
		Debug.Log("Elapsed: " + sw.ElapsedMilliseconds);

		ImageBuilder.SaveImage(c, w, h, "output_013_");
	}
}