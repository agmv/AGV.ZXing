Remove-Item -Path ZXingLib.zip -ErrorAction SilentlyContinue
Remove-Item -Path .\AGV.ZXing\bin\Release\net8.0\linux-x64\publish -Recurse -ErrorAction SilentlyContinue
dotnet publish AGV.ZXing -c Release -r linux-x64 --no-self-contained
Compress-Archive -Path .\AGV.ZXing\bin\Release\net8.0\linux-x64\publish\* -DestinationPath ZXingLib.zip -Force