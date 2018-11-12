namespace Test
{
	public class ColorTexture : Texture
	{
		Vec3 color;

		public ColorTexture(Vec3 color)
		{
			this.color = color;
		}

		public override Vec3 Value(float u, float v, ref Vec3 p)
		{
			return color;
		}
	}
}