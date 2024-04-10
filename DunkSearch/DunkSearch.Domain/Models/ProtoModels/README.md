* First make sure these NuGet packages are installed:
  * Google.Protobuf
  * Google.Protobuf.Tools
* Right-click the tools package in the solution explorer and open it in file explorer.
* Navigate into its \tools\windows_x86 folder. There will be a protoc.exe file. 
* In the solution, make sure the .proto file exists in the desired format. 
* Open your terminal and navigate to the folder with protoc.exe, then run this command: 
`protoc -I="C:\DEV\dunksearch\DunkSearch\DunkSearch.Domain" --csharp_out="C:\DEV\dunksearch\DunkSearch\DunkSearch.Domain\Models\ProtoModels" C:\DEV\dunksearch\DunkSearch\DunkSearch.Domain\Models\ProtoModels\caption_request_params.proto`
* The file will now be generated and you can use them in your code.
* For more information, see here: https://protobuf.dev/getting-started/csharptutorial/