using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using SimpleTDD;

namespace SimpleTDD { 

public abstract class BaseTest : MonoBehaviour {


	protected virtual void SetupTest(List<string> testList) {} // old style to add test methods

	public void RunTest(string testMethodName)
	{
		System.Type thisType = this.GetType();
		MethodInfo theMethod = thisType.GetMethod(testMethodName);

		if(theMethod == null) {
			Debug.Log("SimpleTDDBaseTest: RunTest: no such test " + testMethodName);
			return;
		}

		theMethod.Invoke(this, null);
	}

	public List<string> GetSubTestList()
	{
		// TODO

		List<string> result = new List<string>();

		SetupTestsWithAttribute(result);

		// Extra Testing (Old Fashion)
		SetupTest(result);
//		result.Add("test1");
//		result.Add("test2");
//		result.Add("test3");
//		result.Add("test4");

		return result;
	}

	private void SetupTestsWithAttribute(List<string> result)
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
				//Debug.Log("Test Method: " + m.Name);	
				result.Add(m.Name);
			}

		}
	}

	public string[] GetTestMethods()
	{
		List<string> methodList = new List<string>();

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
				//Debug.Log("Test Method: " + m.Name);	
				methodList.Add(m.Name);
			}

		}

		return methodList.ToArray();
	}
}

}