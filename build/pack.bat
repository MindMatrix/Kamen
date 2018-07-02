FOR /D /r %%G in ("src\obj\Release\*") DO gitlink "src\obj\Release\%%~nxG\MindMatrix.Kamen.pdb"
FOR /D /r %%G in ("src\bin\*") DO gitlink "src\bin\%%~nxG\MindMatrix.Kamen.pdb"
dotnet pack src\Kamen\MindMatrix.Kamen.csproj --include-symbols --no-build -c Release /p:VERSION=%1
