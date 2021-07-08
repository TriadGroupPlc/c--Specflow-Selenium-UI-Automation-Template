#!/bin/bash
xvfb:20 &
export DISPLAY=:20
dotnet test ${PROJECT_NAME} --filter "TestCategory=${TAG}" --logger "trx;LogFileName=acceptance_test_result.trx" --no-build --no-restore