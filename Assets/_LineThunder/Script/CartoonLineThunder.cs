using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonLineThunder : MonoBehaviour {

	public int emitFreequence;
	float partCount;
	public float partCountPerD1;
	public float flySize;
	public float perlineFlySize;
	public Vector3 startPos;
	public Vector3 endPos;
	public Transform startTf;
	public Transform endTf;
	List<Vector3> linePoints = new List<Vector3>();
	LineRenderer _LineRenderer;

	int emitPerSecond;
	
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
			startPos = startTf.position;
			endPos = endTf.position;
			UpdateThunder();
		}
	}

	public void UpdateThunder(){
		linePoints.Clear();
		startPos = transform.InverseTransformPoint(startPos);
		endPos = transform.InverseTransformPoint(endPos);
		linePoints.Add(startPos);
		partCount = Vector3.Distance(startPos, endPos) * partCountPerD1;
		var step = 1f / partCount;
		for(var t = 0f; t < 1f; t+=step){
			var pos = Vector3.Lerp(startPos, endPos, t);
			
			linePoints.Add(pos + RandomOffset(flySize));
		}
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
