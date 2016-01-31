using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour {
	public int gotoSceen = 0;
	// Use this for initialization
	void Start () {
		GetComponent<Transform>().position = new Vector3(9f,0f,0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GetComponent<Transform> ().position.x > -9f)
		GetComponent<Transform> ().position = new Vector3 (GetComponent<Transform> ().position.x - .1f, GetComponent<Transform> ().position.y, 0);
		if (Input.anyKeyDown)
			Application.LoadLevel (gotoSceen);
	}
}
