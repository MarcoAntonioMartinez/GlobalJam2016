using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health = 100;
	public GameObject player;
	public float speed = .3f;
	private Cam point;
	private int off = 0;
	public Vector3 dir;
	private Vector3 enemy;
	private int rand = 30;
	private float randx = 1;
	private float randy = 1;
	// Use this for initialization
	void Start () {
		point = GameObject.Find ("Main Camera").GetComponent<Cam> ();
		player = GameObject.Find ("Player");
		enemy = GetComponent<Transform>().position;
		dir = player.GetComponent<Transform> ().position  - enemy;
		gameObject.SetActive (true);
		Random.seed = System.DateTime.Now.Millisecond;
	}
	public void setHealth(int n){
		health = n;
	}

	void FixedUpdate () {
		if (rand < 0) {
			rand = 30;
			randx = 1;
			randy = 1;
			if(Random.value < .5f)
			{
				int sign = 1;
				if(Random.value < .2f)
					sign = -1;
				randx = Random.value * sign;
				 sign = 1;
				if(Random.value < .2f)
					sign = -1;
				randy = Random.value * sign;
			}
		}
		rand --;


		off --;
		dir = player.GetComponent<Transform> ().position  - enemy;
		enemy = new Vector3 ((Mathf.Sign(dir.x) * speed* randx) + GetComponent<Transform>().position.x, (Mathf.Sign(dir.y) * speed * randy) +  GetComponent<Transform>().position.y, 0);
		GetComponent<Transform> ().position = enemy;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser") {
			health -= player.GetComponent<PlayerCon> ().dmg ();
			if (health < 0) {
				health = 100;
				point.addPoint ();
				gameObject.SetActive (false);
			}
		}

	}
}
