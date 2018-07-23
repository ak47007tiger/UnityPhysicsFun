using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearCloth : MonoBehaviour {

	public int xcount;
	public int ycount;

	public float offset;

	Vector3 posOffset;

	public GameObject prefab_mass;

	ClothMass[,] massGrid;

	private void Start()
	{
		CreateMass();
	}

	void CreateMass()
	{
		posOffset = new Vector3(xcount * offset, ycount * offset) * -0.5f;

		massGrid = new ClothMass[xcount, ycount];
		var transform = this.transform;
		for(var x = 0; x < xcount; x++)
		{
			for (int y = 0; y < ycount; y++)
			{
				var mass = Instantiate(prefab_mass, transform).GetComponent<ClothMass>();
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
				mass.JointConnectLeft(Left(mass));
				mass.JointConnectTop(Top(mass));
				mass.JointConnectRight(Right(mass));
				mass.JointConnectBottom(Bottom(mass));
			}
		}

	}

	public ClothMass Left(ClothMass mass)
	{
		var tx = mass.x - 1;
		var ty = mass.y;
		if (0 <= tx && tx < xcount)
		{
			return massGrid[tx, ty];
		}
		return null;
	}

	public ClothMass Right(ClothMass mass)
	{
		var tx = mass.x + 1;
		var ty = mass.y;
		if (0 <= tx && tx < xcount)
		{
			return massGrid[tx, ty];
		}
		return null;
	}

	public ClothMass Top(ClothMass mass)
	{
		var tx = mass.x;
		var ty = mass.y + 1;
		if (0 <= ty && ty < ycount)
		{
			return massGrid[tx, ty];
		}
		return null;
	}

	public ClothMass Bottom(ClothMass mass)
	{
		var tx = mass.x;
		var ty = mass.y - 1;
		if (0 <= ty && ty < ycount)
		{
			return massGrid[tx, ty];
		}
		return null;
	}

	Vector3 ComputeInitialPos(int x, int y)
	{
		return new Vector3(x * offset, y * offset) + posOffset;
	}
}
