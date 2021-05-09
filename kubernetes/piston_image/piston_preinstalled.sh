mkdir -p ~/piston_image/docker

echo 'FROM ghcr.io/engineer-man/piston@sha256:7b038a8a5733edbda6d33586e2e6490c3ba3f04f142d11ce1cec7a2876734b36

RUN mkdir -p /piston/packages
RUN mkdir -p /piston/jobs
RUN mkdir -p /tmp' > ~/piston_image/docker/dockerfile

cd ~/piston_image/docker

docker build . -t piston_bare

docker run --name piston_bare -p 2000:2000 -d piston_bare 

docker container ls

git clone https://github.com/engineer-man/piston.git 
cd piston 
git checkout 3e9705bcd39b52ca85384cb4217bec76fa820b73
cd cli && npm i && cd -
cli/index.js ppman install python
cli/index.js ppman install python 2.7.18
cli/index.js ppman install dotnet
cli/index.js ppman install java
cli/index.js ppman install mono
cli/index.js ppman install gcc

docker commit piston_bare piston_preinstalled

docker save piston_preinstalled | gzip > ~/piston_image/docker/piston_preinstalled.tar.gz
