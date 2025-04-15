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

## Restart a Deployment
Restart a specific deployment:
```bash
kubectl rollout restart deployment <deployment-name>
```

## Get Namespaces
List all namespaces in the cluster:
```bash
kubectl get namespace
```

## Get Pods in a Specific Namespace
List all pods in a specific namespace:
```bash
kubectl get pods --namespace=<namespace-name>
```

## Get Storage Classes
List all storage classes in the cluster:
```bash
kubectl get storageclass
```

## Get Persistent Volume Claims (PVCs)
List all persistent volume claims in the current namespace:
```bash
kubectl get pvc
```

## Create a Secret
Create a generic secret with a specific key-value pair:
```bash
kubectl create secret generic <secret-name> --from-literal=<key>=<value>
```

For example, to create a secret for an MSSQL database with a password:
```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
```

## View Logs for a Deployment
View logs for a specific deployment:
```bash
kubectl logs deploy/<deployment-name>
```

Use these commands to manage your Kubernetes resources efficiently.