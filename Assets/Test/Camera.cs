namespace Test
{
	public class Camera
	{
		Vec3 origin;
		Vec3 lowerLeftCorner;
		Vec3 horizontal;
		Vec3 vertical;

		public Camera()
		{
			origin = new Vec3(0f, 0f, 0f);
			lowerLeftCorner = new Vec3(-2f, -1f, 1f);
			horizontal = new Vec3(4f, 0f, 0f);
			vertical = new Vec3(0f, 2f, 0f);
		}

		public Ray GetRay(float u, float v)
		{
			return new Ray(origin, lowerLeftCorner + horizontal * u + vertical * v - origin);
		}
	}
}