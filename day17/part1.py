#!/usr/bin/python
import sys

if len(sys.argv) < 2:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part1.py <step> [max (def: 2017)]
    step - the step value of the spinlock
  '''

step = int(sys.argv[1])
max = int(sys.argv[2]) if len(sys.argv) > 2 else 2017

def doStep(s, times):
    i = 0
    pos = 0
    arr = [0]
    while i < times:
        i += 1
        pos = (pos + s) % len(arr) + 1
        arr.insert(pos, i)
    return arr

a = doStep(step, max)

if a.index(max) + 1 < len(a):
    print a[a.index(max) + 1]
