dotnet publish AGV.ZXing -c Release -r linux-x64 --self-contained false
Compress-Archive -Path .\AGV.ZXing\bin\Release\net6.0\linux-x64\publish\* -DestinationPath ZXingLib.zip -Force