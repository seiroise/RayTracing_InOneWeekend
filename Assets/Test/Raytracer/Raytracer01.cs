using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
	public class Raytracer01
	{
		public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
		public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

		public static Vec3 SampleColor(Ray ray)
		{
			Vec3 ud = ray.direction.normalized;
			float t = ud.y * 0.5f + 0.5f;
			return Vec3.Lerp(BG_BOTTOM, BG_TOP, t);
		}

		[MenuItem("Raytracer/01")]
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

			ImageBuilder.SaveImage(c, w, h, "/Test/Images/01_ouput");
		}
	}
}