#!/usr/bin/python
import sys

sum = 0
filePath = ''

if len(sys.argv) < 2:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part1.py filePath
  filePath	- The filename of a "spreadsheet" to check.
  '''

filePath = sys.argv[1]
arr = []
if filePath:
  inFile = open(filePath)
  str = inFile.read()
  print str

  lines = filter(None, str.split('\n'))

  for l in lines:
    l = l.replace('\t', ' ')
    arr.append(map(int, l.split(' ')))
  print arr

  for l in arr:
    sum += max(l) - min(l)
print sum
