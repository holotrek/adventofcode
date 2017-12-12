#!/usr/bin/env node
'use strict';

const program = require('commander');

program
.version('0.0.1')
.command('part1 [square]','Run the part1 script, specifying the option for which square to begin carrying data for (defaults to 1).')
.command('part2 [X]','Run the part2 script, specifying X where the spiral memory stops when there exists a number > X.')
.parse(process.argv);
