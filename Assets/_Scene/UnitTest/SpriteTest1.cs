using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class SpriteTest1 : BaseTest {
	public GameObject spriteObject;

	public bool autoRotate = false;
	public float rotateSpeed = 10;

	public void RotateSprite(float angleDelta) {
		Vector3 euler = spriteObject.transform.eulerAngles;

		euler.z += angleDelta;
		spriteObject.transform.eulerAngles = euler;
	}

	void Update() {
		if(autoRotate) {
			RotateSprite (rotateSpeed * Time.deltaTime);
		}
	}

	[Test]
	public void RotateLeft()
	{
		RotateSprite (rotateSpeed);
	}

	[Test]
	public void RotateRight()
	{
		RotateSprite (-rotateSpeed);
	}

	[Test]
	public void AutoRotate()
	{
		//Debug.Log("###### TEST 1 ######");
		autoRotate = ! autoRotate;
	}

	[Test]
	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}
}
