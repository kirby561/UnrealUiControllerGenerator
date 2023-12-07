# Deprecated

This app has been replaced by a plugin directly in the Unreal Editor instead:

https://github.com/kirby561/UmgControllerGeneratorPlugin

# Unreal UI Controller Generator
This is a very simple WPF application that generates a C++ UI controller from a widget blueprint hierarchy in Unreal Engine.

The motivation for this was to make it faster to make widget blueprints in the Unreal Editor and be able to have a corresponding C++ controller for adding logic or dynamic behavior.
To use it:
1) Copy/Paste the "widget hierarchy" from within the Blueprint editor (Select the root node, press Ctrl+A, Ctrl+C):
   ![image](https://github.com/kirby561/UnrealUiControllerGenerator/assets/836379/b6868806-a6a0-4baf-b5dc-67f444c333de)
2) Paste the hierarchy into the input textbox in UnrealUiControllerGenerator
3) Enter the name of the controller (probably just the Widget Blueprint Name)
4) Enter the WBP reference path (right click the Widget Blueprint in the Unreal Editor->Copy Reference. Paste and remove everything but the path. For example: "/Game/Editor/UI/WBP_EditorPartDetails')
5) Select Window, Attach or Create depending on your use case.
    - Window will make a controller with AddToViewport and RemoveFromViewport methods. These can be used to show/hide the widget as an independent window.
    - Attach will make a controller with an Attach method meant to attach the controller to an existing widget to provide access to its elements.
    - Create will make a controller with a Create method that will just create an instance of the widget with access to its elements.
7) Select an output path where the .h and .cpp files will go
8) Press Create!

# Building
This project has no dependencies except Visual Studio 2022.
Open UnrealUiControllerGenerator.sln in VS2022 and build/run.
