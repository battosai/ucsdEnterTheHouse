using UnityEngine;
using System.Collections;

public class trapdoorBlocked : MonoBehaviour 
{
	public bool trapdoorReady;
	public int objectsInTheWay;

	void Awake()
	{
	}

	void Start()
	{
		trapdoorReady = false;
		objectsInTheWay = 0;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<liftMeUp>() != null)
			objectsInTheWay++;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<liftMeUp> () != null)
			objectsInTheWay--;
	}
		
	void Update()
	{
		if (objectsInTheWay > 0)
			trapdoorReady = false;
		else
			trapdoorReady = true;
	}
}
