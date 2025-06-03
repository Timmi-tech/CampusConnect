#!/bin/sh
set -e

# Run EF Core migrations
dotnet ef database update --project ../ChatSystem-1.Infrastructure --startup-project . --no-build

# Start the application details
exec dotnet ChatSystem-1.Api.dll