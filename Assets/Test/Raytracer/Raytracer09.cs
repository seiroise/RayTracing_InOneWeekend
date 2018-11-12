using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
	public class Raytracer09
	{

		public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
		public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

		public static Vec3 SampleColor(Ray ray, Hitable scene, int depth)
		{
			var hitRec = new HitRecord();
			var sctRec = new ScatterRecord();
			if (scene.Hit(ray, 1e-3f, float.MaxValue, ref hitRec))
			{
				if (depth < 50 && hitRec.mat.Scatter(ray, ref hitRec, ref sctRec))
				{
					return sctRec.attenuation * SampleColor(sctRec.ray, scene, depth + 1);
				}
				else
				{
					return new Vec3(0f, 0f, 0f);
				}
			}
			else
			{
				Vec3 ud = ray.direction.normalized;
				var t = ud.y * 0.5f + 0.5f;
				return Vec3.Lerp(BG_BOTTOM, BG_TOP, t);
			}
		}

		[MenuItem("Raytracer/09")]
		public static void Trace()
		{
			int w = 256;
			int h = 128;
			int samples = 32;

			// 基本的に左手系
			var camera = new Camera();
			var scene = new HitableList();

			// textures
			var redTex = new ColorTexture(new Vec3(1f, 0.3f, 0.2f));
			var yellowTex = new ColorTexture(new Vec3(1f, 1f, 0.2f));
			var grayTex = new ColorTexture(new Vec3(0.7f, 0.7f, 0.7f));
			var checkerTex = new CheckerTexture(redTex, yellowTex, 10f);

			// materials
			var redLamb = new Textured(new Lambertian(), redTex);
			var grayLamb = new Textured(new Lambertian(), grayTex);
			var glass = new Dielectric(1.5f);
			var gold = new Textured(new Metal(0.5f), yellowTex);
			var checkerLamb = new Textured(new Lambertian(), checkerTex);

			// shapes
			scene.list.Add(new Sphere(new Vec3(-0.6f, 0f, 1f), 0.5f, redLamb));
			scene.list.Add(new Sphere(new Vec3(0f, -0.2f, 0.6f), 0.15f, gold));
			scene.list.Add(new Sphere(new Vec3(0.6f, 0f, 1f), 0.5f, glass));
			// scene.list.Add(new Sphere(new Vec3(0.6f, 0f, 1f), -0.45f, glass));

			// 地面(とにかくでかい球)
			// scene.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f, grayLamb));
			scene.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f, checkerLamb));

			var imgFilter = new GammaFilter(2.2f);
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
						col += SampleColor(r, scene, 0);
					}
					c[y * w + x] = imgFilter.Filter(col / samples);
				}
			}

			ImageBuilder.SaveImage(c, w, h, "/Test/Images/09_ouput");
		}
	}
}