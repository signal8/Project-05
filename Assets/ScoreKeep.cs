using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeep : MonoBehaviour
{
	public TMP_Text scoreDisplay;
	private GameObject sceneSwitch;
	private int score;

	void Start()
	{
		sceneSwitch = GameObject.Find("SceneSwitcher");

		GetScore();
		if (scoreDisplay != null) scoreDisplay.text = score.ToString();
	}

	int GetScore()
	{
		SceneIncrement s = sceneSwitch.GetComponent<SceneIncrement>();
		score = s.GetScore();
		return score;
	}
	
	void BackToStart()
	{
		sceneSwitch.SendMessage("BackToStart");
	}
}
