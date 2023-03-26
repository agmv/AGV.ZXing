dotnet publish -c Release
Compress-Archive -Path .\AGV.ZXing\bin\Release\net6.0\publish\* -DestinationPath ZXingLib.zip -Force