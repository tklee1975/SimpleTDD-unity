using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class SpriteTest : BaseTest {


	public Sprite sprite;

	[Test]
	public void SpriteInfo() {
		if (sprite == null) {
			AppendLog("Sprite is null");
			return;
		}

		string info = "Sprite texture size=" + sprite.texture.texelSize
			+ " xx=" + sprite.bounds
			+" xx=" + sprite.textureRect;

		ShowScreenLog ();
		UpdateLog (info);
	}
}
