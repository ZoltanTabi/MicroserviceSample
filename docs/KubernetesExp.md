# Kubernetes YAML Files Explanation

This document provides an explanation of the YAML files located in the `K8S` folder.

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

## Deployment Kind
The `Deployment` kind in Kubernetes is used to manage stateless applications. It ensures that the desired number of pod replicas are running and provides features like rolling updates and rollbacks.

## Service Kind
The `Service` kind in Kubernetes is used to expose a set of pods as a network service. It provides stable networking and load balancing for the pods it selects.

### NodePort Service Type
The `NodePort` type of Service exposes the application on a static port on each node in the cluster. This allows external traffic to access the application using `<NodeIP>:<NodePort>`. It is commonly used for development or testing purposes.

- **Key Characteristics of NodePort**:
    - Opens a specific port on all cluster nodes.
    - Maps the external port to the target port of the pods.
    - Requires the port to be within the range `30000-32767` by default.

This configuration is useful for exposing services externally without requiring a cloud provider's load balancer.

### ClusterIP Service Type
The `ClusterIP` type of Service is the default service type in Kubernetes. It exposes the application on an internal IP address within the cluster, making it accessible only to other services or pods inside the cluster.

- **Key Characteristics of ClusterIP**:
    - Provides internal communication between services and pods.
    - Does not expose the application to external traffic.
    - Automatically assigns a cluster-internal IP address.
    - Ideal for microservices that need to communicate internally without external access.

This configuration is commonly used for backend services or internal APIs that do not require external exposure.

### LoadBalancer Service Type
The `LoadBalancer` type of Service is used to expose the application to external traffic by provisioning a load balancer. This type is typically used in cloud environments where the cloud provider can automatically create and manage the load balancer.

- **Key Characteristics of LoadBalancer**:
    - Automatically provisions an external load balancer.
    - Exposes the application to external traffic using the load balancer's IP address or hostname.
    - Maps the external load balancer to the target port of the pods.
    - Requires a cloud provider that supports load balancers (e.g., AWS, Azure, GCP).

This configuration is ideal for production environments where external access to the application is required with high availability and scalability.

## PersistentVolumeClaim Kind
The `PersistentVolumeClaim` (PVC) kind in Kubernetes is used to request storage resources from the cluster. It acts as a claim to a `PersistentVolume` (PV), which provides the actual storage. PVCs allow applications to use storage without needing to know the underlying storage details.

### Key Sections:
- **apiVersion**: Specifies the Kubernetes API version (e.g., `v1`).
- **kind**: Indicates the resource type, which is `PersistentVolumeClaim`.
- **metadata**: Contains the name and labels for the PVC.
- **spec**: Defines the storage request, including:
    - **accessModes**: Specifies how the volume can be accessed (e.g., `ReadWriteOnce`, `ReadOnlyMany`, `ReadWriteMany`).
    - **resources**: Requests the storage size (e.g., `storage: 10Gi`).
    - **storageClassName**: Specifies the storage class to use (optional).

### How PVC Works:
1. A PVC is created by defining the desired storage requirements.
2. Kubernetes matches the PVC to an available PV that meets the requirements.
3. Once bound, the PVC can be used by pods to access the storage.

### Example Use Case:
A PVC is commonly used to provide persistent storage for databases, logs, or other stateful applications. It ensures that data persists even if the pod is restarted or rescheduled.

### Example YAML:
```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: my-pvc
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 5Gi
  storageClassName: standard
```

This configuration requests a 5Gi storage volume with `ReadWriteOnce` access mode, using the `standard` storage class.


## Persistent Volume Claim (PVC):
 * A Persistent Volume Claim is a request for storage by a user.
 * It abstracts the details of the underlying storage and allows users to request specific storage requirements such as size and access modes (e.g., ReadWriteOnce, ReadOnlyMany, ReadWriteMany).
 * PVCs are bound to Persistent Volumes (PVs) that satisfy the requested storage requirements.
 * Once bound, the PVC can be used by pods to access the storage.

## Persistent Volume (PV):
 * A Persistent Volume is a piece of storage in the cluster that has been provisioned by an administrator or dynamically provisioned using a Storage Class.
 * PVs are independent of the lifecycle of any individual pod and provide a way to persist data beyond the lifespan of a pod.
 * They have specific attributes such as capacity, access modes, and reclaim policies (e.g., Retain, Recycle, Delete).

## Storage Class:
 * A Storage Class defines the storage provisioner and parameters for dynamically provisioning Persistent Volumes.
 * It allows administrators to define different classes of storage (e.g., SSD, HDD, network-attached storage) with varying performance and cost characteristics.
 * Users can specify a Storage Class in their PVC to request storage with specific characteristics.
 * Storage Classes enable dynamic provisioning, where PVs are created on-demand when a PVC is created.
