$ErrorActionPreference = "Stop"

# Build the Docker image
docker build --pull -t rg.fr-par.scw.cloud/sofun-api-reg/aspnetapp:latest -f Dockerfile.working .

# Push the Docker image to the registry
docker push rg.fr-par.scw.cloud/sofun-api-reg/aspnetapp:latest