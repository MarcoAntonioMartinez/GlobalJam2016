using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	public GameObject poolg ;
	//private ObjectPoolScript poolE;
	private ObjectPoolScript poolc;
	private ObjectPoolScript poolS;
	public GameObject pl;
	private GameObject obj;
	private int points = 0;
	private int lvl = 0;
	public GameObject reward;
	// Use this for initialization
	void Start () {
		//poolE = GameObject.Find ("EnemyPool").GetComponent<ObjectPoolScript> ();
		poolS = GameObject.Find ("SpawnerPool").GetComponent<ObjectPoolScript> ();
		poolc = GameObject.Find("CratePool").GetComponent<ObjectPoolScript> ();
		nextLevel ();
		reward = Instantiate (reward, Vector3.zero, Quaternion.identity) as GameObject;
		reward.GetComponent<Reward> ().ply (pl);
		reward.SetActive (false);
		Random.seed = System.DateTime.Now.Millisecond;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{


		if (points == lvl *((int)Mathf.Sqrt(lvl)+1) ) {
				poolc.clear ();
				reward.GetComponent<Reward> ().newItem ();
				points = 0;
			}
	}
	public void addPoint()
	{
		points ++;
	}

	public void nextLevel()
	{
		lvl ++;

		 if(lvl%200 !=0)
		{
			//gens spawns
			for(int i = 0; i < lvl; i ++)
			{      
				GameObject sp =poolS.FetchObject();
				sp.GetComponent<Transform>().position = new Vector3(((((Random.value * 100) % 16)-8.1f)+Random.value),((((Random.value * 100) % 8)-4f)+Random.value),0);
				sp.GetComponent<SpawnerScript>().setSpawnNumber((int)Mathf.Sqrt(lvl) ,lvl *50);
			}
			//gen boxes
			for(int i = 0; i < 10; i ++)
			{      
				poolc.FetchObject().GetComponent<Transform>().position = new Vector3(((((Random.value * 100) % 16)-8.1f)+Random.value),((((Random.value * 100) % 8)-4f)+Random.value),0);
			}
		}
		else 
		{
			//boss fight
		}
	}
	public int getLvl(){
		return lvl;
	}
}
