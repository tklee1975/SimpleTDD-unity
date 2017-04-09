using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class AttributeTest : BaseTest {

	[Test]
	public void test1()
	{
		Debug.Log("###### TEST 1 ######");
	}




	// https://www.tutorialspoint.com/csharp/csharp_reflection.htm
	[Test]
	public void testAttributeList()
	{
		System.Type type = this.GetType();
		foreach (MethodInfo m in type.GetMethods())
		{
			//Debug.Log("Method detail: " + m.Name);
			foreach (System.Attribute a in m.GetCustomAttributes(true))
			{
				SimpleTDD.Test test = a as SimpleTDD.Test;
				if(null == test) {
					continue;
				}
				Debug.Log("Test Method: " + m.Name);	
			}
			
		}
	}

	[Test]
	public void testGetMethods()
	{
		string[] testMethods = GetTestMethods();

		foreach(string name in testMethods) {
			Debug.Log("TestMethod: " + name);
		}
	}

}
