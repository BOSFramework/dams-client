#!/usr/bin/env bash

scriptname=$(basename $0)
scriptpath=$(realpath $0)
repo_root="${SYSTEM_DEFAULTWORKINGDIRECTORY:-"$(dirname ${scriptpath%/*})"}"

function print_and_log {
  message="$@"
  logger "[$scriptname] $message"
  printf "[$scriptname] $message\n"
}

cd $repo_root
print_and_log "dotnet clean $repo_root"
dotnet clean -c Release
find | grep -E "/dist$|/bin$|/obj$|/TestResults$" | xargs rm -rf