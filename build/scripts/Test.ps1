param(
    [bool]$buildServerBuild = $false,
    [string]$buildDirectory = $null
)

$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'

$repositoryRoot = Resolve-Path "$PSScriptRoot\..\.."
if (!$buildDirectory) { $buildDirectory = Resolve-Path "$repositoryRoot\out"; }
$testResultsDirectory = "$buildDirectory\testresults";

Write-Host "`r`n------------- Executing Unit Tests with code coverage -------------`r`n"

dotnet "test" `
"--logger" "trx;LogFileName=UnitTestResults.trx" `
"--output" "$buildDirectory\publish\tests" `
"--results-directory" "$testResultsDirectory" `
"/bl:$buildDirectory\log\RunUnitTests.binlog;ProjectImports=Embed" `
"/p:CollectCoverage=true" `
"/p:CoverletOutput=$testResultsDirectory\unittest-coverage" `
"/p:CoverletOutputFormat=cobertura"  `
"/p:Exclude=[JFlepp.*.Test.*]*" `
"$repositoryRoot\JFlepp.Epub.Test.Unit\JFlepp.Epub.Test.Unit.csproj"

Write-Host "`r`n------------- Executing Integration Tests -------------`r`n"

dotnet "test" `
"--logger" "trx;LogFileName=IntegrationTestResults.trx" `
"--output" "$buildDirectory\publish\tests" `
"--results-directory" "$testResultsDirectory" `
"/bl:$buildDirectory\log\RunIntegrationTests.binlog;ProjectImports=Embed" `
"$repositoryRoot\JFlepp.Epub.Test.Integration\JFlepp.Epub.Test.Integration.csproj"

if ($buildServerBuild) { exit; }

Write-Host "`r`n------------- Generating Report -------------`r`n"

& "$buildDirectory\tools\reportgenerator.exe" `
"-reports:$testResultsDirectory\unittest-coverage.cobertura.xml" `
"-targetdir:$testResultsDirectory\unittest-coveragereport" `
-reporttypes:HTML

Invoke-Item "$testResultsDirectory\unittest-coveragereport\index.htm"