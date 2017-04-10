using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class CollisionTest : BaseTest {
	public GameObject starPrefab;

	[Test]
	public void ClearStar()
	{
		GameObject parent = GameObject.Find("PlaySpace");

		List<GameObject> deleteList = new List<GameObject>();
		int numChild = parent.transform.childCount;
		for(int i=0; i<numChild; i++) {
			Transform t = parent.transform.GetChild(i);
			GameObject obj = t.gameObject;
			if(obj.name.StartsWith("Star") == false) {
				continue;
			}
			deleteList.Add(obj);
		}

		foreach(GameObject obj in deleteList) {
			Object.DestroyObject(obj);
		}
	}

	[Test]
	public void SpawnStar()
	{
		Vector3 spawnPos = new Vector3(0, 2, 0);
		GameObject newObj = Object.Instantiate(starPrefab) as GameObject;

		newObj.transform.position = spawnPos;

		GameObject parent = GameObject.Find("PlaySpace");
		newObj.transform.SetParent(parent.transform);
			
	//	Instantiate(
	}
}
