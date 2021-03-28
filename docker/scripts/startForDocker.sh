#!/bin/bash

_term() {
	docker stop piston_piston_api_1
}

trap _term SIGTERM

FILE=/var/init.custom

hostip=$(ip route show | awk '/default/ {print $3}')
echo "hostIP: $hostip"

service docker start
echo "start service for docker"
sleep 10
if test -f "$FILE"; then
    echo "$FILE exists."
else
	echo "start init"
	docker-compose --project-directory /var/docker/piston up -d piston_api
	npm install -g yarn 
	yarn --cwd /var/docker/piston/cli
	echo "list"
	/var/docker/piston/cli/index.js -u http://$hostip:6969 ppman list > /var/remote/list.txt
	echo $(cat /var/remote/list.txt)
	python3 /var/remote/installRuntimes.py /var/remote $hostip:6969
	chmod +x /var/remote/installRuntimes.sh
	/var/remote/installRuntimes.sh
	touch $FILE
	docker container ls -a
	docker stop piston_piston_api_1
	sleep 5
	echo "finish init"
fi
echo "compose up"
docker-compose --project-directory /var/docker/piston up
