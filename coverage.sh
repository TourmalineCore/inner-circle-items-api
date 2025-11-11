#!/bin/bash
set -e
set -o pipefail

dotnet-coverage collect -f cobertura --session-id calculate-project-coverage-session --include-files "./**/bin/Debug/net9.0/*.dll" "dotnet test --no-build"

# Optional: verify file exists
if [ ! -f output.cobertura.xml ]; then
  echo "ERROR: Coverage file not generated!"
  exit 1
fi

line_coverage_float=$(xq -x '/coverage/@line-rate' output.cobertura.xml)
if [ -z "$line_coverage_float" ]; then
  echo "ERROR: Failed to extract line-rate from coverage file"
  exit 1
fi

line_coverage_percent=$(awk -v num="$line_coverage_float" 'BEGIN { printf "%.2f", num * 100 }')
echo "CALCULATED_COVERAGE=$line_coverage_percent"
