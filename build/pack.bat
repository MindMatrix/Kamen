FOR /D /r %%G in ("artifacts\Kamen\obj\Release\*") DO gitlink "artifacts\Kamen\obj\Release\%%~nxG\Kamen.pdb"
FOR /D /r %%G in ("artifacts\Kamen\bin\*") DO gitlink "artifacts\Kamen\bin\%%~nxG\Kamen.pdb"
dotnet pack source\Kamen\Kamen.csproj --include-symbols --no-build -c Release /p:VERSION=%1