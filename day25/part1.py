#!/usr/bin/python
import itertools

def turing(state, steps, instructions):
    tape = [0]
    curPos = 0
    for _ in itertools.repeat(None, steps):
        instrSet = instructions[state]
        instr = instrSet[tape[curPos]]
        tape[curPos] = instr[0]
        curPos += instr[1]
        state = instr[2]
        if curPos < 0:
            tape.insert(0, 0)
            curPos = 0
        elif curPos == len(tape):
            tape.append(0)
    return tape

instructions = {}
instructions['A'] = {}
instructions['A'][0] = [1, 1, 'B']
instructions['A'][1] = [0, -1, 'B']
instructions['B'] = {}
instructions['B'][0] = [1, -1, 'A']
instructions['B'][1] = [1, 1, 'A']
tape = turing('A', 6, instructions)
print sum(tape)

instructions = {}
instructions['A'] = {}
instructions['A'][0] = [1, 1, 'B']
instructions['A'][1] = [0, -1, 'B']
instructions['B'] = {}
instructions['B'][0] = [1, -1, 'C']
instructions['B'][1] = [0, 1, 'E']
instructions['C'] = {}
instructions['C'][0] = [1, 1, 'E']
instructions['C'][1] = [0, -1, 'D']
instructions['D'] = {}
instructions['D'][0] = [1, -1, 'A']
instructions['D'][1] = [1, -1, 'A']
instructions['E'] = {}
instructions['E'][0] = [0, 1, 'A']
instructions['E'][1] = [0, 1, 'F']
instructions['F'] = {}
instructions['F'][0] = [1, 1, 'E']
instructions['F'][1] = [1, 1, 'A']
tape = turing('A', 12683008, instructions)
print sum(tape)


