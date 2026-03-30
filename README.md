# Grid Paths Native AOT Demo

This repository demonstrates how to implement a grid path counting algorithm in C# using dynamic programming and expose it as a native library for a C++ application.

The solution contains two projects:

- `Task15Lib` — C# class library compiled as a native library with .NET Native AOT
- `Native.Test` — minimal C++ console application that loads the exported functions from the generated DLL

## What the library does

The library calculates the number of unique paths from the top-left corner of a grid to the bottom-right corner.

Allowed moves:
- right
- down

Implemented public API:

- `GetSquareGridPathCount(int size)`
- `GetRectGridPathCount(int rows, int cols)`

The internal algorithm uses dynamic programming.

## Solution structure

- `Task15Lib/` — C# Native AOT library
- `Native.Test/` — C++ console app
- `ProjectEuler.sln` — solution file

## Requirements

- Visual Studio 2022
- .NET 10 SDK
- Desktop development with C++ workload
- Windows x64

## Important build order

The C# project must be published first.

The C++ project does not reference the library directly at build time.  
It loads the generated DLL at runtime using `LoadLibrary` and `GetProcAddress`.

Because of that, the native DLL must exist before running the C++ application.

## How to run

### Step 1. Publish the C# native library

In Visual Studio:

1. Open the solution
2. Right-click `Task15Lib`
3. Click `Publish`
4. Use a Folder publish profile
5. Publish the project

The output DLL will be generated in a folder similar to:

`Task15Lib\bin\Release\net10.0\win-x64\`

### Step 2. Run the C++ project

After the publish step completes:

1. Set `Native.Test` as the startup project
2. Start the C++ application

The C++ app loads the DLL dynamically from the configured relative path.

## Notes

- If the C++ app fails to load the DLL, verify the path in `main.cpp`
- Make sure the DLL was published for `win-x64`
- Make sure the C++ project is also running as `x64`

## Why dynamic programming

Dynamic programming was chosen because it is simple, readable, and easy to extend.

For each cell, the number of paths is:

`paths[row, col] = paths[row - 1, col] + paths[row, col - 1]`

This approach can also be extended later to support blocked or dead cells.
