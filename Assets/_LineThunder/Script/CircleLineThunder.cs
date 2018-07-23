using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLineThunder : MonoBehaviour {

	public int emitFreequence;
	float partCount;
	public float partCountPerD1;
	public float flySize;
	public float radius;
	public float perlineFlySize;

	List<Vector3> linePoints = new List<Vector3>();
	LineRenderer _LineRenderer;

	int emitPerSecond;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_LineRenderer = GetComponent<LineRenderer>();
		emitPerSecond = (int)(60f / emitFreequence);
	}

	int _count = 0;
	private void Update() {
		_count++;
		if(_count == emitPerSecond){
			_count = 0;
			
			UpdateThunder();
		}
	}

	Vector3 CirclePos(float radius, float radian){
		var x = Mathf.Cos(radian);
		var y = Mathf.Sin(radian);
		return new Vector3(x,y,0) * radius;
	}

	public void UpdateThunder(){
		linePoints.Clear();
		partCount = Mathf.PI * radius * 2 * partCountPerD1;
		var step = 1f / partCount;
		var firstPos = CirclePos(radius, 0) + PerlineOffset(perlineFlySize);
		linePoints.Add(firstPos);
		for(var t = step; t < 1f; t+=step){
			linePoints.Add(CirclePos(radius, t * Mathf.PI * 2) + PerlineOffset(perlineFlySize));
		}
		linePoints.Add(firstPos);
		//linePoints.Add(endPos);
		_LineRenderer.positionCount = linePoints.Count;
		_LineRenderer.SetPositions(linePoints.ToArray());
	}

	Vector3 RandomOffset(float size){
		return new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)) * size;
	}

	Vector3 PerlineOffset(float size){
		var noiseX = Mathf.Lerp(-size,size,Mathf.PerlinNoise(Random.Range(0f,1f), Random.Range(0f,1f)));
		var noiseY = Mathf.Lerp(-size,size,Mathf.PerlinNoise(Random.Range(0f,1f), Random.Range(0f,1f)));
		var noiseZ = Mathf.Lerp(-size,size,Mathf.PerlinNoise(Random.Range(0f,1f), Random.Range(0f,1f)));
		return new Vector3(noiseX, noiseY, noiseZ);
	}
}
