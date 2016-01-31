using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour {

	private PlayerCon player;
	public GameObject item;
	public bool per = false;
	public int removeDelay = 100;
	public int rew = -1;
	private int fireType = 1;
	private float hover = 0;

	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;


	}
	public void ply(GameObject pl){
		player = pl.GetComponent<PlayerCon>();

	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 temp = item.GetComponent<Transform> ().position;
		item.GetComponent<Transform> ().position = new Vector3 (temp.x,temp.y + (Mathf.Sin(hover)*.01f),0);
		hover += .1f;



	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			GameObject.Find ("Main Camera").GetComponent<Cam> ().nextLevel ();
			gameObject.SetActive (false);
		}
	}
	public void newItem()
	{
		gameObject.SetActive (true);
		removeDelay = 100;
		int win = (int)((Random.value * 100) % 7);
		Debug.Log (win);
		if (win == 0) {

		} else if (win == 1) {
			//peircing shots
			per = true;
			player.buffs(0,-2,0,fireType,0,per);
			item.GetComponent<SpriteRenderer>().color = Color.grey;
		}else if (win == 2) {
			//att ++

				
				fireType  ++;
				if (fireType >= 3)
				fireType = 3;
			player.buffs(0,0,0,fireType,0,per);
				item.GetComponent<SpriteRenderer>().color = Color.red;

		}else if (win == 3) {
			//fireRate
			player.buffs(0,-2,0,fireType,0,per);
			item.GetComponent<SpriteRenderer>().color = Color.blue;
		}else if (win == 4) {
			//damage
			player.buffs(0,0,5,fireType,0,per);
			item.GetComponent<SpriteRenderer>().color = Color.magenta;
		}else if (win == 5) {
			//range
			player.buffs(0,0,0,fireType,20,per);
			item.GetComponent<SpriteRenderer>().color = Color.green;
		}else if (win == 6) {
			//walkSpeed
			player.buffs(.1f,0,5,fireType,0,per);
			item.GetComponent<SpriteRenderer>().color = Color.yellow;
		}

	}
}
