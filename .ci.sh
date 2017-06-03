#!/bin/bash
set -e

# Build
xbuild /t:CoreCompile /p:Configuration=Prerelease /p:SolutionDir=$(pwd) *.csproj

# Deploy
git fetch --unshallow # Fixes version number generation for more than 50 commits
xbuild /t:CIBuild /p:Configuration=Prerelease /p:SolutionDir=$(pwd) *.csproj
