# FileUtilities

Utilities written in C# to split and merge large files

## Description

This sample show you how split a large file into different files, and how merge all those parts into a single file.
For it, you can choice split a file by size or by number of files.

##Class Diagram of the project
![screenshot](https://raw.githubusercontent.com/J0rgeSerran0/FileUtilities/master/FileUtilities_ClassDiagram.png)

##Sample use
* The next C# sample, split a large file into files of 4 Megabytes:
```csharp
var fileUtilities = new FileUtilities.Discompose();
fileUtilities.SplitFileBySizeOfFiles(@"C:\Temp\BigFile.txt", 4, FileUtilities.SizeType.MBytes);
```
* The next C# sample, split a large file into 5 files:
```csharp
var fileUtilities = new FileUtilities.Discompose();
fileUtilities.SplitFileBySizeOfFiles(@"C:\Temp\BigFile.txt", 5);
```
* The next C# sample, merge the split files into a file:
```csharp
var fileUtilities = new FileUtilities.Compose();
fileUtilities.MergeFiles(@"C:\Temp");
```
