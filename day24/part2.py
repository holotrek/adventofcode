#!/usr/bin/python
import sys

if len(sys.argv) < 2:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part2.py <puzzleInputFile>
  '''

data = [x.strip() for x in open(sys.argv[1]).readlines()]
bridges = []
linksUsed = {}

def checkLink(num, comp):
    ports = [int(x) for x in comp.split('/')]
    if ports[0] == num:
        return [ports[0], ports[1]]
    elif ports[1] == num:
        return [ports[1], ports[0]]
    else:
        return None

def findBridge(bridge):
    for b in bridges:
        if set(b) == set(bridge):
            return True
    return False

def findNextLinks(lastNum, bridge = None, linksUsed = None):
    if bridge is None:
        bridge = []
    else:
        bridges.append(bridge)
    if linksUsed is None:
        linksUsed = []
    for l in data:
        if not l in linksUsed:
            lnk = checkLink(lastNum, l)
            if lnk != None:
                newBridge = bridge[:]
                newBridge.append(lnk[0])
                newBridge.append(lnk[1])
                findNextLinks(lnk[1], newBridge, linksUsed[:] + [l])

findNextLinks(0)

maxStrength = 0
lengths = map(lambda x: len(x), bridges)
maxLength = max(lengths)
for b in bridges:
    if len(b) == maxLength:
        strength = sum(b)
        if strength > maxStrength:
            maxStrength = strength

print maxStrength
