using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public AudioClip footsteps;
	public float mouseSpeed;
	public float moveSpeed;
	public int clueNum;
	public AudioSource stereo;
	public Texture clue1, clue2, clue3, controlPage;
	public Texture found1, found2, found3;
	public bool menuOn;
	public RawImage currPage;
	
	GameObject controls;
	GameObject pageNum;
	GameObject theLift;
	
	bool moveFwd;
	bool moveBwd;
	bool moveLeft;
	bool moveRight;
	bool footstepsOn;

	string closedMenuLiftText = "While using the lift: Walk forward or sideways to carry the object with you. Walk backwards to drop the object.";
	string closedMenuText = "Press [Tab] for Menu";
	string openedMenuText = "";
	
	void Awake()
	{
		stereo = GetComponent<AudioSource>();

		theLift = GameObject.FindWithTag ("Lift");
		controls = GameObject.FindWithTag("Controls");
		controls.GetComponent<Text>().text = openedMenuText;
		pageNum = GameObject.FindWithTag("PageNum");
		currPage = GameObject.FindWithTag("Pages").GetComponent<RawImage>();
		currPage.texture = controlPage;
		currPage.enabled = true;

		found1 = null;
		found2 = null;
		found3 = null;
		
		footstepsOn = false;
		menuOn = true;
		clueNum = 1;
	}
	
	void FixedUpdate() //called just before any physics operations; physics code
	{
		bool inputEnabled = GameObject.FindWithTag("Door").GetComponent<doorUnlock>().userInputEnabled;
		
		//record when keys are pressed
        moveFwd = Input.GetKey(KeyCode.W);
        moveBwd = Input.GetKey(KeyCode.S);
		moveLeft = Input.GetKey(KeyCode.A);
		moveRight = Input.GetKey(KeyCode.D);
		
		//if endgame is reached, user input will be disabled from doorUnlock script
		if(inputEnabled)
		{
			//only move when W, A, S, or D is pressed
			if(moveFwd)
				transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
			else if(moveBwd)
				transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime * -1;
			if(moveLeft)
				transform.position = transform.position - (transform.right * moveSpeed * Time.deltaTime);
			else if(moveRight)
				transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
			
			//rotate player along with camera via mouse to allow strafing
			float horizontal = mouseSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
			transform.Rotate(0, horizontal, 0, Space.World);
		}
	}
	
	void Update()
	{	
		bool inputEnabled = GameObject.FindWithTag("Door").GetComponent<doorUnlock>().userInputEnabled;
	
		if(inputEnabled)
		{
			//if sound isn't playing and we're walking, play sound
			if((moveFwd || moveBwd || moveLeft || moveRight) && !footstepsOn)
			{
				footstepsOn = true;
				stereo.clip = footsteps;
				stereo.Play();
			}
			//pause if sound is playing and we've stopped walking
			else if(!moveFwd && !moveBwd && !moveLeft && !moveRight && footstepsOn)
			{
				stereo.Pause();
				footstepsOn = false;
			}
			
			if(Input.GetKeyDown(KeyCode.Tab))
			{
				menuOn = !menuOn;
				currPage.enabled = menuOn;
				
				if(menuOn)
					pageNum.GetComponent<Text>().text = "1/" + clueNum;
			}
			
			if(menuOn)
			{
				controls.GetComponent<Text>().text = openedMenuText;
				
				if(Input.GetKeyDown(KeyCode.Alpha1) || clueNum == 1)
				{
					pageNum.GetComponent<Text>().text = "1/" + clueNum;
					currPage.texture = controlPage;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha2) && clueNum >= 2)
				{
					pageNum.GetComponent<Text>().text = "2/" + clueNum;
					currPage.texture = found1;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha3) && clueNum >= 3)
				{
					pageNum.GetComponent<Text>().text = "3/" + clueNum;
					currPage.texture = found2;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha4) && clueNum >= 4)
				{
					pageNum.GetComponent<Text>().text = "4/" + clueNum;
					currPage.texture = found3;
				}
				
				pageNum.GetComponent<Text>().text = pageNum.GetComponent<Text>().text.Substring(0, pageNum.GetComponent<Text>().text.Length - 1);
				pageNum.GetComponent<Text>().text += clueNum;
			}
			else
			{
				pageNum.GetComponent<Text>().text = "";
				currPage.texture = controlPage;
				controls.GetComponent<Text>().text = closedMenuText;

				if (theLift != null && theLift.GetComponent<liftController> ().liftingObject)
					controls.GetComponent<Text> ().text = closedMenuLiftText;
			}
		}
	}
}
