#!/bin/bash

_term() {
	echo "shutting down"
	docker stop piston_piston_api_1
	exit
}

trap _term SIGTERM SIGINT SIGQUIT SIGHUP ERR

FILE=/var/init.custom

hostip=$(ip route show | awk '/default/ {print $3}'):6969
echo "hostIP: $hostip"

service docker start
echo "start service for docker"
sleep 5
if test -f "$FILE"; then
    echo "$FILE exists."
else
	echo "start init"
	docker-compose -f /var/docker/piston/docker-compose.yaml up -d piston_api
	npm install -g yarn 
	yarn --cwd /var/docker/piston/cli
	echo "list"
	/var/docker/piston/cli/index.js -u http://$hostip ppman list > /var/remote/list.txt
	echo $(cat /var/remote/list.txt)
	python3 /var/remote/installRuntimes.py /var/remote $hostip
	chmod +x /var/remote/installRuntimes.sh
	/var/remote/installRuntimes.sh
	touch $FILE
	docker container ls -a
	docker stop piston_piston_api_1
	sleep 5
	echo "finish init"
fi
echo "compose up"
docker-compose -f /var/docker/piston/docker-compose.yaml up -d piston_api
while true; do
	sleep 1
done