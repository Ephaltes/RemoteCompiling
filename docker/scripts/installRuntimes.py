#!/usr/bin/python3

import sys
import os


def main(argv):
    if len(argv) < 2:
        print('installRuntimes.py <runtimes from piston and runtimes to install path> <IPAdress to API>')
        sys.exit(2)

    directory = argv[0]
    iphost = argv[1]
    pistonRuntimesFile = directory + "/list.txt"
    toInstallFile = directory + "/toinstall.txt"
    print('Input file is ', pistonRuntimesFile)
    symbol = "• "
    toInstall = []
    pistonRuntimes = []
    outputInstall = []
    with open(pistonRuntimesFile) as file:
        pistonRuntimes = file.readlines()
        for i, line in enumerate(pistonRuntimes):
            pistonRuntimes[i] = line.replace(symbol, "").strip()

    with open(toInstallFile) as file:
        toInstall = list(filter(None, (line.rstrip() for line in file)))

    for line in pistonRuntimes:
        if any(install in line for install in toInstall):
            outputInstall.append(line)

    fullpath = directory + "/installRuntimes.sh"
    print(fullpath)
    with open(fullpath, "w") as file:
        for install in outputInstall:
            file.write("/var/docker/piston/cli/index.js -u http://" + iphost +" ppman install " + install + "\n")


if __name__ == "__main__":
    main(sys.argv[1:])