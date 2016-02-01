using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	private ObjectPoolScript pool;
	public int spawnNumber = 5;
	public int health = 100;
	public int delay = 60;
	private int delayCount = 10;
	// Use this for initialization
	void Start () {
		pool = GameObject.Find ("EnemyPool").GetComponent<ObjectPoolScript> ();
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate () {
		if (delayCount < 0) 
		{
			delayCount = delay;
			GameObject temp = pool.FetchObject();
			temp.GetComponent<Transform>().position = GetComponent<Transform>().position;
			temp.GetComponent<Enemy>().setHealth(health);
			temp.SetActive(true);
			spawnNumber --;
			if (spawnNumber < 0){
				spawnNumber = 5;
				gameObject.SetActive (false);
			}
		}
		delayCount --;

	}
	public void setSpawnNumber(int n,int heal)
	{
		spawnNumber = n;
		health = heal;
	}
}
