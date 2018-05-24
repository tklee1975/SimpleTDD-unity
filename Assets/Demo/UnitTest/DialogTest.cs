using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class DialogTest : BaseTest {
	private HelloDialog mDialog = null;
	private int mTextIndex = 0;

	void Start() {
		GameObject dialogObj = GameObject.Find("HelloDialog");
		if(dialogObj != null) {
			mDialog = dialogObj.GetComponent<HelloDialog>();
		}

		Debug.Log("mDialog=" + mDialog);

		mDialog.SetOnOkayDoneCallback(OnOkayDone);
	}

	public void OnOkayDone(HelloDialog dialog) {
		Debug.Log("Okay is done");
	}

	[Test]
	public void test1()
	{
		Debug.Log("###### TEST 1 ######");
	}

	[Test]
	public void SetMessage()
	{
		string[] textArray = {
			"Testing 1", 
			"Testing 2", 
			"Testing 3", 
		};

		string textStr = textArray[mTextIndex % textArray.Length];
		mTextIndex++;

		mDialog.SetMessage(textStr);
	}

	[Test]
	public void ShowDialog()
	{
		mDialog.ShowDialog();
	}

	[Test]
	public void CloseDialog()
	{
		mDialog.CloseDialog();
	}
}
