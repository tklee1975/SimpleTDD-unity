using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTest : SimpleTDD.BaseTest {

	protected override void SetupTest(List<string> testList)
	{
		testList.Add("testScreenData");
	}

	public void testScreenData()
	{
		Debug.Log("###### TEST Screen Data ######");

		print("currentRes=" + Screen.currentResolution);
		print("isFullScreen=" + Screen.fullScreen);

		// the player width x height
		print("width=" + Screen.width + " height=" + Screen.height);
		print("orientation=" + Screen.orientation);
				// ScreenOrientation.Portrait, ScreenOrientation.Landscape

		// Resolution Array
//		Resolution[] resolutions = Screen.resolutions;
//		foreach (Resolution res in resolutions) {
//			print("res> " + res.width + "x" + res.height);
//		}
	}

}
