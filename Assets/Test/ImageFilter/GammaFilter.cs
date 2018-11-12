namespace Test
{
	public class GammaFilter : ImageFilter
	{
		float factor;

		public GammaFilter(float factor) {
			this.factor = factor;
		}

		public override Vec3 Filter(Vec3 src)
		{
			return Vec3.Pow(src, 1f / factor);
		}
	}
}