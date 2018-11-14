using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge_movement : MonoBehaviour 
{
	public GameObject empty;

	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			gameObject.transform.position = empty.transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis(0f, Vector3.right);
			gameObject.transform.rotation = Quaternion.AngleAxis(-92f, Vector3.up);
		//	gameObject.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
		}
	}
}
