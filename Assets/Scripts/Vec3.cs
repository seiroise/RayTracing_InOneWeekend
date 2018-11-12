using UnityEngine;

public struct Vec3
{
	float e0, e1, e2;
	public Vec3(float v)
	{
		e0 = e1 = e2 = v;
	}
	public Vec3(float e0, float e1, float e2)
	{
		this.e0 = e0;
		this.e1 = e1;
		this.e2 = e2;
	}

	public float x
	{
		get { return e0; }
		set { e0 = value; }
	}
	public float y
	{
		get { return e1; }
		set { e1 = value; }
	}
	public float z
	{
		get { return e2; }
		set { e2 = value; }
	}
	public float r
	{
		get { return e0; }
		set { e0 = value; }
	}
	public float g
	{
		get { return e1; }
		set { e1 = value; }
	}
	public float b
	{
		get { return e2; }
		set { e2 = value; }
	}

	public static Vec3 operator -(Vec3 a)
	{
		return new Vec3(-a.x, -a.y, -a.z);
	}
	public static Vec3 operator +(Vec3 a, Vec3 b)
	{
		return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
	}
	public static Vec3 operator +(Vec3 a, float s)
	{
		return new Vec3(a.x + s, a.y + s, a.z + s);
	}
	public static Vec3 operator +(float s, Vec3 a)
	{
		return new Vec3(a.x + s, a.y + s, a.z + s);
	}
	public static Vec3 operator -(Vec3 a, Vec3 b)
	{
		return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
	}
	public static Vec3 operator -(Vec3 a, float s)
	{
		return new Vec3(a.x - s, a.y - s, a.z - s);
	}
	public static Vec3 operator -(float s, Vec3 a)
	{
		return new Vec3(a.x - s, a.y - s, a.z - s);
	}
	public static Vec3 operator *(Vec3 a, float s)
	{
		return new Vec3(a.x * s, a.y * s, a.z * s);
	}
	public static Vec3 operator *(float s, Vec3 a)
	{
		return new Vec3(a.x * s, a.y * s, a.z * s);
	}
	public static Vec3 operator *(Vec3 a, Vec3 b)
	{
		return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
	}
	public static Vec3 operator /(Vec3 a, float s)
	{
		return new Vec3(a.x / s, a.y / s, a.z / s);
	}
	public static Vec3 operator /(Vec3 a, Vec3 b)
	{
		return new Vec3(a.x / b.x, a.y / b.y, a.z / b.z);
	}
	public static float Dot(Vec3 a, Vec3 b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}
	public static Vec3 Cross(Vec3 a, Vec3 b)
	{
		return new Vec3(
			a.y * b.z - a.z * b.y,
			a.z * b.x - a.x * b.z,
			a.x * b.y - a.y * b.x
		);
	}
	public static Vec3 Reflect(Vec3 v, Vec3 n)
	{
		return v - (2f * Dot(v, n)) * n;
	}
	public static bool Refract(Vec3 v, Vec3 n, float n1OverN2, ref Vec3 refracted)
	{
		var uv = v.normalized;
		var dt = Dot(uv, n);
		var d = 1f - n1OverN2 * n1OverN2 * (1f - dt * dt);
		if (d > 0f)
		{
			refracted = n1OverN2 * (uv - n * dt) - n * Mathf.Sqrt(d);
			// refracted = -niOverNt * (uv - n * dt) - n * Mathf.Sqrt(d);
			return true;
		}
		else
		{
			return false;
		}
	}
	public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
	{
		return a * (1f - t) + b * t;
	}
	public static Vec3 Pow(Vec3 a, float exp)
	{
		return new Vec3(Mathf.Pow(a.x, exp), Mathf.Pow(a.y, exp), Mathf.Pow(a.z, exp));
	}

	public static Vec3 randomInUnitSphere
	{
		get
		{
			Vec3 p;
			do
			{
				p = (2f * new Vec3(Random.value, Random.value, Random.value)) - 1f;
			} while (p.sqrLength >= 1f);
			return p;
		}
	}
	public static Vec3 randomInUnitDiscXY
	{
		get
		{
			Vec3 p;
			Vec3 xy = new Vec3(1f, 1f, 0f);
			do
			{
				p = 2f * new Vec3(Random.value, Random.value, 0f) - xy;
			} while (p.sqrLength >= 1f);
			return p;
		}
	}
	public static Vec3 randomInUnitDiscXZ
	{
		get
		{
			Vec3 p;
			Vec3 xy = new Vec3(1f, 0f, 1f);
			do
			{
				p = 2f * new Vec3(Random.value, 0f, Random.value) - xy;
			} while (p.sqrLength >= 1f);
			return p;
		}
	}

	public float length { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
	public float sqrLength { get { return x * x + y * y + z * z; } }
	public Vec3 normalized
	{
		get
		{
			float k = 1f / length;
			return new Vec3(x * k, y * k, z * k);
		}
	}

	public void MakeUnit()
	{
		float k = 1f / length;
		e0 *= k; e1 *= k; e2 *= k;
	}



	public override string ToString()
	{
		return string.Format("({0},{1},{2})", x, y, z);
	}
}