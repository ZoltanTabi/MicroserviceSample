# Kubernetes Cheat Sheet

This cheat sheet provides a quick reference to commonly used Kubernetes commands.

## Kubernetes Version
Check the version of Kubernetes:
```bash
kubectl version
```

## Apply a Deployment
Apply a deployment configuration from a YAML file:
```bash
kubectl apply -f <file-name>.yaml
```

## Get Deployments
List all deployments in the current namespace:
```bash
kubectl get deployments
```

## Get Pods
List all pods in the current namespace:
```bash
kubectl get pods
```

## Delete a Deployment
Delete a specific deployment:
```bash
kubectl delete deployment <metadata.name>
```

## Get Services
List all services in the current namespace:
```bash
kubectl get services
```

Use these commands to manage your Kubernetes resources efficiently.