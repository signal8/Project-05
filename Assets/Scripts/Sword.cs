using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public int damage = 3;
	public int speed = 3;

	private float timer = 5.0f;
	public bool despawning = false;

	void Awake()
	{
		Transform cam = GameObject.Find("Main Camera").transform;
		if (transform.parent == cam) return;
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
