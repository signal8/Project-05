using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
	public int protection = 5;
	private float timer = 5.0f;
	public bool despawning = false;

	void Awake()
	{
		Transform player = GameObject.FindWithTag("Player").transform;
		if (transform.parent == player) return;
		despawning = true;
	}

	void Update()
	{
		if (despawning == false) return;
		if (timer > 0.0f) timer -= Time.deltaTime;
		else Destroy(gameObject);
	}

	void DontDie()
	{
		DontDestroyOnLoad(gameObject);
	}
}
