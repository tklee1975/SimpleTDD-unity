using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoTest : SimpleTDDBaseTest {

	protected override void SetupTest(List<string> testList)
	{
		testList.Add("test1");
		testList.Add("test2");
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

	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}
}