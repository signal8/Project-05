using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIncrement : MonoBehaviour
{
	public int scenePos = 0;
	private int score = 0;

	void Start()
	{
		GameObject g = GameObject.Find("SceneSwitcher");
		if( g != null ) Destroy(g);
		gameObject.name = "SceneSwitcher";
		DontDestroyOnLoad(gameObject);
	}
	void IncrementScene()
	{
		scenePos++;
		SceneManager.LoadScene(scenePos);
	}
	void IncrementPointer()
	{
		scenePos++;
	}
	public void SetScore(int s)
	{
		score = s;
	}
	public int GetScore()
	{
		return score;
	}
	void BackToStart()
	{
		scenePos = -1;
		IncrementScene();
	}
}
