using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPlayer : MonoBehaviour
{
    public Vector2 excanvateSpeed;
    public bool excanvating;
    public Tunnel2DBuilder tunnel2DBuilder;
    public Rigidbody2D rb2d;
    public BoxCollider2D bc2d;
    PlayerPlatformerController _PlayerPlatformerController;
    Transform cacheTf;

    public float size;
    public BoxCollider2D groundBc2d;
	public Tunnel2D tunnel2D;

    List<Vector3> tunnelPath = new List<Vector3>();
    List<Vector3> tunnelPath1 = new List<Vector3>();
    List<Vector3> tunnelPath2 = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        _PlayerPlatformerController = GetComponent<PlayerPlatformerController>();
        cacheTf = transform;
    }

    float startExcanvateY;

    float ComputeSpeed(float v, float speed)
    {
        if (v != 0)
        {
            return v / Mathf.Abs(v) * speed;
        }
        return 0;
    }

    Vector3 Rotate(Vector3 v3, float angle)
    {
        float rangle = Mathf.Deg2Rad * angle;
        return new Vector3(
            v3.x * Mathf.Cos(rangle) - v3.y * Mathf.Sin(rangle),
            v3.x * Mathf.Sin(rangle) + v3.y * Mathf.Cos(rangle));
    }

    void AppendTunnelPathPoint(Vector3 point)
    {
        var last = tunnelPath[tunnelPath.Count - 1];
        var normal = (point - last).normalized;
        var d1 = Rotate(normal, 90);
        var d2 = Rotate(normal, -90);
        var p1 = point + d1 * size;
        var p2 = point + d2 * size;
        //Debug.Log(string.Format("n:{0},d1:{1},d2:{2}", normal, d1, d2));
        //Debug.Log(string.Format("p:{0},p1:{1},p2:{2}", point, p1, p2));
        tunnelPath.Add(point);

		// if(tunnelPath1.Count > 1 && Vector3.Distance(p1,tunnelPath1[tunnelPath1.Count - 1]) < size){
		// 	tunnelPath1.RemoveAt(tunnelPath1.Count - 1);
		// }
		// if(tunnelPath1.Count > 0 && Vector3.Distance(p1,tunnelPath1[tunnelPath1.Count - 1]) > size * 2){
		// 	var lp = Vector3.Lerp(tunnelPath1[tunnelPath1.Count - 1], p1,0.5f);
		// 	tunnelPath1.Add(lp);
		// }
        tunnelPath1.Add(p1);

		// if(tunnelPath2.Count > 1 && Vector3.Distance(p2,tunnelPath2[tunnelPath2.Count - 1]) < size){
		// 	tunnelPath2.RemoveAt(tunnelPath2.Count - 1);
		// }
		// if(tunnelPath2.Count > 0 && Vector3.Distance(p2,tunnelPath2[tunnelPath2.Count - 1]) > size * 2){
		// 	tunnelPath2.Add(Vector3.Lerp(tunnelPath2[tunnelPath2.Count - 1], p2,0.5f));
		// }
        tunnelPath2.Add(p2);
		tunnel2D.SetPaths(tunnelPath1,tunnelPath2);
		if(tunnelPath1.Count > 1 && tunnelPath2.Count > 1){
			tunnel2D.UpdateMesh();
		}
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        DrawLine(tunnelPath, Color.yellow);
        DrawLine(tunnelPath1, Color.green);
        DrawLine(tunnelPath2, Color.red);
    }

    void DrawLine(List<Vector3> path, Color color)
    {
        if (path.Count < 2)
        {
            return;
        }
        Gizmos.color = color;
		var cubeSize = Vector3.one * size * 0.5f;
        for (var i = 0; i < path.Count - 1; i++)
        {
            //Gizmos.DrawLine(path[i], path[i + 1]);
            Gizmos.DrawCube(path[i], cubeSize);
        }
        Gizmos.DrawCube(path[path.Count - 1], cubeSize);
    }

    private void Update()
    {
        if (Input.GetKeyUp("i"))
        {
            excanvating = !excanvating;
            if (excanvating)
            {
                startExcanvateY = cacheTf.localPosition.y;
                rb2d.bodyType = RigidbodyType2D.Kinematic;
                tunnelPath.Add(cacheTf.localPosition);
            }
            else
            {
                rb2d.bodyType = RigidbodyType2D.Dynamic;
            }
            bc2d.enabled = !excanvating;
            _PlayerPlatformerController.enabled = !excanvating;
        }

        if (excanvating)
        {
            Vector3 move = Vector3.zero;
            move.x = ComputeSpeed(Input.GetAxis("Horizontal"), excanvateSpeed.x);
            move.y = ComputeSpeed(Input.GetAxis("Vertical"), excanvateSpeed.y);
            var delatMovePos = move * Time.deltaTime;
            if (delatMovePos != Vector3.zero)
            {
                var nextPos = cacheTf.localPosition + delatMovePos;
                if (nextPos.y > startExcanvateY)
                {
                    nextPos.y = startExcanvateY;
                }
                cacheTf.localPosition = nextPos;
                if (Vector3.Distance(tunnelPath[tunnelPath.Count - 1], nextPos) > size && groundBc2d.OverlapPoint(nextPos))
                {
                    AppendTunnelPathPoint(nextPos);
                }
            }
        }
    }

}
