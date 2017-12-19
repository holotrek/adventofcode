#!/usr/bin/python
import sys

if len(sys.argv) < 4:
  print 'Invalid command: ' + ' '.join(sys.argv)
  print '''
  Usage:
    ./part1.py <iterations> <generator1> <generator2>
    iterations  - Number of generator iterations to compare.
    generatorN	- The initial value of generator N.
  '''

iterations = long(sys.argv[1])
gen1 = long(sys.argv[2])
gen2 = long(sys.argv[3])

gen1factor = 16807
gen2factor = 48271
div = 2147483647

def generate(gen, factor, div):
    return (gen*factor) % div

def toBin(num):
    return "{0:b}".format(num)

matchCount = 0
i = 0
while i < iterations:
    i += 1

    gen1 = generate(gen1, gen1factor, div)
    gen2 = generate(gen2, gen2factor, div)

    gen1Last16 = toBin(gen1)[-16:]
    gen2Last16 = toBin(gen2)[-16:]

    # print gen1Last16
    # print gen2Last16
    # print ""
    
    if gen1Last16 == gen2Last16:
        matchCount+=1

print matchCount