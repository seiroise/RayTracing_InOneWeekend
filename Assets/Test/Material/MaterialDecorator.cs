namespace Test
{
	public class Textured : Material
	{
		Material mat;
		Texture tex;

		public Textured(Material mat, Texture tex)
		{
			this.tex = tex;
			this.mat = mat;
		}

		public override bool Scatter(Ray ray, ref HitRecord hitRec, ref ScatterRecord sctRec)
		{
			var result = mat.Scatter(ray, ref hitRec, ref sctRec);
			sctRec.attenuation = tex.Value(hitRec.u, hitRec.v, ref hitRec.p);

			return result;
		}
	}
}