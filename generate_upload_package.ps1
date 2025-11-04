dotnet publish AGV.ZXing -c Release -r linux-x64 --no-self-contained
Compress-Archive -Path .\AGV.ZXing\bin\Release\net8.0\linux-x64\publish\* -DestinationPath ZXingLib.zip -Force