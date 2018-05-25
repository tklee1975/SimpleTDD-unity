using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using SimpleTDD;

namespace SimpleTDD
{

    public abstract class BaseTest : MonoBehaviour
    {


        protected virtual void SetupTest(List<string> testList) { } // Can use to test scrolling


        //
        protected virtual void WillRunTest(string testName) { }
        protected virtual void DidRunTest(string testName) { }

        private TDDTest mMainTestLogic;

        void Awake()
        {
            //mMainTestLogic = gameObject.GetComponent<TDDTest>();
            mMainTestLogic = GameObject.FindObjectOfType<TDDTest>();
            DidAwake();
        }

	    protected virtual void DidAwake() {
		    // To be implemented
	    }

        //
        public void RunTest(string testMethodName)
        {
            System.Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(testMethodName);

            if (theMethod == null)
            {
                Debug.Log("SimpleTDDBaseTest: RunTest: no such test " + testMethodName);
                return;
            }

            WillRunTest(testMethodName);

            // Create the given Method
            theMethod.Invoke(this, null);

            DidRunTest(testMethodName);
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
                    if (null == test)
                    {
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
                    if (null == test)
                    {
                        continue;
                    }
                    //Debug.Log("Test Method: " + m.Name);
                    methodList.Add(m.Name);
                }
            }

            return methodList.ToArray();
        }

        public void ShowScreenLog()
        {
            if (mMainTestLogic != null)
            {
                mMainTestLogic.ShowScreenLog();
            }
        }

        public void HideScreenLog()
        {
            if (mMainTestLogic != null)
            {
                mMainTestLogic.HideScreenLog();
            }
        }

        public void UpdateLog(string message)
        {
            if (mMainTestLogic != null)
            {
                mMainTestLogic.UpdateLog(message);
            }
        }

        public void AppendLog(string message)
        {
            if (mMainTestLogic != null)
            {
                mMainTestLogic.AppendLog(message);
            }
        }

       	protected bool mPrintDebugLog = false;
        	public bool PrintDebugLog {
		        get {
			        return mPrintDebugLog;
		        }
		        set {
			        mPrintDebugLog = value;
		    }
	    }

    }

}
