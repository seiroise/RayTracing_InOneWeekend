using System.Collections.Generic;

namespace Test
{
	public class HitableList : Hitable
	{
		public readonly List<Hitable> list;

		public HitableList()
		{
			list = new List<Hitable>();
		}

		public override bool Hit(Ray ray, float tMin, float tMax, ref HitRecord rec)
		{
			var tempRec = new HitRecord();
			var hitAnything = false;
			var closest = tMax;
			for (int i = 0; i < list.Count; ++i)
			{
				if (list[i].Hit(ray, tMin, closest, ref tempRec))
				{
					hitAnything = true;
					closest = tempRec.t;
					rec = tempRec;
				}
			}
			return hitAnything;
		}
	}
}