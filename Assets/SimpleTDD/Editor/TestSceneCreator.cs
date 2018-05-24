using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;


// Reference
//  2DGameKit Source: /Utilities/Editor/NewSceneCreator.cs
//

namespace SimpleTDD 
{
	public class TestSceneCreator : EditorWindow {
		const string TEST_TEMPLATE = "TDDTest";

		protected string mNewSceneName = "NewTest";
		protected string mTestFolder = "Test";

        protected readonly GUIContent mNameContent = new GUIContent ("New Scene Name");
		protected readonly GUIContent mFolderContent = new GUIContent ("Test Folder Path");
    
        [MenuItem("Window/SimpleTDD/Create New Test Scene...", priority = 100)]
        static void Init ()
        {
            TestSceneCreator window = GetWindow<TestSceneCreator> ();
            window.Show();
        }

        void OnGUI ()
        {
            mNewSceneName = EditorGUILayout.TextField (mNameContent, mNewSceneName);

			mTestFolder = EditorGUILayout.TextField (mFolderContent, mTestFolder);
        
            if(GUILayout.Button ("Create")) {
                CheckAndCreateScene ();
			}
        }

		bool AskForActiveSceneSave() {		// return false: cancel action,  true: continue
			Scene currentActiveScene = SceneManager.GetActiveScene ();

            if (currentActiveScene.isDirty == false)
            {
				return true;	// can continue;
			}


        	string title = currentActiveScene.name + " Has Been Modified";
            string message = "Do you want to save the changes you made to " + currentActiveScene.path 
							+ "?\nChanges will be lost if you don't save them.";
            int option = EditorUtility.DisplayDialogComplex (title, message, "Save", "Don't Save", "Cancel");
			if(option == 2) {	// Cancel 
				return false;	
			}

			if (option == 0)		// Save 
            {
               EditorSceneManager.SaveScene (currentActiveScene);
            }

			return true;
		}

		bool CheckSceneName() {
			string[] result = AssetDatabase.FindAssets("name:" + mNewSceneName);

			if(result.Length > 0) {
				EditorUtility.DisplayDialog("Error",
                    "The scene name " + mNewSceneName + " already exist."
					+ " Try to use another name.",
                    "OK");
				return false;
			} else {
				return true;
			}
		}

		void CheckAndCreateScene() {
			Debug.Log("Create Scene. name=" + mNewSceneName);

			if (EditorApplication.isPlaying)
            {
                Debug.LogWarning ("Cannot create scenes while in play mode.  Exit play mode first.");
                return;
            }

			if(AskForActiveSceneSave() == false) {
				return;
			}

			
			CreateScene();
		}

		protected void CreateTestFolder()
    	{
			
			if(AssetDatabase.IsValidFolder("Assets/" + mTestFolder)) {
				return;
			}


        	AssetDatabase.CreateFolder("Assets", mTestFolder);
        	// string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
			// Debug.Log("DEBUG: newFolderPath=" + newFolderPath);
    	}

		protected string FindTestSceneTemplate(string[] searchResult) {
			foreach(string guid in searchResult) {
				string path = AssetDatabase.GUIDToAssetPath(guid);
				Debug.Log("path=" + path);
				if(path.EndsWith(TEST_TEMPLATE + ".unity")) { 
					return path;
				}
			}

			return "";
		}

		 static void CopySceneAndOpen(string sourceScene, string targetScene)
     	{
        	 FileUtil.CopyFileOrDirectory(sourceScene, targetScene);
			 Scene newScene = EditorSceneManager.OpenScene(targetScene, OpenSceneMode.Single);
     	}

		protected void CreateScene ()
        {
			string[] result = AssetDatabase.FindAssets(TEST_TEMPLATE);

			// Check for the template scene
			
			string templateScenePath = FindTestSceneTemplate(result);	// 
			Debug.Log("templateScenePath=" + templateScenePath);
			if(templateScenePath == "") {
				EditorUtility.DisplayDialog("Error",
                    "Template Test Scene " + TEST_TEMPLATE + " was not found in SimpleTDD Folder. "
					+ " This scene is required to create a new test.",
                    "OK");
				return;
			}

			if(CheckSceneName() == false) {
				return;
			}


			CreateTestFolder();

			
			string newScenePath = "Assets/" + mTestFolder + "/" + mNewSceneName + ".unity";
			//CopySceneAndOpen(templateScenePath, newScenePath);
			

			AssetDatabase.CopyAsset(templateScenePath, newScenePath);
            AssetDatabase.Refresh();
			Scene newScene = EditorSceneManager.OpenScene(newScenePath, OpenSceneMode.Single);

			SetupTest();

			Close();		// Close the window
		}

		void SetupTest() {
			CreateNewTest.SetupTest();
		}
	}
	
}
