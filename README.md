# PlatformManager-FirstMicroservice
This is my first microservice

### k8s ingress nginx controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.1/deploy/static/provider/cloud/deploy.yaml

### make acme.com point to localhost(127.0.0.1)
- on windows go to C:\Windows\System32\drivers\etc
- open hosts file as adminsrator
- add this line: 127.0.0.1 acme.com
- save

### create k8s secret for ms SQL
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"

### to run fresh new image
kubectl rollout restart deployment image-name

## KUBERNETES RUN COMMANDS
kubectl apply -f commands-depl.yaml
kubectl apply -f ingress-srv.yaml
kubectl apply -f local-pvc.yaml
kubectl apply -f mssql-plat-depl.yaml
kubectl apply -f platforms-depl.yaml
kubectl apply -f platforms-np-srv.yaml
kubectl apply -f rabbitmq-depl.yaml
