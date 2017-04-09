using UnityEditor;
using UnityEngine;
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

	[MenuItem("Assets/Create/SimpleTDD Test Script")]
	static void Create()
	{
		GameObject selected = Selection.activeObject as GameObject;
		if (selected == null )
		{
			Debug.Log("Selected object not Valid");
			return;
		}
		Debug.Log("Selected object=" + selected.name);

		// Filename
		string className = selected.name.Replace(" ","");
	
		string filename = "Assets/UnitTest/"+className+".cs";

		// 
		if( File.Exists(filename) ){
			Debug.Log("Test Script already exists");
//			Type classType = SimpleTDDEditorHelper.GetType("UnitTest/" + className);
//			Debug.Log("classType=" + classType + " className=" + className);
//			selected.AddComponent(classType);
			return;
		}

		string content = GetScriptContent(className);

		// 
		using (StreamWriter outfile = new StreamWriter(filename))
		{
			outfile.WriteLine(content);
		}


		ImportAsset(className, filename);	
	}

	static void ImportAsset(string className, string scriptPath)
	{
		// @BMayne’s amazing suggestion, set the name for later reference.
		EditorPrefs.SetString (NAME_CLASS_LABEL, className);

		// You probably don’t need to do both of these, but I’m just making sure.
		// (Experiment with the ones you might or might not need)
		AssetDatabase.ImportAsset (scriptPath);
		if(AUTO_ADD_COMPONENT) {
			AssetDatabase.Refresh (ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceUncompressedImport );
		}
	}


	[UnityEditor.Callbacks.DidReloadScripts]
	private static void ScriptReloaded() 
	{
		Debug.Log("ScriptReloaded!!");
		// If the key doesn’t exist, don’t bother, as we’re not generating stuff.
		if (!EditorPrefs.HasKey (NAME_CLASS_LABEL))
		{
			Debug.Log("ScriptReloaded: No key found");
			return;
		}

		// If they key does exist and the object doesn’t, it’s just a left over key.
		string name = EditorPrefs.GetString (NAME_CLASS_LABEL);
		AddGeneratedComponent(name, name);

		//Delete the key because we don’t need it anymore!
		EditorPrefs.DeleteKey(NAME_CLASS_LABEL);
	}

	static void AddGeneratedComponent(string objectName, string typeName)
	{
		GameObject go = GameObject.Find( objectName );

		if (go == null)
		{
			Debug.Log("AddGeneratedComponent: No game object found");
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
