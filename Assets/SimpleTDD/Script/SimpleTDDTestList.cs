using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTDDTestList : MonoBehaviour {
	public string[] testSceneList;
	public SimpleTDDListButton buttonPrefab;
	public Transform contentPanel;
	private ScrollRect mScrollRect;

	private List<string> mTestList = new List<string>();
	//private string[] mTestList;

	// Use this for initialization
	void Start () {
		foreach(string scene in testSceneList) {
			mTestList.Add(scene);
		}
//		mTestList.Add("DemoTest1");
//		mTestList.Add("SimpleTest2");
//		mTestList.Add("SimpleTest3");

		mScrollRect = GetComponentInChildren<ScrollRect>();


		SetupTestList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<string> GetTestList()
	{
		return mTestList;
	}

	void SetupTestList()
	{
		float marginX = 10;
		Vector3 position = new Vector3(marginX, -10, 0);
		Vector2 buttonSize = new Vector2(90, 30);
		float screenWidth = Screen.width;
		float leftBound = screenWidth - 30;
		float spacing = 5;
		float scrollViewHeight = Screen.height - 50;

		foreach(string test in mTestList) {
			// Debug.Log("test: " + test);

			SimpleTDDListButton button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
			button.transform.SetParent(contentPanel);

			SimpleTDDHelper.SetUIObjectTopLeftPostion(button.gameObject, position);

//			RectTransform rectTrans = button.GetComponent<RectTransform>();
//			rectTrans.anchorMax = new Vector2(0, 1);
//			rectTrans.anchorMin = new Vector2(0, 1);
//			rectTrans.localPosition = position;

			button.SetTest(test);

			// Calculate next position 
			position.x += buttonSize.x + spacing;

			if((position.x + buttonSize.x) > leftBound) {
				position.x = marginX;
				position.y -= (buttonSize.y + spacing);
			}

			//if(r

			//position.y -= 50;
			//button.transform.parent = 
		}

		// 

		float totalHeight = -position.y;
		Debug.Log("TotalHeight=" + totalHeight);


		// 
		if(totalHeight < scrollViewHeight) {
			RectTransform rectTrans = contentPanel.gameObject.GetComponent<RectTransform>();
			Vector2 size = rectTrans.sizeDelta;
			size.y = totalHeight;
			rectTrans.sizeDelta = size;
			//rectTrans.
			//contentPanel.

			// TODO: Dynamic update the scroll bar

			//mScrollRect.Rebuild(CanvasUpdate.Layout);



		}
	}
}
