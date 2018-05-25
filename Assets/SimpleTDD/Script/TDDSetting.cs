using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTDD
{
    // uncomment CreateAsset if need to create this setting scriptable object
    //[CreateAssetMenu(fileName = "SimpleTDDSetting", menuName = "SimpleTDD/Setting", order = 1)]
    public class TDDSetting : ScriptableObject {
        public string defaultTestFolder = "Test";
        public string defaultTestName = "NewTest";

        private static TDDSetting sInstance = null;
        public static TDDSetting Instance
        {
            get
            {
                if (!sInstance) {
                   // sInstance = Resources.FindObjectsOfTypeAll<TDDSetting>()
                   TDDSetting[] settingResult = Resources.FindObjectsOfTypeAll<TDDSetting>();
                   if(settingResult.Length > 0) {
                       sInstance = settingResult[0];
                   }
                }
                return sInstance;
            }
        }
    }
}