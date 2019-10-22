param(
    [string]$buildDirectory = $null
)

$repositoryRoot = "$PSScriptRoot\..\.."
$buildDirectory = "$repositoryRoot\out"

$nuget = "C:\Users\Jann Flepp\bin\nuget.exe";

Write-Host "`r`n------------- Removing old artifacts -------------`r`n"
Remove-Item $buildDirectory -Recurse -Force -Verbose

Write-Host "`r`n------------- Restoring tools -------------`r`n"
dotnet "tool" "install" "dotnet-reportgenerator-globaltool" "--tool-path" "$buildDirectory\tools"

Write-Host "`r`n------------- Restoring packages -------------`r`n"
dotnet restore "$repositoryRoot\JFlepp.Epub.sln"

Write-Host "`r`n------------- Building solution -------------`r`n"
dotnet "build" `
"--configuration" "Release" `
"--runtime" "win10-x64" `
"$repositoryRoot\JFlepp.Epub.sln" `
"/bl:$buildDirectory\log\BuildSolution.binlog;ProjectImports=Embed" `
"/t:build"

Write-Host "`r`n------------- Publishing Integration Tests -------------`r`n"
dotnet "publish" `
"--output" "$buildDirectory\publish\test" `
"--configuration" "Release" `
"--runtime" "win10-x64" `
"--no-build" `
"/bl:$buildDirectory\log\PublishIntegrationTests.binlog;ProjectImports=Embed" `
"$repositoryRoot\JFlepp.Epub.Test.Integration\JFlepp.Epub.Test.Integration.csproj" `
"/t:publish"

Write-Host "`r`n------------- Publishing Unit Tests -------------`r`n"
dotnet "publish" `
"--output" "$buildDirectory\publish\test" `
"--configuration" "Release" `
"--runtime" "win10-x64" `
"--no-build" `
"/bl:$buildDirectory\log\PublishUnitTests.binlog;ProjectImports=Embed" `
"$repositoryRoot\JFlepp.Epub.Test.Unit\JFlepp.Epub.Test.Unit.csproj" `
"/t:publish"

Write-Host "`r`n------------- Publishing Viewer -------------`r`n"
dotnet "publish" `
"--output" "$buildDirectory\publish\viewer" `
"--configuration" "Release" `
"/p:Platform=x64" `
"--runtime" "win10-x64" `
"--self-contained" `
"--no-build" `
"/bl:$buildDirectory\log\PublishViewer.binlog;ProjectImports=Embed" `
"$repositoryRoot\JFlepp.Epub.Viewer\JFlepp.Epub.Viewer.csproj" `
"/t:publish"
