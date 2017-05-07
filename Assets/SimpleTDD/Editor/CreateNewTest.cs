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
	const string NAME_CLASS_LABEL = "NewSimpleTDDTestCase";
	public static bool AUTO_ADD_COMPONENT = false;


	[MenuItem("GameObject/SimpleTDD Setup")]
	static void SetupTest()
	{
		// http://answers.unity3d.com/questions/14637/get-the-currently-open-scene-name-or-file-name.html
		Scene scene = SceneManager.GetActiveScene();

		string className = scene.name;
		string path = Path.GetDirectoryName(scene.path);
		string unitTestPath = path + "/UnitTest";
			//"QuickTest";

		// 1. Create the Test Script 
		CreateTestScript(unitTestPath, className);

		// 2. Create the GameObject 
		CreateTestObject(className);

		// 3. Refresh and bind GameObject to Script
		AddComponent(className);
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

	static void AddComponent(string className) {
		// @BMayne’s amazing suggestion, set the name for later reference.
		EditorPrefs.SetString (NAME_CLASS_LABEL, className);

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
		if (!EditorPrefs.HasKey (NAME_CLASS_LABEL))
		{
			Debug.Log("SimpleTDD Setup: No key found");
			return;
		}

		// If they key does exist and the object doesn’t, it’s just a left over key.
		string name = EditorPrefs.GetString (NAME_CLASS_LABEL);
		AddGeneratedComponent(name, name);

		//Delete the key because we don’t need it anymore!
		EditorPrefs.DeleteKey(NAME_CLASS_LABEL);

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
