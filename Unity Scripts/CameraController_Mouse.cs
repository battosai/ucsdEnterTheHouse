using UnityEngine;
using System.Collections;

public class CameraController_Mouse : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;
	public float mouseSpeed;
	public Texture2D crosshairImage;
	public float xRotation, yRotation;
	
	void Start()
	{
		offset = transform.position - player.transform.position;
	}
	
	void FixedUpdate()
	{	
		xRotation -= Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
		yRotation += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
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

