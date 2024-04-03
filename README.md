# 385Examples

Using Unity 2020.3.1.f1 LTS for these examples

Updated some of the examples to 2023.2.14f1

To edit .gitignore, you might need to make it visible to your editor. See https://imstudio.medium.com/visual-studio-code-show-hidden-folder-5fd0f01d3d5e 

To use Visual Studio Code, see https://code.visualstudio.com/docs/other/unity and install extensions to VSC

Web Page: https://pisan385.github.io/385Examples/
Repository: https://github.com/pisan385/385Examples

# 01Basic

GameController - Escape will quit application or if in Unity editor or 
WebGL set playing to false

GreenUp - Has movement, M for mouse vs keyboard control. SPACE fires eggs. Eggs object must be a Prefab

Eggs - have a lifetime, and are destroyed when their life ends

# 02Collision

CameraSupport - calculates the boundaries of camera so we can tell if an object is inside/outside

GreenUp - Can destroy planes by touching (but Eggs don't destroy planes)

GameController - Responsible for creating new planes

Canvas - Has text, different coordinate system

Exercise: Get the eggs to destroy planes as well

# 03wasd_turbo

Change the speed of the square based on "1", TAB, LeftControl . Demonstrating StartCoroutine and Invoke to do things with delay

# Week1-5

See index.htm files
