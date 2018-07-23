using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel2D : MonoBehaviour
{
	public float size;
	public Texture texture;

	public int[] meshUvStartEnd;
	public Vector2 uvStartEnd;

    MeshFilter _MeshFilter;
    Mesh mesh;

	List<Vector3> path1;
    List<Vector3> path2;

	List<Vector3> verticesList0 = new List<Vector3>();
	List<Vector3> verticesList = new List<Vector3>();
	List<int> trianglesList = new List<int>();
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

	MaterialPropertyBlock propertyBlock;

	public void SetPaths(List<Vector3> path1, List<Vector3> path2){
		this.path1 = path1;
		this.path2 = path2;
	}

    // Use this for initialization
    void Start()
    {
        _MeshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
		propertyBlock = new MaterialPropertyBlock();

		propertyBlock.SetTexture("_MainTex",texture);

		GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);
    }

	Vector3 GetPos(float t, List<Vector3> path){
		var p = Mathf.Min(Mathf.Max(t,0),1);
		if(p == 1) return path[path.Count - 1];
		if(p == 0) return path[0];

		var index = p * (path.Count - 1);
		var start = path[(int)index];
		var end = path[((int)index) + 1];
		return Vector3.Lerp(start,end, index - (int)index);
	}

    public void UpdateMesh1()
    {
		trianglesList.Clear();

		var count = Mathf.Max(path1.Count, path2.Count);
		vertices = new Vector3[count * 2];
		var j = 0;
		var step = 1f/(count - 1);
		for(var i = 0f; i <= 1f; i+=step){
			vertices[j] = GetPos(i, path1);
			j++;
			vertices[j] = GetPos(i, path2);
			j++;
		}
		Debug.Log(string.Format("{0},{1}",j,count));
		triangles = new int[(count - 1) * 2 * 3];
		j = 0;
		var k = 0;
		for(var i = 0;  i < (count - 1); i++){
			j = i * 2;
			triangles[k++] = j;
			triangles[k++] = j + 3;
			triangles[k++] = j + 1;

			triangles[k++] = j;
			triangles[k++] = j + 2;
			triangles[k++] = j + 3;
		}
		var lb = Vector2.zero;
		var lt = new Vector2(0,1);
		var rb = new Vector2(1,0);
		var rt = new Vector2(1,1);
		uv = new Vector2[vertices.Length];
		uv[0] = Vector2.zero;
		uv[1] = new Vector2(0,1);
		uv[uv.Length - 2] = new Vector2(1,0);
		uv[uv.Length - 1] = new Vector2(1,1);
		for(var i = 0; i < uv.Length - 4; i +=4){
			uv[i] = lb;
			uv[i+1] = lt;
			uv[i+2] = rb;
			uv[i+3] = rt;
		}
		//triangles = trianglesList.ToArray();

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
        _MeshFilter.mesh = mesh;
    }

	public void UpdateMesh()
    {
		trianglesList.Clear();

		var count = Mathf.Min(path1.Count, path2.Count);
		vertices = new Vector3[count * 2];
		for(var i = 0;  i < count; i++){
			vertices[i * 2] = path1[i];
			vertices[i * 2 + 1] = path2[i];
		}
		triangles = new int[(count - 1) * 2 * 3];
		var k = 0;
		for(var i = 0;  i < (count - 1); i++){
			var j = i * 2;
			triangles[k++] = j;
			triangles[k++] = j + 3;
			triangles[k++] = j + 1;

			triangles[k++] = j;
			triangles[k++] = j + 2;
			triangles[k++] = j + 3;
		}
		var lb = Vector2.zero;
		var lt = new Vector2(0,1);
		var rb = new Vector2(1,0);
		var rt = new Vector2(1,1);
		uv = new Vector2[vertices.Length];
		uv[0] = Vector2.zero;
		uv[1] = new Vector2(0,1);
		uv[uv.Length - 2] = new Vector2(1,0);
		uv[uv.Length - 1] = new Vector2(1,1);
		for(var i = 0; i < uv.Length - 4; i +=4){
			uv[i] = lb;
			uv[i+1] = lt;
			uv[i+2] = rb;
			uv[i+3] = rt;
		}
		//triangles = trianglesList.ToArray();

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
        _MeshFilter.mesh = mesh;
    }

}
