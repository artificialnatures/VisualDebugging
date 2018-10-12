# Visual Debugging
A repository for experiments visualizing geometry data in debugging environments

## GeometryVisualizer
The GeometryVisualizer solution contains all of the core functionality for managing geometry data, processes and communication. These are the projects included:

### GeometryVisualizer
The GeometryVisualizer project defines interfaces and implementation classes for encoding geometry and scenes (collections of geometry data).

### GeometryVisualizer.Communication
The GeometryVisualizer.Communication project defines interfaces and implementation classes for communicating data between processes. This includes serialization logic. The default communication method uses sockets on the localhost.

### GeometryVisualizer.Process
The GeometryVisualizer.Process project defines interfaces and implementation classes for launching and managing the graphical visualizer process. The default visualizer is a [Unity](http://unity3d.com) application.

### GeometryVisualizer.Tests
Unit tests for the GeometryVisualizer solution.

### GeometryVisualizer.Console
GeometryVisualizer.Console provides an axample application for testing the visualizer and learning how it operates. *This is a good place to start.*

## GeometryVisualizerUnityProject
This is the [Unity](http://unity3d.com) project that builds the visualizer executables. It contains only a minimal amount of code. Most of the logic (including Unity-specific logic) is in the GeometryVisualizer solution.

## VisualStudio
This solution contains an implementation of a Visual Studio debugger extension based on GeometryVisualizer.

## Building
Build GeometryVisualizer.sln This copies assemblies to GeometryVisualizerUnityProject and Package/lib/
Build GeometryVisualizerUnityProject into Package/contentFiles/GeometryVisualizer/platform/ Name the executable GeometryVisualizer.
