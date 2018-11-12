using UnityEngine;

namespace Test
{
	public class CheckerTexture : Texture
	{
		Texture t0;
		Texture t1;
		float freq;

		public CheckerTexture(Texture t0, Texture t1, float freq)
		{
			this.t0 = t0;
			this.t1 = t1;
			this.freq = freq;
		}

		public override Vec3 Value(float u, float v, ref Vec3 p)
		{
			float sines = Mathf.Sin(freq * p.x) * Mathf.Sin(freq * p.y) * Mathf.Sin(freq * p.z);
			if (sines < 0)
			{
				return t0.Value(u, v, ref p);
			}
			else
			{
				return t1.Value(u, v, ref p);
			}
		}
	}
}