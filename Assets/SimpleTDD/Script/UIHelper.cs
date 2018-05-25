using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleTDD
{
    public class UIHelper
    {
        public static void SetUIObjectTopLeftPostion(GameObject obj, Vector2 position)
        {
            Vector3 pos3 = Vector3.zero;
            pos3.x = position.x;
            pos3.y = position.y;

            // 	
            RectTransform rectTrans = obj.GetComponent<RectTransform>();
            rectTrans.anchorMax = new Vector2(0, 1);
            rectTrans.anchorMin = new Vector2(0, 1);
            rectTrans.localPosition = pos3;

        }
    }

}

