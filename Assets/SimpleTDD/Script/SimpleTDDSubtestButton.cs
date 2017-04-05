using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleTDDSubtestButton : MonoBehaviour {
	public SimpleTDDTest testOwner = null;
	public bool isBackButton = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTest(string name)
	{
		Text text = GetComponentInChildren<Text>();
		text.text = name;
	}

	private string GetLabelName()
	{
		Text text = GetComponentInChildren<Text>();
		return text.text;
	}

	private void BackToMain()
	{
		SceneManager.LoadScene(SimpleTDDConst.MAIN_SCENE_NAME);
	}


	public void RunTest()
	{
		if(isBackButton) {
			BackToMain();
			return;
		}
		string testName = GetLabelName();

		Debug.Log("testName=" + testName);
		if(testOwner != null) {
			testOwner.RunTest(testName);
		}
	}
}
