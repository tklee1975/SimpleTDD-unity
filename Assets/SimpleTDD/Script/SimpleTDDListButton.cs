using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleTDDListButton : MonoBehaviour {
	private string mTestName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string GetTestName()
	{
		return mTestName;
	}

	public void SetTest(string name)
	{
		mTestName = name;

		Text text = GetComponentInChildren<Text>();
		text.text = name;
	}

	public void RunTest()
	{
		Debug.Log("RunTest: name=" + mTestName);
		SceneManager.LoadScene(mTestName);
	}
}
