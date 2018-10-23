using UnityEngine;
using UnityEditor;

public class RayTrace002
{
    public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
    public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

    public static Vec3 BGColor(Ray ray)
    {
        if (IsHittenSphere(new Vec3(0f, 0f, 1f), 0.5f, ray))
        {
            return new Vec3(1f, 0f, 0f);
        }
        Vec3 uDir = ray.direction.normalized;
        float t = 0.5f * (uDir.y + 1f);
        return (1f - t) * BG_BOTTOM + t * BG_TOP;
    }

    public static bool IsHittenSphere(Vec3 center, float radius, Ray ray)
    {
        var oc = ray.origin - center;
        var dir = ray.direction;

        var a = Vec3.Dot(dir, dir);
        var b = 2f * Vec3.Dot(dir, oc);
        var c = Vec3.Dot(oc, oc) - radius * radius;

        var d = b * b - 4 * a * c;
        return d >= 0;
    }

    [MenuItem("ImageBuilder/RayTrace002")]
    public static void Test001()
    {
        int w = 512;
        int h = 256;
        Vec3 lowerLeftCorner = new Vec3(-2f, -1, 1f);
        Vec3 horizontal = new Vec3(4f, 0f, 0f);
        Vec3 vertical = new Vec3(0f, 2f, 0f);
        Vec3 origin = new Vec3(0f, 0f, 0f);

        Vec3[] c = new Vec3[w * h];

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                float u = (float)x / w;
                float v = (float)y / h;
                Ray r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
                c[y * w + x] = BGColor(r);
            }
        }

        ImageBuilder.SaveImage(c, w, h);
    }
}