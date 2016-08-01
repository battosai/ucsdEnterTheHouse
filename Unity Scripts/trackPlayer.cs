using UnityEngine;
using System.Collections;
using LSL;

/*		For narrow scenario: mark when pick up key items, look at different pages,
 * 		For broad scenario:  have player press key when they have an idea
 */ 

public class trackPlayer : MonoBehaviour {
	public int scenario;

	liblsl.StreamInfo inf;
	liblsl.StreamOutlet outl;
	string[] strings;
	int i;

	// Use this for initialization
	void Start () {
		i = 0;
		inf = new liblsl.StreamInfo("Test1", "Markers", 1, 0, liblsl.channel_format_t.cf_string, "giu4569");
		outl = new liblsl.StreamOutlet(inf);

		strings = new string[] { "Test", "ABC", "123" };
	}

	// Update is called once per frame
	void Update () 
	{
		//broad scenario check
		if (scenario == 0)
			broadCheck ();
		
	}

	void broadCheck()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift))
			outl.push_sample (strings);
	}

	//public void lslfnc()
	//{
	//	outl.push_sample (strings);
	//}
}
