using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidOffsetThunder : MonoBehaviour
{
    public float distance;

    public int emitFreequence;
    float partCount;
    public float partCountPerD1;
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

	void UpdateThunder(){
		linePoints.Clear();
		startPos = transform.InverseTransformPoint(startPos);
		endPos = transform.InverseTransformPoint(endPos);
		linePoints.Add(startPos);
		partCount = Vector3.Distance(startPos, endPos) * partCountPerD1;
		var step = 1f / partCount;
		var s = startPos;
		var d = distance;
		var e = endPos;
		for(var t = 0f; t < 1f; t+=step){
			var m = FillLine(s,e, d);
			e = m;
			linePoints.Insert(1,m);
			d *= 0.5f;
		}
		linePoints.Add(endPos);

		//linePoints.Add(endPos);
		_LineRenderer.positionCount = linePoints.Count;
		_LineRenderer.SetPositions(linePoints.ToArray());
	}

	void UpdateThunder1(){
		linePoints.Clear();
		startPos = transform.InverseTransformPoint(startPos);
		endPos = transform.InverseTransformPoint(endPos);
		linePoints.Add(startPos);
		partCount = Vector3.Distance(startPos, endPos) * partCountPerD1;
		var step = 1f / partCount;
		var s = startPos;
		var d = distance;
		for(var t = 0f; t < 1f; t+=step){
			var m = FillLine(s,endPos, d);
			s = m;
			linePoints.Add(m);
			d *= 0.5f;
		}
		//linePoints.Add(endPos);
		_LineRenderer.positionCount = linePoints.Count;
		_LineRenderer.SetPositions(linePoints.ToArray());
	}

    Vector3 FillLine(Vector3 start, Vector3 end, float distance)
    {
		var mid = (start + end) * 0.5f;
		mid.x += distance * (Random.Range(0f,1f) - 0.5f);
		mid.y += distance * (Random.Range(0f,1f) - 0.5f);
		mid.z += distance * (Random.Range(0f,1f) - 0.5f);
		return mid;
    }
}
