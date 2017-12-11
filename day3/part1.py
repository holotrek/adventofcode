#!/usr/bin/python
import sys
from enum import Enum

distance = 0
input = int(raw_input('Please enter the square to carry data from (default: 1): '))
if not input:
  input = 1

class Directions(Enum):
  right = 1
  up = 2
  left = 3
  down = 4

def getNextDirection(dir):
  nextDir = Directions(dir.value + 1)
  if not nextDir:
    nextDir = Directions(1)
  return nextDir

grid = []
curX = 0
curY = 0
curDir = Directions.right
print curDir
curDir = getNextDirection(curDir)
print curDir

for i in range(input):
  num = i + 1

  #First, put the number in the current available space (caluclated by previous iteration)
  grid.append((curX, curY))

  #Second, calculate the next available space
  
