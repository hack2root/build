$SONARCLOUDTOKEN=$env:SONARCLOUDTOKEN

& dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll begin /d:sonar.login="$SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.xml" /d:sonar.coverage.exclusions="**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs" /d:sonar.organization="hack2root-github" /d:sonar.verbose=true 
& dotnet build --configuration Release
& dotnet test --configuration Release  --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
#& OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:".\Build.Tests\bin\Release\Build.Tests.dll -noshadow" -filter:"+[Build*]* -[*Test*,*Exception*,*Attribute*]*" -output:".\Build.Tests\coverage.xml"
& dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll end /d:sonar.login="$SONARCLOUDTOKEN"
