using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableList : Hitable
{
	public readonly List<Hitable> list;

	public HitableList()
	{
		list = new List<Hitable>();
	}

	public override bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
	{
		var tempRec = new HitRecord();
		var hitAnything = false;
		var closetSoFar = tMax;

		for (int i = 0; i < list.Count; ++i)
		{
			if (list[i].Hit(r, tMin, closetSoFar, ref tempRec))
			{
				hitAnything = true;
				closetSoFar = tempRec.t;	// レイの当たった位置でクリップしていく
				rec = tempRec;
			}
		}
		return hitAnything;
	}
}