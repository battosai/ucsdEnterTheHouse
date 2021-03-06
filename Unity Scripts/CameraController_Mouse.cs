﻿using UnityEngine;
using System.Collections;

public class CameraController_Mouse : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;
	public float mouseSpeed;
	public Texture2D crosshairImage;
	public float xRotation, yRotation;

	float timestamp;
	
	void Awake()
	{
		Cursor.visible = false;
	}
	
	void Start()
	{
		offset = transform.position - player.transform.position;
		timestamp = Time.time;
		AudioListener.volume = 0.0f;
	}
	
	void FixedUpdate()
	{	
		xRotation -= Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
		xRotation = Mathf.Clamp(xRotation, -50, 55);
		
		yRotation += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}

	void Update()
	{
		if (Time.time - timestamp >= 0.6) 
		{
			AudioListener.volume = 1.0f;
		}
	}

	void LateUpdate() //runs every frame like Update, but guaranteed to run after all updates have been processed (make sure player has moved for each frame)
	{
		transform.position = player.transform.position + offset;
	}
	
	//crosshairs
	void OnGUI()
	{
		float xMin = (Screen.width/2) - (crosshairImage.width/2);
		float yMin = (Screen.height/2) - (crosshairImage.height/2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}
}

