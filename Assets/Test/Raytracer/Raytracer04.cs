﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
	public class Raytracer04
	{

		public static readonly Vec3 BG_TOP = new Vec3(0.5f, 0.7f, 1f);
		public static readonly Vec3 BG_BOTTOM = new Vec3(1f, 1f, 1f);

		public static Vec3 SampleColor(Ray ray, Hitable scene)
		{
			var rec = new HitRecord();
			if (scene.Hit(ray, 0f, float.MaxValue, ref rec))
			{
				return rec.n * 0.5f + 0.5f;
			}
			else
			{
				Vec3 ud = ray.direction.normalized;
				var t = ud.y * 0.5f + 0.5f;
				return Vec3.Lerp(BG_BOTTOM, BG_TOP, t);
			}
		}

		[MenuItem("Raytracer/04")]
		public static void Trace()
		{
			int w = 256;
			int h = 128;

			// 基本的に左手系
			Vec3 lowerLeftCorner = new Vec3(-2f, -1f, 1f);
			Vec3 horizontal = new Vec3(4f, 0f, 0f);
			Vec3 vertical = new Vec3(0f, 2f, 0f);
			Vec3 origin = new Vec3(0f, 0f, 0f);

			var scene = new HitableList();
			scene.list.Add(new Sphere(new Vec3(0f, 0f, 1f), 0.5f));
			scene.list.Add(new Sphere(new Vec3(0f, -100.5f, 1f), 100f));

			Vec3[] c = new Vec3[w * h];

			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					var u = (float)x / w;
					var v = (float)y / h;
					Ray r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
					c[y * w + x] = SampleColor(r, scene);
				}
			}

			ImageBuilder.SaveImage(c, w, h, "/Test/Images/04_ouput");
		}
	}
}