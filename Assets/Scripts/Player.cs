using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5.0f;
	public float rotSpeed = 90.0f;
	public float clampAngle = 45.0f;
	public int maxHP = 100;
	public int score = 0;
	public float hitTimer = 0.5f;
	public int hitDamage = 5;

	public TMP_Text scoreDisplay;
	public TMP_Text statDisplay;

	public GameObject currentSword;
	public GameObject currentArmor;

	private bool inControl = false;
	private bool takingDamage = false;
	private bool needsUpdating = true;
	private float cameraRotationX = 0.0f;
	private Transform cam = null;
	private CharacterController controller;
	private int hp = 100;
	private int strength;
	private int speed;
	private int armor;
	private Sword swordStats;
	private Armor armorStats;
	private float timer;
	private GameObject sceneSwitch;
	public GameObject mDN;
	private AudioSource swingAudio;
	private AudioSource deathAudio;

	void Start()
	{
		swingAudio = GetComponent<AudioSource>();
		deathAudio = mDN.GetComponent<AudioSource>();

		controller = gameObject.GetComponent<CharacterController>();
		cam = gameObject.GetComponentInChildren<Camera>().transform;

		swordStats = gameObject.GetComponentInChildren<Sword>();
		armorStats = gameObject.GetComponentInChildren<Armor>();

		sceneSwitch = GameObject.Find("SceneSwitcher");
	}

	void Update()
	{
		if (hp <= 0) Die();

		// MOUSE LOCKING

		if (Input.GetMouseButtonDown(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
			inControl = true;
		}
		else if (Input.GetKey(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			inControl = false;
		}

		// FPS INPUT

		controller.Move((Input.GetAxis("Horizontal") * 
				(moveSpeed * Time.deltaTime)) *
				transform.right);

		controller.Move((Input.GetAxis("Vertical") * 
				(moveSpeed * Time.deltaTime)) *
				transform.forward);

		if (inControl == false) return;

		transform.Rotate(0, Input.GetAxis("Mouse X") * 
				(rotSpeed * Time.deltaTime), 0, Space.Self);

		cameraRotationX = Mathf.Clamp(cameraRotationX - 
				(Input.GetAxis("Mouse Y") * 
				(rotSpeed * Time.deltaTime)), -clampAngle, 
				clampAngle);
		
		cam.localRotation = Quaternion.AngleAxis(cameraRotationX,
				Vector3.right);

		// ARMOR UPGRADING

		if (needsUpdating == true)
		{
			swordStats = currentSword.GetComponent<Sword>();
			armorStats = currentArmor.GetComponent<Armor>();
			strength = swordStats.damage;
			speed = swordStats.speed;
			armor = armorStats.protection;
			needsUpdating = false;
		}

		// ENEMY DESTRUCTION

		if (Input.GetMouseButtonDown(0))
		{
			swingAudio.Play();
			RaycastHit hit;
			if (Physics.Raycast(transform.position,
				cam.transform.forward, out hit, 5) &&
				hit.collider.gameObject.CompareTag("Enemy"))
			{
				hit.transform.gameObject.SendMessage("Die");
				score += 500;
				deathAudio.Play();
			}
		}

		// HUD UPDATING

		scoreDisplay.text = "Score: " + score;
		statDisplay.text = "HP: " + hp + "\n" +
				"STR: " + strength + "\n" +
				"SPD: " + speed + "\n" +
				"AMR: " + armor + "\n";
		
		if (!takingDamage) return;

		if (timer >= 0.0f) timer -= Time.deltaTime;
		else
		{
			timer = hitTimer;
			hp -= hitDamage;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Enemy")) takingDamage = true;
		else 
		{
			SceneIncrement s = sceneSwitch.GetComponent
				<SceneIncrement>();
			s.SetScore(score);
			Cursor.lockState = CursorLockMode.None;
			sceneSwitch.SendMessage("IncrementScene");
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Enemy")) takingDamage = false;
	}

	void Die()
	{
		SceneIncrement s = sceneSwitch.GetComponent<SceneIncrement>();
		s.SetScore(score);
		sceneSwitch.SendMessage("IncrementPointer");
		Cursor.lockState = CursorLockMode.None;
		sceneSwitch.SendMessage("IncrementScene");
	}
}
