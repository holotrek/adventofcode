#!/usr/bin/python
import sys

if len(sys.argv) < 2:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part1.py <puzzleInputFile>
  '''

data = [x.strip() for x in open(sys.argv[1]).readlines()]

def checkLink(num, comp):
    ports = [int(x) for x in comp.split('/')]
    if ports[0] == num:
        return ports[1]
    elif ports[1] == num:
        return ports[0]
    else:
        return None

def findLink(bridge, num):
    for c in data:
        if c in bridge:
            continue
        otherNum = checkLink(num, c)
        if otherNum != None:
            return c

def makeLink(bridge, num, comp):
    other = checkLink(num, comp)
    bridge.append(comp)
    return other

bridge = []
lastNum = 0
while True:
    lnk = findLink(bridge, lastNum)
    if lnk == None:
        break
    lastNum = makeLink(bridge, lastNum, lnk)

print bridge
