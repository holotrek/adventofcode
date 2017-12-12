#!/usr/bin/env node
'use strict';

const program = require('commander');
const AdditiveSpiralMemory = require('./additive-spiral-memory.js');

program
    .parse(process.argv);

const input = program.args.length ? program.args[0] : 1;
const spiral = new AdditiveSpiralMemory(input);
spiral.print();
console.log(spiral.largestSum);