using UnityEngine;
using System.Collections;
using GamepadInput;

/*
 * Could this code be better... yes
 * 
 */
public class PlayerCon : MonoBehaviour 
{
	//things
	private ObjectPoolScript Laser;
	public int health = 6;
	public int conNum = 1;
	public GameObject head;
	public float speed = .1f;
	public float checkDis = .125f;
	public GameObject body;
	public int gunCycle = 2;
	public int damage = 1;
	public int fireType = 1;
	public int range = 20;
	//Sprites
	public Sprite bUp;
	public Sprite bDown;
	public Sprite bLeft;
	public Sprite bRight;
	public Sprite hUp;
	public Sprite hDown;
	public Sprite hLeft;
	public Sprite hRight;
	//used for rend sprites
	private float dirHeadX = 0f;
	private float dirBodyX = 0f;
	private float dirHeadY = 0f;
	private float dirBodyY = 0f;
	//for changing values
	private bool dmgon = false;
	private int dmgCool = 0;
	private bool pir = false;
	private int moveLR = 0;
	private int moveUD = 0;
	private int fireLR = 0;
	private int fireUD = 0;
	private Vector3 attdir = Vector3.zero;
	private bool attGo = false;
	private int gunCool = 0;
	private SpriteRenderer Body;
	private SpriteRenderer Head;
	private Transform full;
	private GamepadState state;
	private Vector3 fullBodyPosition = new Vector3 (0, 0, 0);

	// Use this for initialization
	void Start () 
	{
		//set stuff to be changed
		Body = body.GetComponent<SpriteRenderer> ();
		Head = head.GetComponent<SpriteRenderer> ();
		full = GetComponent<Transform> ();
		Laser = GameObject.Find ("PoolLasers").GetComponent<ObjectPoolScript>();
	}
	
	// Update as fast as possible
	void Update () 
	{
		//Keyboard Input
		if (Input.GetKey (KeyCode.A))
			moveLR = -1;
		else if (Input.GetKey (KeyCode.D))
			moveLR = 1;
		else
			moveLR = 0;
		if (Input.GetKey (KeyCode.S))
			moveUD = -1;
		else if (Input.GetKey (KeyCode.W))
			moveUD = 1;
		else 
			moveUD = 0;
		if (Input.GetKey (KeyCode.UpArrow))
			fireUD = 1;
		else if (Input.GetKey (KeyCode.DownArrow))
			fireUD = -1;
		else 
			fireUD = 0;
		if (Input.GetKey (KeyCode.LeftArrow))
			fireLR = -1;
		else if (Input.GetKey (KeyCode.RightArrow))
			fireLR = 1;
		else 
			fireLR = 0;

		//get the state of the gamepad
		state = GamePad.GetState((GamePad.Index)conNum);
		//head direction/////////////////////////////////////////////////
		if (state.rightStickAxis[0] > 0.5f || fireLR > 0f ) 
		{
			
			attGo = true;
			attdir = Vector3.right;
			dirHeadX = 1f;
			dirHeadY = 0f;
		} else if (state.rightStickAxis[0] < -0.5f || fireLR < 0f) 
		{
			attGo = true;
			attdir = Vector3.left;
			dirHeadX = -1f;
			dirHeadY = 0f;
		} else if (state.rightStickAxis [1] > 0.5f || fireUD > 0f) 
		{
			attGo = true;
			attdir = Vector3.up;
			dirHeadY = 1f;
			dirHeadX = 0f;

		} else if (state.rightStickAxis [1] < -0.5f || fireUD < 0f) 
		{
			attGo = true;
			attdir = Vector3.down;
			dirHeadY = -1f;
			dirHeadX = 0f;
		} else 
		{
			attGo = false;
		}
		//set head sprite/////////////////////////////////////////////////
		if (dirHeadX == 1f) 
		{
			Head.sprite = hRight;
		} else if (dirHeadY == 1f) 
		{
			Head.sprite = hUp;
		} else if (dirHeadX == -1f) 
		{
			Head.sprite = hLeft;
		} else if (dirHeadY == -1f) 
		{
			Head.sprite = hDown;
		}
		//direction detirminer for body///////////////////////////////////////////
		if (state.LeftStickAxis[0] > 0.5f || moveLR > 0) 
		{
			if(dirHeadX == -1f)
				dirBodyX = -1f;
			else 
				dirBodyX = 1f;
			dirBodyY = 0;

		}
		else if (state.LeftStickAxis[0] < -0.5f || moveLR < 0) 
		{
			if(dirHeadX == 1f)
				dirBodyX = 1f;
			else 
				dirBodyX = -1f;
			dirBodyY = 0;
		}
		if (state.LeftStickAxis[1] > 0.5f || moveUD > 0) 
		{
			if(dirHeadY == -1f)
				dirBodyY = -1f;
			else 
				dirBodyY = 1f;
			dirBodyX = 0;
		}
		else if (state.LeftStickAxis[1] < -0.5f || moveUD < 0) 
		{
			if(dirHeadY == 1)
				dirBodyY = 1f;
			else 
				dirBodyY = -1f;
			dirBodyX = 0;
		}
		//set body sprite/////////////////////////////////////////////////////////
		if (dirBodyX == 1f) 
		{
			Body.sprite = bRight;
		}
		else if (dirBodyY == 1f) 
		{
			Body.sprite = bUp;
		}
		else if (dirBodyX == -1f) 
		{
			Body.sprite = bLeft;
		}
		else if (dirBodyY == -1f) 
		{
			Body.sprite = bDown;
		}
	}




	//called on an interval 
	void FixedUpdate()
	{

		state = GamePad.GetState((GamePad.Index)conNum);
		fullBodyPosition = new Vector3 ((speed * (state.LeftStickAxis [0] + moveLR)) + gameObject.GetComponent<Transform> ().position.x, (speed * (state.LeftStickAxis [1] + moveUD)) + gameObject.GetComponent<Transform> ().position.y, 0);

		//wepon///////////////////////////////////////////////////////////////////
		if (fireType == 1) {
			if (gunCool < 0 && attGo) {

				gunCool = gunCycle;
				GameObject tempLaser = GameObject.Find ("PoolLasers").GetComponent<ObjectPoolScript> ().FetchObject ();
				tempLaser.GetComponent<Transform> ().position = fullBodyPosition;
	
				tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
			}

		} else if (fireType == 2) 
		{
			if (gunCool < 0 && attGo) {

				gunCool = gunCycle;
				if(attdir.x == 1 || attdir.x == -1)
				{
					GameObject tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x ,fullBodyPosition.y+.3f,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x ,fullBodyPosition.y-.3f,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
				}
				else
				{
					GameObject tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x +.3f,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x -.3f ,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
				}

			}
		}
		else if (fireType == 3) 
		{
			if (gunCool < 0 && attGo) {

				gunCool = gunCycle;
				if(attdir.x == 1 || attdir.x == -1)
				{
					GameObject tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x ,fullBodyPosition.y+.1f,0);
					tempLaser.GetComponent<lazer> ().dir (new Vector3(attdir.x,attdir.y + .2f,0),range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x ,fullBodyPosition.y-.1f,0);
					tempLaser.GetComponent<lazer> ().dir (new Vector3(attdir.x,attdir.y - .2f,0),range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x ,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
				}
				else
				{
					GameObject tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x +.1f,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (new Vector3(attdir.x + .2f,attdir.y ,0),range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x -.1f ,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (new Vector3(attdir.x - .2f,attdir.y,0 ),range,pir);
					tempLaser = Laser.FetchObject ();
					tempLaser.GetComponent<Transform>().position = new Vector3(fullBodyPosition.x  ,fullBodyPosition.y,0);
					tempLaser.GetComponent<lazer> ().dir (attdir,range,pir);
				}
				
			}
		}

		gunCool --;
		if (dmgon){
			dmgCool --;
			if(dmgCool < 0)
			{
				dmgCool = 50;
				health --;
			}
		}
		dmgCool --;
		full.position = fullBodyPosition;
	}
	public void buffs(float speedChange, int fireRateChange, int damageChange, int firetype, int rangeI, bool per)
	{
		speed += speedChange;
		gunCycle += fireRateChange;
		damage += damageChange;
		fireType = firetype;
		range += rangeI;
		pir = per;

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") {
			dmgon = true;

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{

		if (other.tag == "Enemy") {
			dmgon = false;
			
		}
			

	}
	public int getHealth()
	{
		return health;
	}
	public int dmg()
	{
		return damage;
	}
}
