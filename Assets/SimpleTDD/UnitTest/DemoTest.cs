using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoTest : SimpleTDD.BaseTest {

	protected override void SetupTest(List<string> testList)
	{
		testList.Add("test1");
		testList.Add("testGetType");
		testList.Add("test3");
		testList.Add("test4");
		testList.Add("test5");
		testList.Add("test6");
		testList.Add("test7");
		testList.Add("test8");
	}

	public void test1()
	{
		Debug.Log("###### TEST 1 ######");
		GameObject obj = GameObject.Find("TestObject");
		Debug.Log("obj=" + obj);
		obj.transform.Translate(new Vector3(0.1f, 0.2f, 0));
	}

	public void testGetType()
	{
		Debug.Log("###### TEST 2 ######");
		string[] testList = {
			"DemoTest",
			"DemoTest2",
			"GameObject"
		};

		foreach(string className in testList) {
			System.Type type = System.Type.GetType(className);
			Debug.Log(className + " type=" + type);
		}
	}
}