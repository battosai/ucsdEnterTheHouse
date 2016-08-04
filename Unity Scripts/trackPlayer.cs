using UnityEngine;
using System.Collections;
using LSL;

/*		For narrow scenario: mark when pick up key items, look at different pages,
 * 		For broad scenario:  have player press key when they have an idea
 */ 

public class trackPlayer : MonoBehaviour {
	public int scenario;

	iPickYouUp narrowPickUpScript;
	bool contRun1, contRun2, contRun3, contRun4;

	liblsl.StreamInfo inf;
	liblsl.StreamOutlet outl;
	string[] strings;

	void Awake()
	{
		narrowPickUpScript = GameObject.FindWithTag ("Player").GetComponent<iPickYouUp>();
	}

	void Start () {
		inf = new liblsl.StreamInfo("Test1", "Markers", 1, 0, liblsl.channel_format_t.cf_string, "giu4569");
		outl = new liblsl.StreamOutlet(inf);
		Application.runInBackground = true;
		strings = new string[] { "Test", "ABC", "123" };
		contRun1 = false;
		contRun2 = false;
		contRun3 = false;
		contRun4 = false;
	}

	void Update () 
	{
		//narrow
		if (scenario == 1)
			narrowCheck ();
		//broad
		else if (scenario == 0)
			broadCheck ();
	}

	void broadCheck()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
		{
			outl.push_sample (strings);
		}
	}

	void narrowCheck()
	{
		if (narrowPickUpScript.hasClue1 && !contRun1) {
			contRun1 = true;
			outl.push_sample (strings);
		}
		if (narrowPickUpScript.hasClue2 && !contRun2) {
			contRun2 = true;
			outl.push_sample (strings);
		}
		if (narrowPickUpScript.hasClue3 && !contRun3) {
			contRun3 = true;
			outl.push_sample (strings);
		}
		if (narrowPickUpScript.hasKey && !contRun4) {
			contRun4 = true;
			outl.push_sample (strings);
		}
	}
}
