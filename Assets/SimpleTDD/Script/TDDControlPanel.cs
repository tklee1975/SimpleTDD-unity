using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Make the Buttons fill the Given Panel
 */
namespace SimpleTDD
{
	public class TDDControlPanel : MonoBehaviour {
		public ScrollRect scrollView;
		public GameObject buttonPrefab;
		public Vector2 buttonSize = new Vector2(100, 50);
		public Vector2 margin = new Vector2(10, -20);	// also initial position
		public Vector2 spacing = new Vector2(5, 5);
		public TDDTest testOwner = null;


		protected RectTransform mContentRectTF;

		protected bool mSetupSuccess;
		protected int mCol = 3;
		// protected float mButtonWidth = 100;
		// protected float mButtonHeight = 50;

		protected float mViewWidth = 100;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Start()
		{
			CheckSetup();
			ResetContentView();
			UpdateViewWidth();
		}

		void ResetContentView() {
			mContentRectTF.anchoredPosition = Vector2.zero;
		}

		void UpdateViewWidth() {
			RectTransform scrollRT = scrollView.transform as RectTransform;
			//Debug.Log("ContentRectTF=" + mContentRectTF + " rect=" + mContentRectTF.rect);
			//Debug.Log("scrollView=" + scrollRT + " rect=" + scrollRT.rect);

			mViewWidth = mContentRectTF.rect.width;
			if(mViewWidth <= 0) {
				mViewWidth = scrollRT.rect.width;
			}
			if(mViewWidth < 100) {
				mViewWidth = 100;
			}
		}

		void CheckSetup() {
			mSetupSuccess = true;


			mContentRectTF = null;
			if(scrollView == null) {
				Debug.LogWarning("SimpleTDD.controlPanel: scrollView undefined");
				mSetupSuccess = false;
			} else {
				mContentRectTF = scrollView.transform.Find("Viewport/Content") as RectTransform;
			}

			if(mContentRectTF == null) {
				Debug.LogWarning("SimpleTDD.controlPanel: Content RectTransform undefined");
				mSetupSuccess = false;
			}


			if(buttonPrefab == null) {
				Debug.LogWarning("SimpleTDD.controlPanel: buttonPrefab undefined");
				mSetupSuccess = false;
			}

		}

		protected Vector2 GetNextPosition(Vector2 position) {
			Vector2 newPos = position;
			newPos.x += buttonSize.x + spacing.x;

			if(newPos.x >= (mViewWidth - buttonSize.x)) {
				newPos.x = margin.x;
				newPos.y -= (buttonSize.y + spacing.y);
			}

			return newPos;
		}

		public void SetupButtons(string[] testList) {
			UpdateViewWidth();
			ResetContentView();

			Vector2 position = margin;
			foreach(string name in testList) {
				AddTestButton(name, position);
				position = GetNextPosition(position);
			}

			UpdateScrollViewHeight(-position.y + buttonSize.y + spacing.y);
			//scrollView.set
		}

		 private void UpdateScrollViewHeight(float height)
        {
            Vector2 size = mContentRectTF.sizeDelta;
            Debug.Log("UpdateScrollViewHeight: sizeDelta: size=" + size);

            size.y = height;
            mContentRectTF.sizeDelta = size;
        }


		private void AddTestButton(string testName, Vector2 position)
        {
			GameObject newButton = Instantiate(buttonPrefab);

			newButton.transform.SetParent(mContentRectTF, false);

			RectTransform rt = newButton.gameObject.GetComponent<Transform>() as RectTransform;
			rt.sizeDelta = buttonSize;
			rt.anchoredPosition = position;

			SubtestButton button = newButton.GetComponent<SubtestButton>();
			if(button != null) {
				button.testOwner = this.testOwner;
				button.SetTest(testName);
			}

			Debug.Log("AddTestButton: testName=" + testName + " position=" + position);


            // SubtestButton button = Instantiate<SubtestButton>(buttonPrefab);
            // button.transform.SetParent(contentPanel, false);
            // button.SetTest(testName);

            // button.testOwner = this;
            // Debug.Log("DEBUG: AddTestButton: position=" + position);

            // RectTransform rt = button.gameObject.GetComponent<Transform>() as RectTransform;
            // rt.anchoredPosition = position;

            //UIHelper.SetUIObjectTopLeftPostion(button.gameObject, position);
        }

		// Update is called once per frame
		void Update () {

		}
	}
}
