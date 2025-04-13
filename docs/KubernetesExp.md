# Kubernetes YAML Files Explanation

This document provides an explanation of the two YAML files located in the `K8S` folder.

## 1. `deployment.yaml`

The `deployment.yaml` file defines the deployment configuration for your application. It specifies how many replicas of your application should run, the container image to use, and other deployment-related settings.

### Key Sections:
- **apiVersion**: Specifies the Kubernetes API version (e.g., `apps/v1`).
- **kind**: Indicates the resource type, which is `Deployment`.
- **metadata**: Contains the name and labels for the deployment.
- **spec**: Defines the desired state of the deployment, including:
    - **replicas**: Number of pod replicas.
    - **selector**: Matches the pods managed by this deployment.
    - **template**: Specifies the pod configuration, including:
        - **containers**: Defines the container image, ports, and environment variables.

## 2. `service.yaml`

The `service.yaml` file defines a Kubernetes Service, which exposes your application to other services or external traffic.

### Key Sections:
- **apiVersion**: Specifies the Kubernetes API version (e.g., `v1`).
- **kind**: Indicates the resource type, which is `Service`.
- **metadata**: Contains the name and labels for the service.
- **spec**: Defines the service configuration, including:
    - **selector**: Matches the pods to expose.
    - **ports**: Specifies the port mappings (e.g., targetPort and port).
    - **type**: Determines the service type (e.g., `ClusterIP`, `NodePort`, or `LoadBalancer`).

These YAML files work together to deploy and expose your application in a Kubernetes cluster.

## Understanding Deployment and Service Kinds with NodePort

### Deployment Kind
The `Deployment` kind in Kubernetes is used to manage stateless applications. It ensures that the desired number of pod replicas are running and provides features like rolling updates and rollbacks.

### Service Kind
The `Service` kind in Kubernetes is used to expose a set of pods as a network service. It provides stable networking and load balancing for the pods it selects.

### NodePort Service Type
The `NodePort` type of Service exposes the application on a static port on each node in the cluster. This allows external traffic to access the application using `<NodeIP>:<NodePort>`. It is commonly used for development or testing purposes.

- **Key Characteristics of NodePort**:
    - Opens a specific port on all cluster nodes.
    - Maps the external port to the target port of the pods.
    - Requires the port to be within the range `30000-32767` by default.

This configuration is useful for exposing services externally without requiring a cloud provider's load balancer.
