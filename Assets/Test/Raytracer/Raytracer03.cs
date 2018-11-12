using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
	public class Raytracer03
	{

		public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
		public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

		public static float HitSpehere(Vec3 center, float radius, Ray ray)
		{
			var dir = ray.direction;
			var oc = ray.origin - center;

			var a = Vec3.Dot(dir, dir);
			var b = Vec3.Dot(dir, oc);
			var c = Vec3.Dot(oc, oc) - radius * radius;

			var d = b * b - a * c;
			if (d >= 0f)
			{
				return (-b - Mathf.Sqrt(b * b - a * c)) / a;
			}
			else
			{
				return -1f;
			}
		}

		public static Vec3 SampleColor(Ray ray)
		{
			var t = HitSpehere(new Vec3(0f, 0f, 1f), 0.5f, ray);
			if (t > 1e-3f)
			{
				var normal = (ray.Point(t) - new Vec3(0f, 0f, 1f)).normalized;
				return normal * 0.5f + 0.5f;
			}

			Vec3 ud = ray.direction.normalized;
			t = ud.y * 0.5f + 0.5f;
			return Vec3.Lerp(BG_BOTTOM, BG_TOP, t);
		}

		[MenuItem("Raytracer/03")]
		public static void Trace()
		{
			int w = 256;
			int h = 128;

			// 基本的に左手系
			Vec3 lowerLeftCorner = new Vec3(-2f, -1f, 1f);
			Vec3 horizontal = new Vec3(4f, 0f, 0f);
			Vec3 vertical = new Vec3(0f, 2f, 0f);
			Vec3 origin = new Vec3(0f, 0f, 0f);

			Vec3[] c = new Vec3[w * h];

			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					var u = (float)x / w;
					var v = (float)y / h;
					Ray r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
					c[y * w + x] = SampleColor(r);
				}
			}

			ImageBuilder.SaveImage(c, w, h, "/Test/Images/03_ouput");
		}
	}
}