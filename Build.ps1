$str = GitVersion.exe | out-string
$json = ConvertFrom-Json $str
$version = $json.NuGetVersionV2

Write-Host "Building:"
Write-Host $Env:APPVEYOR_BUILD_VERSION
Write-Host $version

dotnet build .\src\Plugin.Plumber.Component.Decorator\Plugin.Plumber.Component.Decorator.csproj
dotnet pack .\src\Plugin.Plumber.Component.Decorator\Plugin.Plumber.Component.Decorator.csproj -c Release -o ..\..\artifacts -p:Version=$version