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

  for l in arr:
    div = 0
    for i in l:
      for j in l:
        if i > j and i%j == 0:
          div = i/j
    sum += div

print sum
