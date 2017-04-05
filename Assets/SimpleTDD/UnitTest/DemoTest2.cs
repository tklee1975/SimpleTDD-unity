using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoTest2 : SimpleTDDBaseTest {
	public GameObject spriteObject;
	public Sprite[] spriteList;

	private int counter;

	void Start()
	{
		counter = 0;
	}

	protected override void SetupTest(List<string> testList)
	{
		testList.Add("testChangeSprite");
	}

	public void testChangeSprite()
	{
		Debug.Log("testChangeSprite");
		if(spriteObject == null) {
			return;
		}
		if(spriteList.Length == 0) {
			return;
		}

		int spriteIndex = counter % spriteList.Length;

		spriteObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteIndex];

		counter++;
	}

}