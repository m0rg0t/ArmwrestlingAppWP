# Stopped game regression checks

Run from repository root with the .NET 10 SDK:

```sh
dotnet run --project tests/Portable.Tests/Portable.Tests.csproj --configuration Release
```

Links the complete production MainPage.xaml.cs, with small test-only Phone UI, dispatcher, accelerometer and sharing substitutes. Synthetic queued readings demonstrate that stopped rounds and newly restarted rounds must ignore old callbacks; restart labels reset immediately. Positive/negative active readings still cap the ball movement. The original source changes a stopped first-player score from 12 to 22.

The harness has no NuGet package dependencies and clears package sources in its own NuGet.Config. It is a separate modern test host, not a port or a whole-app build. Stubs are test-only and are not included in the legacy project. Only synthetic fixtures are used; no HTTP requests, device access, capture, user data or credentials.

Still unvalidated: Windows Phone 7.1 Silverlight SDK, XAML build, hardware sensor timing and physical-device gameplay. Original target, dependency manifests and binary references are preserved. Dependency upgrades require a compatible historical restore/build baseline; this PR does not claim modernized dependencies or a functioning remote service.
