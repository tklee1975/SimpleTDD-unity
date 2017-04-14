SimpleTDD Framework for Unity
===========

Overview
-----------
SimpleTDD is a framework help developers to build Unit Test in an easy way like what JUnit do. (note:  JUnit is a famous unit test framework in Java)

The objectives of project is to:
- Help developers coding in the Test Driven style
- Easy to create a test without do extra coding such as building GUI
- Make coding more fun (remark: doing repeating things isn't fun!)

Unit Test Demos
----------
An unit test demostrating the Collision:
https://youtu.be/IWZja-UmgGM


How to install
----------
### Setup the SimpleTDD project
To use SimpleTDD, first of all is to download the project and export the package;

Here are the steps to setup the original project:
- Download the project from Github : https://github.com/tklee1975/SimpleTDD-unity
- After Download, Open the Unity project
- Open the Main Scene 'SimpleTDD/Scene/SimpleTDDMain'
- Play to try the demo unit test
- Export the package
  - Select 'Assets/Export Package'
  - Select the folder tree of "SimpleTDD"
  - Click "Export" button and save a location and file you want and use later;
    For example: SimpleTDD-unity.unitypackage


### Import the SimpleTDD to your project
Second, import the SimpleTDD unity package to your project

Here are the steps:
- Open your project
- Import the package exported before 'SimpleTDD-unity.unitypackage'
- If success, you will get two folders:
   'SimpleTDD' - the core package
   'UnitTest' - the demo unit test Script

### Create a Testing Scene
After setting up the framework, you can do any test you want:

Here are the steps to create new Testing Scene:
- Copy a scene from SimpleTDD/Scene/SampleTest
- Setup the Unit Test using Menu 'GameObject/SimpleTDD Setup'
- You now try the Test by click 'Play', the list of test buttons are based on the methods having attribute '[Test]' in your testing script

# Supporting Version
- Unity 5.6

# Help and Support
Free feel to contact me by facebook or twitter for any help or want to support me or want to have any new feature.
- My FB      : https://www.facebook.com/kencoder.hk.9
- My Twitter : https://twitter.com/kenlakoo
