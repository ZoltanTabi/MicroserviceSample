# Docker Cheat Sheet Documentation

This cheat sheet provides a quick reference for common Docker commands used in building, running, managing, and pushing Docker containers. Below is a breakdown of the commands and their purposes:

## Commands Example
```bash
# Build an image
docker build -t <docker id>/platformservice .

# Run a container
docker run -p 8080:80 -d <docker id>/platformservice

# List running containers
docker ps

# Stop a container
docker stop <container id>

# Start a container
docker start <container id>

# Push an image to a registry
docker push <docker id>/platformservice

# Execute a command in a running container
docker exec -it <container id> sh

# Inspect container details
docker inspect <container id>

# View container logs
docker logs <container id>
```

## Build
- The `docker build` command is used to create a Docker image from a Dockerfile and a specified context.
- The `-t` flag tags the image with a name in the format `<docker id>/<image name>`.
- Example: `docker build -t <docker id>/platformservice .` builds the image for the `platformservice` microservice.

## Run
- The `docker run` command creates and starts a new container from a specified image.
- The `-p` flag maps a port on the host machine to a port in the container (`8080:80` in this case).
- The `-d` flag runs the container in detached mode (in the background).
- Example: `docker run -p 8080:80 -d <docker id>/platformservice` starts the `platformservice` container.

## Check Running Containers
- The `docker ps` command lists all currently running containers.
- Useful for checking the status of containers and retrieving their IDs.

## Stop
- The `docker stop` command stops a running container.
- You need to specify the `<container id>` of the container you want to stop.

## Start
- The `docker start` command starts an existing container that has been stopped.
- You need to specify the `<container id>` of the container you want to start.

## Push
- The `docker push` command uploads a Docker image to a Docker registry (e.g., Docker Hub).
- The image must be tagged with your Docker ID to push it to your account.
- Example: `docker push <docker id>/platformservice` pushes the `platformservice` image to the registry.

## Execute Commands in a Running Container
- The `docker exec` command allows you to run a command inside a running container.
- The `-it` flag enables interactive mode and allocates a pseudo-TTY.
- Example: `docker exec -it <container id> sh` this opens a shell inside the container with ID `<container id>`.

- You can also execute additional commands inside the container. For example:
    ```bash
    apt-get update && apt-get install curl -y
    curl http://localhost:80/
    ```
    The first command updates the package list and installs `curl`, while the second command tests the container's HTTP server by making a request to `http://localhost:80/`.

## Inspect Container Details
- The `docker inspect` command retrieves detailed information about a container or image in JSON format.
- Example: `docker inspect <container id>` displays detailed information about the container with ID `<container id>`.

## View Container Logs
- The `docker logs` command fetches the logs of a container.
- Useful for debugging and monitoring container output.
- Example: `docker logs <container id>` retrieves the logs of the container with ID `<container id>`.
