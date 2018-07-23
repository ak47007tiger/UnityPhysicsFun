using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothMass2D : MonoBehaviour {

	public int x;
	public int y;

	public void ConnectMass(ClothMass2D clothMass2D)
	{
		if(clothMass2D != null)
		{
			//var joint = CreateRelativeJoint2D();
			var joint = CreateSpringJoint2D();
			joint.connectedBody = clothMass2D.GetComponent<Rigidbody2D>();
		}
	}

	void JointStrategy(ClothMass2D clothMass2D, RelativeJoint2D joint)
	{
		joint.linearOffset = clothMass2D.transform.localPosition - transform.localPosition;
	}

	RelativeJoint2D CreateRelativeJoint2D()
	{
		var joint = gameObject.AddComponent<RelativeJoint2D>();
		joint.autoConfigureOffset = false;
		joint.maxForce = 10;
		joint.maxTorque = 100;
		joint.breakForce = Random.Range(300, 400);
		joint.breakTorque = Random.Range(300, 400);
		return joint;
	}

	SpringJoint2D CreateSpringJoint2D()
	{
		var joint = gameObject.AddComponent<SpringJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.autoConfigureDistance = false;
		joint.anchor = Vector3.zero;
		joint.connectedAnchor = Vector3.zero;
		joint.distance = 1.2f;
		joint.dampingRatio = 0.8f;
		joint.frequency = 3;
		joint.breakForce = Random.Range(150, 300);
		return joint;
	}
}
