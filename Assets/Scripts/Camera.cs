using UnityEngine;

public class Camera {

	public readonly Vec3 lowerLeftCorner;
	public readonly Vec3 horizontal;
	public readonly Vec3 vertical;
	public readonly Vec3 origin;
	public readonly Vec3 u, v, w;

	public readonly float lensRadius;

	public Camera()
	{
		lowerLeftCorner = new Vec3(-2f, -1f, 1f);
		horizontal = new Vec3(4f, 0f, 0f);
		vertical = new Vec3(0f, 2f, 0f);
		origin = new Vec3(0f, 0f, 0f);
	}

	public Camera(float vfov, float aspect)
	{
		var theta = vfov * Mathf.Deg2Rad;
		var hHeight = Mathf.Tan(theta * 0.5f);
		var hWidth = aspect * hHeight;
		lowerLeftCorner = new Vec3(-hWidth, -hHeight, 1f);
		horizontal = new Vec3(hWidth * 2f, 0f, 0f);
		vertical = new Vec3(0f, hHeight * 2f, 0f);
		origin = new Vec3(0f, 0f, 0f);
	}

	public Camera(Vec3 lookfrom, Vec3 lookat, Vec3 vup, float vfov, float aspect, float aperture, float focusDist = 1f)
	{
		lensRadius = aperture * 0.5f;
		var theta = vfov * Mathf.Deg2Rad;
		var hHeight = Mathf.Tan(theta * 0.5f);
		var hWidth = aspect * hHeight;
		origin = lookfrom;
		// w = (lookfrom - lookat).normalized; 右手系
		w = (lookat - lookfrom).normalized;
		u = Vec3.Cross(vup, w);
		v = Vec3.Cross(w, u);

		// lowerLeftCorner = origin - hWidth * u - hHeight * v + w;
		// horizontal = 2f * hWidth * u;
		// vertical = 2f * hHeight * v;

		lowerLeftCorner = origin - (hWidth * focusDist* u) - (hHeight * focusDist * v) + focusDist * w;
		horizontal = 2f * hWidth * focusDist * u;
		vertical = 2f * hHeight * focusDist * v;
	}

	public Ray GetRay(float s, float t)
	{
		return new Ray(origin, lowerLeftCorner + horizontal * s + vertical * t - origin);
	}

	public Ray GetRayWithOffset(float s, float t)
	{
		var rd = lensRadius * Vec3.randomInUnitDiscXY;
		var offset = u * rd.x + v * rd.y;
		return new Ray(origin + offset, lowerLeftCorner + horizontal * s + vertical * t - origin - offset);
	}
}