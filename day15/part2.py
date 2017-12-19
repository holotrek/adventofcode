#!/usr/bin/python
import sys
import time

if len(sys.argv) < 4:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part2.py <iterations> <generatorA> <generatorB>
    iterations  - Number of generator iterations to compare.
    generatorN	- The initial value of generator N.
  '''

start_time = time.time()

iterations = long(sys.argv[1])
genA = long(sys.argv[2])
genB = long(sys.argv[3])

def generate(gen, factor, div, mod):
    i = 0
    while i < iterations:
        gen = (gen*factor) % div
        if gen % mod == 0:
            yield "{0:b}".format(gen)[-16:]
            i += 1

a = generate(genA, 16807, 2147483647, 4)
b = generate(genB, 48271, 2147483647, 8)

matchCount = 0
i = 0
while i < iterations:
    i += 1
    if a.next() == b.next():
        matchCount += 1

print matchCount
print "%s seconds" % (time.time() - start_time)
