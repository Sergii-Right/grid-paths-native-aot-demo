#include <windows.h>
#include <iostream>
#include <vector>

using namespace std;

typedef unsigned long long(__cdecl* GetRectFn)(int, int);
typedef int(__cdecl* GetRectTextFn)(int, int, void*, int);

int main()
{
    auto dll = LoadLibraryA(R"(..\..\Task15Lib\bin\Release\net10.0\win-x64\Task15Lib.dll)");

    if (!dll)
    {
        cout << "Failed to load DLL\n";
        return 1;
    }

	//simple example, didn't use bigint, maximum is 20x20
    GetRectFn getRect = (GetRectFn)GetProcAddress(dll, "GetRectGridPathCount");
    if (!getRect)
    {
        cout << "Failed to get function pointer\n";
        FreeLibrary(dll);
        return 1;
    }
    cout << "20x20 = " << getRect(20, 20) << endl;


    //with bigint
    auto getRectText = (GetRectTextFn)GetProcAddress(dll, "GetRectGridPathCountText");
    if (!getRectText)
    {
        cout << "Failed to get function pointer. Error = " << GetLastError() << "\n";
        FreeLibrary(dll);
        return 1;
    }

    vector<char> buffer(1024);
    auto result = getRectText(50, 50, buffer.data(), (int)buffer.size());

    if (result > 0)
    {
        cout << "50x50 = " << buffer.data() << "\n";
    }
    else if (result > 1024)
    {
        cout << "Buffer too small. Required: " << result << "\n";
    }
    else
    {
        cout << "Call failed: " << result << "\n";
    }

    FreeLibrary(dll);
}
