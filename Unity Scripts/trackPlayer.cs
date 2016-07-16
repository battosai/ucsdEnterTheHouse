using UnityEngine;
using System.Collections;

/*
 * Change depending on what you want to measure.
 * May also be dependent on how SNAP will interface
 * with Unity.
*/

public class trackPlayer : MonoBehaviour 
{
	GameObject player;

	void Awake()
	{
		player = GameObject.FindWithTag ("Player");
	}

	void Start()
	{
		print (Time.time);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player) 
		{
			print (Time.time);
		}
	}
}
