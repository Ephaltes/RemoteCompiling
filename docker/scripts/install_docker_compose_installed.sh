#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
hostip=127.0.0.1:2000

echo $DIR
echo $hostip

apt-get update -y

apt-get install apt-transport-https -y 
apt-get install ca-certificates  -y
apt-get install curl -y
apt-get install gnupg -y
apt-get install lsb-release -y
apt-get install python3 -y
apt-get install git -y
apt-get install iproute2 -y
apt-get install nodejs -y
apt-get install npm -y

echo "finished installing 2 part of dependencies"

git clone https://github.com/engineer-man/piston /var/docker/piston

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