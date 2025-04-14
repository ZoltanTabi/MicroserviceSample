# Kubernetes Extra Steps

## Deploy Ingress-NGINX Controller
To deploy the Ingress-NGINX controller, use the following command:

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.1/deploy/static/provider/cloud/deploy.yaml
```

This will set up the necessary resources for the Ingress-NGINX controller in your Kubernetes cluster.

This command is suitable for most cloud environments. If you are using Docker Desktop, refer to the [official documentation](https://kubernetes.github.io/ingress-nginx/deploy/#docker-desktop) for specific instructions.

---

## Update Hosts File

To test your setup locally, you may need to update your `hosts` file. Add the following entry to your `C:\Windows\System32\drivers\etc\hosts` file:

```
127.0.0.1 acme.com
```

This maps the domain `acme.com` to your local machine's IP address.

---

## Notes

- Ensure you have administrative privileges to edit the `hosts` file.
- After making changes, you may need to clear your DNS cache or restart your browser for the changes to take effect.
- Verify that the Ingress is properly configured to route traffic to your services.
