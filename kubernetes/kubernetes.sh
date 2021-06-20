# switch to root sudo su -
## Install docker & nodejs
sudo apt-get update;

sudo apt-get install -y apt-transport-https ca-certificates curl gnupg lsb-release;
	
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg;

sudo echo \
  "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null;
  
sudo apt-get update;
sudo apt-get install -y docker-ce docker-ce-cli containerd.io;

sudo usermod -aG docker $USER;

#######################K3S############################################

#########MASTER###############
curl -sfL https://get.k3s.io | sh -s - --docker;
sudo ufw allow 6443/tcp;
sudo ufw allow 443/tcp;

#########MANUAL INPUT###########
# sudo cat /var/lib/rancher/k3s/server/node-token  ## TOKEN FOR SLAVES TO JOIN



#################SLAVES#########
curl -sfL http://get.k3s.io | K3S_URL=https://<master_IP>:6443 K3S_TOKEN=<join_token> sh -s - --docker ## Token from previous step


############Master########################
#sudo k3s ctr images import piston_preinstalled.tar.gz 

## Die zwei dateien müssen vorher kopiert und vorhanden sein
#sudo kubectl apply -f piston_api_deployment.yaml
#sudo kubectl apply -f piston_api_service.yaml























###########Master #########################
##verify the added nodes
#sudo kubectl get nodes
#
#curl -L https://github.com/kubernetes/kompose/releases/download/v1.22.0/kompose-linux-amd64 -o kompose
#chmod +x kompose
#sudo mv ./kompose /usr/local/bin/kompose
#
##sudo apt-get install -y nodejs npm
#
#sudo mkdir -p ~/docker/piston
#
#sudo git clone https://github.com/engineer-man/piston.git ~/docker/piston/temp
#
#sudo mv ~/docker/piston/temp/docker-compose.yaml ~/docker/piston/docker-compose.yaml
#
#sudo rm -r ~/docker/piston/temp
#
#cd ~/docker/piston
#
#sudo kompose convert
#
###### evtl. die replicates in api-deployment / persistent anpassen ##########
#sudo kubectl apply -f api-claim0-persistentvolumeclaim.yaml 
#sudo kubectl apply -f api-service.yaml
#sudo kubectl apply -f api-deployment.yaml
#
#### sudo kubectl get services / sudo kubectl get nodes / sudo kubectl get pods / sudo kubectl describe pods
	












########################Not Working Error 500 when joining cluster ############################### 
### Install microK8s
#
#sudo snap install microk8s --classic
#
##add your user not root to the admin group for kubernetes
#
#sudo usermod -a -G microk8s $USER
#sudo chown -f -R $USER ~/.kube
#su - $USER
#
#
#microk8s enable dashboard dns ingress storage
#
#
############################### MANUAL INPUT NEEDED  ###############################
## alias kube='microk8s.kubectl' ##alias binding
#
#### MASTER
## microk8s add-node
## microk8s enable metallb ##loadbalancer 
### output beim worker/slave eingeben um dem master zu joinen
