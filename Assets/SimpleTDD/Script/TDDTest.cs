using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

namespace SimpleTDD {

public class TDDTest : MonoBehaviour {
	public SimpleTDD.BaseTest testCase = null;
	public SimpleTDD.SubtestButton subtestButtonPrefab;
	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		//testCase = GetComponent<DemoTest3>();
			testCase = FindObjectOfType<BaseTest>();
			Debug.Log("TestCase object=" + testCase);

		if(testCase == null) {
			Debug.LogError("SimpleTDDTest: testCase undefined.\n"
				+ "Please create using Assets/Create/Simple TDD Script");
			return;
		}
		List<string> testList = testCase.GetSubTestList();
		CreateSubtestButton(testList);
//		foreach(string test in testList) {
//			Debug.Log("test=" + test);
//		}
	}

	private void CreateSubtestButton(List<string> testList)
	{
		Vector2 position = new Vector3(5, -5);		// Top Left corner

		float buttonWidth = 80;
		float buttonHeight = 30;
		float contentWidth = Screen.width;		// TODO
		float spacing = 5;
		float rightBound = contentWidth - spacing - 10;

		// Add Back
		SubtestButton backButton = Instantiate(subtestButtonPrefab, 
			Vector3.zero, Quaternion.identity);
		backButton.transform.SetParent(contentPanel);
		UIHelper.SetUIObjectTopLeftPostion(backButton.gameObject, position);
		backButton.SetTest("back");
		backButton.isBackButton = true;
		position.x += buttonWidth + spacing;

			Debug.Log("DEBUG: ScreenWidth=" +contentWidth);
		// Add custom test

		foreach(string test in testList) {

				SubtestButton button = Instantiate(subtestButtonPrefab, 
					Vector3.zero, Quaternion.identity);
			button.transform.SetParent(contentPanel);
			button.SetTest(test);


			UIHelper.SetUIObjectTopLeftPostion(button.gameObject, position);

			button.testOwner = this;

			position.x += buttonWidth+spacing;

			Debug.Log("DEBUG: position.x=" + position.x);

			if((position.x + buttonWidth) >= rightBound) {
				position.x = 5;
				position.y -= (buttonHeight + spacing);
			}

//
//			RectTransform rectTrans = button.GetComponent<RectTransform>();
//			rectTrans.anchorMax = new Vector2(0, 1);
//			rectTrans.anchorMin = new Vector2(0, 1);
//			rectTrans.localPosition = position;
//			button.GetComponent<RectTransform>().localPosition = position;
//
//			button.SetTest(test);
//
//			position.y -= 50;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RunTest(string testName)
	{
		Debug.Log("Testing " + testName);
		testCase.RunTest(testName);
	}
}

}