using UnityEngine;

public abstract class Material {

	public abstract bool Scatter(Ray r_in, HitRecord rec, out Vec3 attenuation, out Ray scattered);
}