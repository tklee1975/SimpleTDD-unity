using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class DemoTest4 : BaseTest {

	protected override void DidRunTest(string testName)
	{
		Debug.Log("After running " + testName);
	}

	protected override void WillRunTest(string testName)
	{
		Debug.Log("Before running " + testName);
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
