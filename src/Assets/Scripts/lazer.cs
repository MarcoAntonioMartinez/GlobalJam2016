using System;
using UnityEngine;
using System.Collections;

public class lazer : MonoBehaviour 
{
	private GameObject go;
	private bool peircing = false;
	public float speed = .2f;
	private Vector3 direction = Vector3.left;
	private Transform full;
	public int life = 20;
	private int lifed = 0;
	private BoxCollider2D col;
	// Use this for initialization
	void Start () {
		go = GetComponent<GameObject> ();
		full = GetComponent<Transform> ();
		col = GetComponent<BoxCollider2D> ();
	}
	public void dir(Vector3 a,int range,bool per )
	{
		peircing = per;
		life = range;
		direction = a;
		lifed = life;
		gameObject.SetActive (true);
	}
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FixedUpdate()
	{

		Vector3 dir = new Vector3 (full.position.x + (direction.x * speed),full.position.y + (direction.y * speed), full.position.z);
		full.position = dir;
		lifed--;

		if (lifed < 0)
			gameObject.SetActive (false);
	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Block" || (other.tag == "Enemy" && !peircing))
			lifed = -1;
	}

}
