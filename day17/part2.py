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
    valueAt1 = 0
    while i < times:
        i += 1
        pos = (pos + s) % i + 1
        if pos == 1:
            valueAt1 = i
    return valueAt1

print doStep(step, max)
