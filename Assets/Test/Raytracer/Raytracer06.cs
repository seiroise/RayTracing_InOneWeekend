using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
	public class Raytracer06
	{

		public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
		public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

		public static Vec3 SampleColor(Ray ray, Hitable scene)
		{
			var rec = new HitRecord();
			if (scene.Hit(ray, 1e-3f, float.MaxValue, ref rec))
			{
				// 反射、減衰はとりあえず0.5なので灰色の想定
				Vec3 target = rec.p + rec.n + Vec3.randomInUnitSphere;
				return SampleColor(new Ray(rec.p, target - rec.p), scene) * 0.5f;
			}
			else
			{
				Vec3 ud = ray.direction.normalized;
				var t = ud.y * 0.5f + 0.5f;
				return Vec3.Lerp(BG_BOTTOM, BG_TOP, t);
			}
		}

		[MenuItem("Raytracer/06")]
		public static void Trace()
		{
			int w = 256;
			int h = 128;
			int samples = 16;

			// 基本的に左手系
			var camera = new Camera();

			var scene = new HitableList();
			scene.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f));
			scene.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f));

			Vec3[] c = new Vec3[w * h];

			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					Vec3 col = new Vec3(0f, 0f, 0f);
					for (int i = 0; i < samples; ++i)
					{
						var u = (x + Random.value) / w;
						var v = (y + Random.value) / h;
						Ray r = camera.GetRay(u, v);
						col += SampleColor(r, scene);
					}
					c[y * w + x] = Vec3.Pow(col / samples, 1f / 2.2f);
				}
			}

			ImageBuilder.SaveImage(c, w, h, "/Test/Images/06_ouput");
		}
	}
}