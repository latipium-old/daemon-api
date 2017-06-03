#!/bin/bash
set -e

# Build
msbuild /p:Configuration=Prerelease /p:SolutionDir=$(pwd) *.csproj

# Deploy
git fetch --unshallow # Fixes version number generation for more than 50 commits
msbuild /t:CIBuild /p:Configuration=Prerelease /p:SolutionDir=$(pwd) *.csproj
