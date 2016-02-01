using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelDis : MonoBehaviour {
	public GameObject level;
	public Text k;
	// Use this for initialization
	void Start () {
		k = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int a = level.GetComponent<Cam> ().getLvl ();
		k.text = "Level: " + a;
	}
}
