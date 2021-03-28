#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
hostip=127.0.0.1:6969

echo $DIR
echo $hostip

apt-get update -y
apt-get upgrade -y

apt-get install apt-transport-https -y 
apt-get install ca-certificates  -y
apt-get install curl -y
apt-get install gnupg -y
apt-get install lsb-release -y
apt-get install python3 -y
apt-get install git -y
apt-get install iproute2 -y

echo "finished installing 1 part of dependencies"

curl -fsSL https://download.docker.com/linux/ubuntu/gpg | gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

echo \
"deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) stable" | tee /etc/apt/sources.list.d/docker.list > /dev/null

apt-get update
apt-get install docker-ce -y
apt-get install docker-ce-cli -y
apt-get install containerd.io -y
apt-get install nodejs -y
apt-get install npm -y

echo "finished installing 2 part of dependencies"

service docker start

curl -L "https://github.com/docker/compose/releases/download/1.28.6/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

chmod +x /usr/local/bin/docker-compose

ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose

git clone -b v3 https://github.com/engineer-man/piston /var/docker/piston

echo "cloning"

docker-compose -f /var/docker/piston/docker-compose.yaml  up -d piston_api

echo "started docker"

npm install -g yarn 

yarn --cwd /var/docker/piston/cli

/var/docker/piston/cli/index.js ppman list > $DIR/list.txt

echo "printed list"

python3 $DIR/installRuntimes.py $DIR $hostip

chmod +x $DIR/installRuntimes.sh

$DIR/installRuntimes.sh

echo "finished installing"