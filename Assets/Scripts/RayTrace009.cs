using UnityEngine;
using UnityEditor;

public class RayTrace009
{
	public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
	public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

	public static Vec3 BGColor(Ray ray, HitableList world, int depth)
	{
		HitRecord rec = null;
		// Shwdow Acne対策でminは0.001fに設定
		if (world.Hit(ray, 0.001f, float.MaxValue, ref rec))
		{
			Ray scattered;
			Vec3 attenuation;
			if (depth < 10 && rec.mat.Scatter(ray, rec, out attenuation, out scattered))
			{
				return attenuation * BGColor(scattered, world, depth + 1);
			}
			else
			{
				return new Vec3(0f, 0f, 0f);
			}
		}
		else
		{
			Vec3 uDir = ray.direction.normalized;
			var t = 0.5f * (uDir.y + 1f);
			return (1f - t) * BG_BOTTOM + t * BG_TOP;
		}
	}

	[MenuItem("ImageBuilder/RayTrace009")]
	public static void Test001()
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();

		var w = 512;
		var h = 256;
		var ns = 10;

		var cam = new Camera();
		var world = new HitableList();
		world.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f, new Lambertian(new Vec3(0.8f, 0.3f, 0.3f))));
		world.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f, new Lambertian(new Vec3(0.8f, 0.8f, 0f))));
		// world.list.Add(new Sphere(new Vec3(0f, 100.5f, 1f), 100f, new Lambertian(new Vec3(0.8f, 0.8f, 0f))));
		world.list.Add(new Sphere(new Vec3(1f, 0f, 1f), 0.5f, new Metal(new Vec3(0.8f, 0.6f, 0.2f), 1f)));
		world.list.Add(new Sphere(new Vec3(-1f, 0f, 1f), 0.5f, new Metal(new Vec3(0.8f, 0.8f, 0.8f), 0.3f)));

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
					col += BGColor(r, world, 0);
				}

				col /= ns;
				col = new Vec3(Mathf.Sqrt(col.r), Mathf.Sqrt(col.g), Mathf.Sqrt(col.b));
				c[y * w + x] = col;
			}
		}

		sw.Stop();
		Debug.Log("Elapsed: " + sw.ElapsedMilliseconds);

		ImageBuilder.SaveImage(c, w, h, "output_materials");
	}
}