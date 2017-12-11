#!/usr/bin/env node
'use strict';

const program = require('commander');
const SpiralMemory = require('./spiral-memory.js');

program
    .parse(process.argv);

const input = program.args.length ? program.args[0] : 1;
const spiral = new SpiralMemory(input);
//spiral.print();
console.log(spiral.calculateDistance(1, input));