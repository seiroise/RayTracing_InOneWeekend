using UnityEngine;
using UnityEditor;

public class RayTrace004
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
		else {
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

    [MenuItem("ImageBuilder/RayTrace004")]
    public static void Test001()
    {
        var w = 512;
        var h = 256;
        var lowerLeftCorner = new Vec3(-2f, -1, 1f);
        var horizontal = new Vec3(4f, 0f, 0f);
        var vertical = new Vec3(0f, 2f, 0f);
        var origin = new Vec3(0f, 0f, 0f);

		var world = new HitableList();
		world.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f));
		world.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f));

        var c = new Vec3[w * h];

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                float u = (float)x / w;
                float v = (float)y / h;
                Ray r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
                c[y * w + x] = BGColor(r, world);
            }
        }

        ImageBuilder.SaveImage(c, w, h);
    }
}