# Docker sample, to reproduce an issue.

This was based on a repo containing test projects: https://github.com/dotnet/dotnet-docker

The non-working docker file is the one proposed by Visual Studio. It can be pushed to Docker Hub just fine, but fails when deploying to Scaleway.

## This works
```console
docker build --pull -t rg.fr-par.scw.cloud/<registry identifier>/aspnetapp:latest -f Dockerfile.working .
docker push rg.fr-par.scw.cloud/<registry identifier>/aspnetapp:latest
```

## This doesn't work
The docker build command succeeds, but there's an error when pushing the image.
```console
docker build --pull -t rg.fr-par.scw.cloud/<registry identifier>/aspnetapp:latest -f Dockerfile.notworking .
docker push rg.fr-par.scw.cloud/<registry identifier>/aspnetapp:latest
```
