using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Text;

// Reference
// http://answers.unity3d.com/questions/12599/editor-script-need-to-create-class-script-automati.html
// http://answers.unity3d.com/questions/1007004/generate-script-in-editor-script-and-gettype-retur.html

public class CreateNewTest {
	const string SCRIPT_NAME_LABEL = "NewSimpleTDDTestCaseScript";
	const string OBJECT_NAME_LABEL = "NewSimpleTDDTestCaseObject";
	public static bool AUTO_ADD_COMPONENT = false;


	[MenuItem("GameObject/SimpleTDD Setup")]
	static void SetupTest()
	{
		// http://answers.unity3d.com/questions/14637/get-the-currently-open-scene-name-or-file-name.html
		Scene scene = SceneManager.GetActiveScene();


		string path = Path.GetDirectoryName(scene.path);
		string unitTestPath = path + "/UnitTest";

		string objectName = scene.name;
		string className = FindValidClassName(objectName, unitTestPath);
			//"QuickTest";

		Debug.Log("FinalName=" + className);

		// 1. Create the Test Script 
		CreateTestScript(unitTestPath, className);

		// 2. Create the GameObject 
		CreateTestObject(objectName);

		// 3. Refresh and bind GameObject to Script
		AddComponent(objectName, className);
	}

	static void SetupUnitTestPath(string path)
	{
		if(File.Exists(path)) {
			return;
		}

		System.IO.Directory.CreateDirectory(path);
	}

	static string GetTestScriptFilename(string path, string className) {
		//return "Assets/UnitTest/"+className+".cs";
		Debug.Log("path=" + path);

		return path + "/" + className + ".cs";
	}

	static void AddComponent(string objectName, string className) {
		// @BMayne’s amazing suggestion, set the name for later reference.
		EditorPrefs.SetString (SCRIPT_NAME_LABEL, className);
		EditorPrefs.SetString (OBJECT_NAME_LABEL, objectName);

		// You probably don’t need to do both of these, but I’m just making sure.
		// (Experiment with the ones you might or might not need)
		Debug.Log("SimpleTDD: refreshing to attach component to gameobject. Please wait");
		AssetDatabase.Refresh (ImportAssetOptions.ForceUpdate 
			| ImportAssetOptions.ForceUncompressedImport );
	}

	static void CreateTestObject(string className)
	{
		// Remove the object before create
		GameObject go = GameObject.Find(className);
		if(go == null) {
			go = new GameObject(className);
		}
		go.transform.position = new Vector3(0,0,0);
	}

	static string FindValidClassName(string className, string path)
	{
		for(int i=0; i<10; i++) {
			string finalName = className + (i == 0 ? "" : "" + i);

			string filename = GetTestScriptFilename(path, finalName);

			if( File.Exists(filename) == false){
				return finalName;
			}
		}

		return className;
	}

	// Just Create the Script, not refresh
	static bool CreateTestScript(string path, string className)
	{
		SetupUnitTestPath(path);
		string filename = GetTestScriptFilename(path, className);

		// 
		if( File.Exists(filename) ){
			Debug.Log("SimpleTDD Setup: Test Script with name '" + className + "' already exist");
			AssetDatabase.ImportAsset (filename);
			return false;
		}

		string content = GetScriptContent(className);

		// 
		using (StreamWriter outfile = new StreamWriter(filename))
		{
			outfile.WriteLine(content);
		}

		AssetDatabase.ImportAsset (filename);

		return true;
	}




	[UnityEditor.Callbacks.DidReloadScripts]
	private static void ScriptReloaded() 
	{
		Debug.Log("SimpleTDD: ScriptReloaded!!");
		// If the key doesn’t exist, don’t bother, as we’re not generating stuff.
		if (!EditorPrefs.HasKey (SCRIPT_NAME_LABEL))
		{
			Debug.Log("SimpleTDD Setup: script name undefined");
			return;
		}

		if (!EditorPrefs.HasKey (OBJECT_NAME_LABEL))
		{
			Debug.Log("SimpleTDD Setup: object name undefined");
			return;
		}


		// If they key does exist and the object doesn’t, it’s just a left over key.
		string scriptName = EditorPrefs.GetString (SCRIPT_NAME_LABEL);
		string objectName = EditorPrefs.GetString (OBJECT_NAME_LABEL);

		AddGeneratedComponent(objectName, scriptName);

		//Delete the key because we don’t need it anymore!
		EditorPrefs.DeleteKey(SCRIPT_NAME_LABEL);
		EditorPrefs.DeleteKey(OBJECT_NAME_LABEL);

		//EditorApplication.SaveScene();
		Debug.Log("SimpleTDD: Test Setup success");
	}

	static void AddGeneratedComponent(string objectName, string typeName)
	{
		GameObject go = GameObject.Find( objectName );

		if (go == null)
		{
			Debug.Log("SimpleTDD Setup: No game object found");
			return;
		}
		// Get the new type from the reloaded assembly!
		// (It won’t work without assembly specification, because this
		//  is an editor script, so the default assembly is “Assembly-CSharp-editor”)
		Type type = Type.GetType(typeName + ",Assembly-CSharp");
		// Debug.Log("ScriptReloaded: AddGeneratedComponent=" + type);

		go.AddComponent( type );
	}


	static string GetScriptContent(string name)
	{
		string template = GetScriptTemplate();

		string content = template.Replace("##name##", name);

		return content;
	}

	static string GetScriptTemplate()
	{
		return 
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class ##name## : BaseTest {
	[Test]
	public void test1()
	{
		Debug.Log(""###### TEST 1 ######"");
	}

	[Test]
	public void test2()
	{
		Debug.Log(""###### TEST 2 ######"");
	}
}";
		
	}
}
