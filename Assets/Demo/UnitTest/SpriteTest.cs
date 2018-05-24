using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class SpriteTest : BaseTest {
	public SpriteRenderer testRenderer;
	public Sprite[] testSpriteList;


	private int mIndex = 0;

	[Test]
	public void ToggleSprite()
	{
		testRenderer.sprite = testSpriteList[mIndex];

		// Next Sprite
		mIndex++;
		if(mIndex >= testSpriteList.Length) {
			mIndex = 0;
		}
	}

	[Test]
	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}
}
