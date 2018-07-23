using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearCloth2D : MonoBehaviour {

	public int xcount;
	public int ycount;

	public float offset;

	Vector3 posOffset;

	public GameObject prefab_mass;

	ClothMass2D[,] massGrid;

	private void Start()
	{
		Random.InitState(System.DateTime.Now.Millisecond);
		CreateMass();
	}

	void CreateMass()
	{
		posOffset = new Vector3(xcount * offset, ycount * offset) * -0.5f;

		massGrid = new ClothMass2D[xcount, ycount];
		var transform = this.transform;
		for (var x = 0; x < xcount; x++)
		{
			for (int y = 0; y < ycount; y++)
			{
				var mass = Instantiate(prefab_mass, transform).GetComponent<ClothMass2D>();
				massGrid[x, y] = mass;
				mass.x = x;
				mass.y = y;
				mass.transform.localPosition = ComputeInitialPos(x, y);
			}
		}
		for (var x = 0; x < xcount; x++)
		{
			for (int y = 0; y < ycount; y++)
			{
				var mass = massGrid[x, y];
				Joints1(mass, x, y);
			}
		}
		GetMass(0, 0).GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void JointStrategy1()
	{
		for (var x = 0; x < xcount; x++)
		{
			for (int y = 0; y < ycount; y++)
			{
				var mass = massGrid[x, y];
				Joints(mass, x, y);
			}
		}
	}

	void Joints(ClothMass2D clothMass2D, int baseX, int baseY)
	{
		clothMass2D.ConnectMass(GetMass(-1 + baseX, 0 + baseY));
		clothMass2D.ConnectMass(GetMass(0 + baseX, 1 + baseY));
		clothMass2D.ConnectMass(GetMass(1 + baseX, 0 + baseY));
		clothMass2D.ConnectMass(GetMass(0 + baseX, -1 + baseY));
	}

	void Joints1(ClothMass2D clothMass2D, int baseX, int baseY)
	{
		for (var x = -1; x < 2; x++)
		{
			for (int y = -1; y < 2; y++)
			{
				if (x == 0 && y == 0)
					continue;

				clothMass2D.ConnectMass(GetMass(x + baseX, y + baseY));
			}
		}
	}

	ClothMass2D GetMass(int x, int y)
	{
		if (0 <= x && x < xcount && 0 <= y && y < ycount)
			return massGrid[x, y];
		return null;
	}

	Vector3 ComputeInitialPos(int x, int y)
	{
		return new Vector3(x * offset, y * offset) + posOffset;
	}
}
