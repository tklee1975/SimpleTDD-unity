using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class SpriteTest : SimpleTDD.BaseTest {
	private GameObject mStar;
	private GameObject mAnimeStar;

	public void Start()
	{
		mStar = GameObject.Find("PlaySpace/TestObject");
		mAnimeStar = GameObject.Find("PlaySpace/AnimateObject");
	}

	[Test]
	public void test1()
	{
		Debug.Log("###### TEST 1 ######");
	}

	[Test]
	public void test2()
	{
		Debug.Log("Testing 2");
	}

	[Test]
	public void testToggleStar()
	{
		bool isActive = mStar.activeSelf;
		Debug.Log("active1=" + mStar.activeSelf + " active2=" + mStar.activeInHierarchy);
		mStar.SetActive(isActive == false);	// toggle the active flag
	}

	[Test]
	public void moveLeft()
	{
		mStar.transform.Translate(new Vector3(-0.1f, 0, 0));
	}

	[Test]
	public void moveRight()
	{
		mStar.transform.Translate(new Vector3(0.1f, 0, 0));
	}

	[Test]
	public void DoFadeIn()
	{
		Animator animator = mAnimeStar.GetComponent<Animator>();
		animator.Play("FadeIn");
	}

	[Test]
	public void DoMoveIn()
	{
		Animator animator = mAnimeStar.GetComponent<Animator>();
		animator.Play("MoveIn");
	}
}
