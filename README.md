# SimpleMicroservice
A simple microservice that works on AKS


To run locally on Docker Containers, from the root folder run:
`docker-compose up`

Navigate to localhost:5000 in a browser.

To run on AKS, following commands should help

1. `az aks create --resource-group myaksrg --name myHelloCluster --enable-addons http_application_routing --generate-ssh-keys`

replace `myaksrg` and `myhelloCluster` with yours.

2. `az aks get-credentials --resource-group myaksrg --name helloakscl`

The above command should set your context to AKS. Run this next:

3. `kubectl apply -f .\aksdeploy.yaml`

The above command pulls from my docker hub. If you wish to use yours, then
- tag your local image
- push to docker
- edit the Image in aksdeploy.yaml 

If you want this image to be pulled locally use: `docker pull nishanil/helloworld`

4. Run this to ensure the Public IP is set: `kubectl get service helloworld --watch`

The above command may take a little while to set the public ip.

Once the public ip is set, navigate to that in a browser. `http://<yourpublicip>/`