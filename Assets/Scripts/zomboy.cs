using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class zomboy : MonoBehaviour
{
	public GameObject wooda;
	public GameObject woods;
	public GameObject stonea;
	public GameObject stones;
	public GameObject irona;
	public GameObject irons;
	public GameObject diamonda;
	public GameObject diamonds;

	private NavMeshAgent agent;
	private GameObject player;
	private bool follow = false;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;

		player = GameObject.FindWithTag("Player");
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, 
					player.transform.position) < 10)
		{
			agent.SetDestination(player.transform.position);
			follow = true;
		}
		if (agent.hasPath || agent.remainingDistance > 0.1f) return;
		else
		{
			if (follow == false) agent.SetDestination(
					(Random.insideUnitSphere * 10) +
					transform.position);
			else agent.SetDestination(player.transform.position);
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
