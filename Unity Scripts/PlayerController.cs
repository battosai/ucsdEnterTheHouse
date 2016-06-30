using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public AudioClip footsteps;
	public float mouseSpeed;
	public float moveSpeed;
	public AudioSource stereo;
	public Texture clue1, clue2, clue3, controlPage;
	public bool menuOn;
	public RawImage currPage;
	
	GameObject controls;
	GameObject pageNum;
	
	bool moveFwd;
	bool moveBwd;
	bool moveLeft;
	bool moveRight;
	bool footstepsOn;
	
	string closedMenuText = "Press [Tab] for Menu";
	string openedMenuText = "";
	
	void Awake()
	{
		stereo = GetComponent<AudioSource>();
		
		controls = GameObject.FindWithTag("Controls");
		controls.GetComponent<Text>().text = openedMenuText;
		pageNum = GameObject.FindWithTag("PageNum");
		currPage = GameObject.FindWithTag("Pages").GetComponent<RawImage>();
		currPage.texture = controlPage;
		currPage.enabled = true;
		
		footstepsOn = false;
		menuOn = true;
	}
	
	void Start()
	{
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
		bool hasClue1 = GetComponent<iPickYouUp>().hasClue1;
		bool hasClue2 = GetComponent<iPickYouUp>().hasClue2;
		bool hasClue3 = GetComponent<iPickYouUp>().hasClue3;
		int totalPageNum;
		
		if(hasClue3)
		{
			totalPageNum = 4;
		}
		else if(hasClue2)
		{
			totalPageNum = 3;
		}
		else if(hasClue1)
		{
			totalPageNum = 2;
		}
		else
		{
			totalPageNum = 1;
		}
	
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
					pageNum.GetComponent<Text>().text = "1/" + totalPageNum;
			}
			
			if(menuOn)
			{
				controls.GetComponent<Text>().text = openedMenuText;
				
				if(Input.GetKeyDown(KeyCode.Alpha1) || (!hasClue1 && !hasClue2 && !hasClue3))
				{
					pageNum.GetComponent<Text>().text = "1/" + totalPageNum;
					currPage.texture = controlPage;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha2) && hasClue1)
				{
					pageNum.GetComponent<Text>().text = "2/" + totalPageNum;
					currPage.texture = clue1;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha3) && hasClue2)
				{
					pageNum.GetComponent<Text>().text = "3/" + totalPageNum;
					currPage.texture = clue2;
				}
				else if(Input.GetKeyDown(KeyCode.Alpha4) && hasClue3)
				{
					pageNum.GetComponent<Text>().text = "4/" + totalPageNum;
					currPage.texture = clue3;
				}
				
				pageNum.GetComponent<Text>().text = pageNum.GetComponent<Text>().text.Substring(0, pageNum.GetComponent<Text>().text.Length - 1);
				pageNum.GetComponent<Text>().text += totalPageNum;
			}
			else
			{
				pageNum.GetComponent<Text>().text = "";
				currPage.texture = controlPage;
				controls.GetComponent<Text>().text = closedMenuText;
			}
		}
	}
}
