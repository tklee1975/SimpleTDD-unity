using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public abstract class SimpleTDDBaseTest : MonoBehaviour {


	protected abstract void SetupTest(List<string> testList);

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
		SetupTest(result);
//		result.Add("test1");
//		result.Add("test2");
//		result.Add("test3");
//		result.Add("test4");

		return result;
	}
}
