#!/bin/bash

dotnet-coverage collect -f cobertura --session-id calculate-project-coverage-session --disable-console-output --include-files "./**/bin/Debug/net9.0/*.dll" "dotnet test --no-build"

line_coverage_float=$(cat output.cobertura.xml | xq -x /coverage/@line-rate)

line_coverage_percent=$(awk -v num="$line_coverage_float" 'BEGIN { print num * 100 }')
# rounded to 2 decimal digits
echo "CALCULATED_COVERAGE=$(printf "%.2f\n" "$line_coverage_percent")" >> "$GITHUB_ENV"
