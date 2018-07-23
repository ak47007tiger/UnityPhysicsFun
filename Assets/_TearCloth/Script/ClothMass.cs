using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothMass : MonoBehaviour {

	public int x;
	public int y;

	public SpringJoint left;
	public SpringJoint top;
	public SpringJoint right;
	public SpringJoint bottom;

	public void TrimJoint()
	{
		DestroyJointSafe(left);
		DestroyJointSafe(top);
		DestroyJointSafe(right);
		DestroyJointSafe(bottom);
	}

	void DestroyJointSafe(SpringJoint joint)
	{
		if (joint.connectedBody == null)
			Destroy(joint);
	}

	public void JointConnectLeft(ClothMass clothMass)
	{
		if (clothMass != null && left == null)
			left = CreateJoint();
		JointConnect(left, clothMass);
	}

	public void JointConnectTop(ClothMass clothMass)
	{
		if (clothMass != null && top == null)
			top = CreateJoint();
		JointConnect(top, clothMass);
	}

	public void JointConnectRight(ClothMass clothMass)
	{
		if (clothMass != null && right == null)
			right = CreateJoint();
		JointConnect(right, clothMass);
	}

	public void JointConnectBottom(ClothMass clothMass)
	{
		if (clothMass != null && bottom == null)
			bottom = CreateJoint();
		JointConnect(bottom, clothMass);
	}

	void JointConnect(SpringJoint joint, ClothMass clothMass)
	{
		if (clothMass != null)
			joint.connectedBody = clothMass.GetComponent<Rigidbody>();
	}

	SpringJoint CreateJoint()
	{
		var joint = gameObject.AddComponent<SpringJoint>();
		joint.autoConfigureConnectedAnchor = false;
		joint.anchor = Vector3.zero;
		joint.connectedAnchor = Vector3.zero;
		joint.minDistance = 1;
		joint.maxDistance = 1;
		joint.enablePreprocessing = false;
		return joint;
	}
}
