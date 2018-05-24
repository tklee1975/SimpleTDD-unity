using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloDialog : MonoBehaviour {
	public delegate void OnOkayDone(HelloDialog dialog);

	// 
	private OnOkayDone mOnOkayDone = null;

	private Text mText = null;

	// Use this for initialization
	void Start () {
		mText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMessage(string message) {
		mText.text = message;
	}

	public void SetOnOkayDoneCallback(OnOkayDone _callback) {
		mOnOkayDone = _callback;
	}

	public void ShowDialog() {
		gameObject.SetActive(true);
	}

	public void CloseDialog() {
		gameObject.SetActive(false);
	}

	public void OnOkClicked() {
		if(mOnOkayDone != null) {
			mOnOkayDone(this);
		}
		
		CloseDialog();
	}
}
