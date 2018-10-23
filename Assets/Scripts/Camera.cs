public class Camera {

	public readonly Vec3 lowerLeftCorner;
	public readonly Vec3 horizontal;
	public readonly Vec3 vertical;
	public readonly Vec3 origin;

	public Camera()
	{
		lowerLeftCorner = new Vec3(-2f, -1f, 1f);
		horizontal = new Vec3(4f, 0f, 0f);
		vertical = new Vec3(0f, 2f, 0f);
		origin = new Vec3(0f, 0f, 0f);
	}

	public Ray GetRay(float u, float v)
	{
		return new Ray(origin, lowerLeftCorner + horizontal * u + vertical * v - origin);
	}
}