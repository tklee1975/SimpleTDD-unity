using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class ControlPanelTest : BaseTest {		
	public TDDControlPanel controlPanel;

	[Test]
	public void testButton()
	{
		string[] testList = new string[100];
		for(int i=0; i<testList.Length; i++) {
			testList[i] = "Test " + i;
		}  
		controlPanel.SetupButtons(testList);
	}

	[Test]
	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}


	[Test]
	public void test3()
	{
		Debug.Log("###### TEST 2 ######");
	}

	[Test]
	public void test4()
	{
		Debug.Log("###### TEST 2 ######");
	}

	[Test]
	public void test5()
	{
		Debug.Log("###### TEST 2 ######");
	}

	[Test]
	public void test6()
	{
		Debug.Log("###### TEST 2 ######");
	}

}
