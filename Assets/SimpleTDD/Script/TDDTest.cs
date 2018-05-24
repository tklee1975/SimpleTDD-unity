using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

namespace SimpleTDD
{

    public class TDDTest : MonoBehaviour
    {
        //public SimpleTDD.BaseTest testCase = null;

        public SimpleTDD.SubtestButton subtestButtonPrefab;

        public Transform contentPanel;
        public float referenceWidth = 320;
        public float buttonWidth = 80;
        public float buttonHeight = 30;  // 40% of Width
        public float buttonSpacing = 5;

		public bool printDebugLog = true;

        public Dictionary<string, BaseTest> mTestDictionary = new Dictionary<string, BaseTest>();


        private Transform mLogPanelTF;
        private Text mLogText;

        void Awake()
        {
            // setup the Log Panel 
            // Logging Panel and Text
            mLogPanelTF = transform.Find("LogPanel");
            Debug.Log("Awake: mLogPanel=" + mLogPanelTF);
            if (mLogPanelTF != null)
            {
                mLogText = mLogPanelTF.Find("LogText").GetComponent<Text>();
            }
            else
            {
                mLogText = null;
            }
            HideScreenLog();

            // button size
            ObtainButtonSize();
        }

        protected void ObtainButtonSize()
        {
            if (subtestButtonPrefab == null)
            {
                return;
            }

            RectTransform rectTransform = subtestButtonPrefab.GetComponent<RectTransform>();

            buttonWidth = rectTransform.sizeDelta.x;
            buttonHeight = rectTransform.sizeDelta.y;

        }

        private bool AddTestEntity(BaseTest testClass, string testName)
        {
            if (mTestDictionary.ContainsKey(testName))
            {
                Debug.LogError("TDDTest: adding test already exists: testName=" + testName);
                return false;   //
            }

            mTestDictionary.Add(testName, testClass);

            return true;
        }

        void SetupTestCase()
        {
            BaseTest[] testClassList = GameObject.FindObjectsOfType<BaseTest>();

            List<string> allTests = new List<string>();

            // Register the test case
            foreach (BaseTest testClass in testClassList)
            {

                if (testClass == null)
                {
                    Debug.Log("SetupTest: testClass is null");
                    continue;
                }

                List<string> testList = testClass.GetSubTestList();
                foreach (string testName in testList)
                {
                    if (AddTestEntity(testClass, testName))
                    {
                        allTests.Add(testName);
                    }
                }
            }

            // 
            CreateSubtestButton(allTests);
        }


        // Use this for initialization
        void Start()
        {
            CanvasScaler scaler = GameObject.FindObjectOfType<CanvasScaler>();
            //CanvasScaler scaler = GameObject.Find("TDDMenu").GetComponent<CanvasScaler>();
            referenceWidth = scaler == null ? 400 : scaler.referenceResolution.x;

            // Setup the Test 
            //Invoke("SetupTestCase", 1);
            SetupTestCase();
        }

        private void AddTestButton(string testName, Vector2 position)
        {
            SubtestButton button = Instantiate(subtestButtonPrefab);
            button.transform.SetParent(contentPanel, false);
            button.SetTest(testName);

            button.testOwner = this;
            Debug.Log("DEBUG: AddTestButton: position=" + position);

            RectTransform rt = button.gameObject.GetComponent<Transform>() as RectTransform;
            rt.anchoredPosition = position;

            //UIHelper.SetUIObjectTopLeftPostion(button.gameObject, position);
        }

        private float GetContentWidth()
        {
            if (contentPanel == null)
            {
                return 400;
            }

            RectTransform rectTrans = contentPanel as RectTransform;
            return rectTrans.rect.size.x;
        }

        private void UpdateScrollViewHeight(float height)
        {
            RectTransform rectTrans = contentPanel as RectTransform;
            Vector2 size = rectTrans.sizeDelta;
            Debug.Log("UpdateScrollViewHeight: sizeDelta: size=" + size);

            size.y = height;
            rectTrans.sizeDelta = size;
        }

        private void CreateSubtestButton(List<string> testList)
        {
            Vector2 position = new Vector3(0, -buttonSpacing);      // Top Left corner

            float width = GetContentWidth();
            float spacing = buttonSpacing;
            float usableWidth = width - 20;     // 20 is the scroll bar width 


            //		float contentWidth = referenceWidth - 20;		// TODO
            //
            //		float rightBound = contentWidth - spacing - 10;
            //
            //			Debug.Log("referenceWidth=" + referenceWidth + " width=" + width);
            //
            //			// Add Back
            //			SubtestButton backButton = Instantiate(subtestButtonPrefab, 
            //				Vector3.zero, Quaternion.identity);
            //				
            //			backButton.transform.SetParent(contentPanel, false);
            //			UIHelper.SetUIObjectTopLeftPostion(backButton.gameObject, position);
            //			backButton.SetTest("back");
            //			backButton.isBackButton = true;
            //			position.x += buttonWidth + spacing;
            //

            float scrollHeight = spacing + buttonHeight;

            foreach (string test in testList)
            {
                AddTestButton(test, position);

                //				SubtestButton button = Instantiate(subtestButtonPrefab, 
                //						Vector3.zero, Quaternion.identity);
                //				button.transform.SetParent(contentPanel, false);
                //				button.SetTest(test);


                //				UIHelper.SetUIObjectTopLeftPostion(button.gameObject, position);
                //
                //				button.testOwner = this;
                //
                position.x += buttonWidth + spacing;

                //Debug.Log("DEBUG: position.x=" + position.x);

                if ((position.x + buttonWidth) >= usableWidth)
                {
                    position.x = 0;
                    position.y -= (buttonHeight + spacing);

                    scrollHeight += buttonHeight + spacing;
                }
            }

            UpdateScrollViewHeight(scrollHeight);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RunTest(string testName)
        {
            if (mTestDictionary.ContainsKey(testName) == false)
            {
                Debug.Log("RunTest: [" + testName + "] not registered");
                return;
            }

            BaseTest testClass = mTestDictionary[testName];

            //Debug.Log("Testing " + testName);
            testClass.RunTest(testName);
        }

        #region Logging Message
        public void ShowScreenLog()
        {
            if (mLogPanelTF == null)
            {
                return;
            }
            mLogPanelTF.gameObject.SetActive(true);
        }

        public void HideScreenLog()
        {
            if (mLogPanelTF == null)
            {
                return;
            }
            mLogPanelTF.gameObject.SetActive(false);
        }

        public void UpdateLog(string message)
        {
            if (mLogText == null)
            {
                return;
            }

            mLogText.text = message;

			if(printDebugLog) {
				Debug.Log(message);
			}
        }

        public void AppendLog(string message)
        {
            if (mLogText == null)
            {
                return;
            }

            string oldMessage = mLogText.text;

            string newMessage = oldMessage + "\n" + message;

            mLogText.text = newMessage;

			if(printDebugLog) {
				Debug.Log(message);
			}
        }
        #endregion
    }

}