using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class TestMenuTest : BaseTest {
	void Start() {
		ShowScreenLog();
		//Debug.Log("TestMenuTest.Start");
	}

	protected override void SetupTest(List<string> testList) {
		for(int i=0; i<30; i++) {
			testList.Add("Test " + i);
		}
	}

	public void Test() {
		Debug.Log("###### TEST ######");
	}

	[Test]
	public void test1()
	{
		Debug.Log("###### TEST 1 ######");
	}

	[Test]
	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}
}
