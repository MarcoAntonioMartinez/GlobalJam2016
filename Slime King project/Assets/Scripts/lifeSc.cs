using UnityEngine;
using System.Collections;

public class lifeSc : MonoBehaviour {
	public GameObject player;
	public Sprite life0;
	public Sprite life1;
	public Sprite life2;
	public Sprite life3;
	public Sprite life4;
	public Sprite life5;
	public Sprite life6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int temp = player.GetComponent<PlayerCon> ().getHealth ();
		if (temp == 6)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life6;
		else if (temp == 5)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life5;
		else if (temp == 4)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life4;
		else if (temp == 3)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life3;
		else if (temp == 2)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life2;
		else if (temp == 1)
			gameObject.GetComponent<SpriteRenderer> ().sprite = life1;
		else if (temp == 0) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = life0;
			Application.LoadLevel (2);
		}
		else
			gameObject.GetComponent<SpriteRenderer> ().sprite = life0;

	}
}
