﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTDDTest : MonoBehaviour {
	public SimpleTDDBaseTest testCase = null;
	public SimpleTDDSubtestButton subtestButtonPrefab;
	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		if(testCase == null) {
			Debug.LogError("SimpleTDDTest: testCase undefined");
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
		float contentWidth = Screen.width;		// TODO
		float spacing = 5;

		// Add Back
		SimpleTDDSubtestButton backButton = Instantiate(subtestButtonPrefab, 
			Vector3.zero, Quaternion.identity);
		backButton.transform.SetParent(contentPanel);
		SimpleTDDHelper.SetUIObjectTopLeftPostion(backButton.gameObject, position);
		backButton.SetTest("back");
		backButton.isBackButton = true;
		position.x += 90;

		// Add custom test

		foreach(string test in testList) {

				SimpleTDDSubtestButton button = Instantiate(subtestButtonPrefab, 
					Vector3.zero, Quaternion.identity);
			button.transform.SetParent(contentPanel);
			button.SetTest(test);


			SimpleTDDHelper.SetUIObjectTopLeftPostion(button.gameObject, position);

			button.testOwner = this;

			position.x += buttonWidth+spacing;

			if((position.x + buttonWidth) >= contentWidth) {
				position.x = 5;
				position.y -= 30;
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