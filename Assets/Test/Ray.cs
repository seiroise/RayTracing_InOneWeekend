using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
	public class Ray
	{
		Vec3 o, d;

		public Ray(Vec3 o, Vec3 d)
		{
			this.o = o;
			this.d = d;
		}

		public Vec3 origin
		{
			get { return o; }
		}
		public Vec3 direction
		{
			get { return d; }
		}

		public Vec3 Point(float t)
		{
			return o + d * t;
		}
	}
}