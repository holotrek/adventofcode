#!/usr/bin/env node
'use strict';

const Directions = require('./directions.js');

function SpiralMemory(finalNumber, initialDirection) {
    this.curDir = initialDirection ? initialDirection : Directions.down;
    this.grid = [];
    this.coords = [];
    this.coordBuffer = Math.ceil(Math.ceil(Math.sqrt(finalNumber)) / 2)
    this.generateSpiral(finalNumber);
};

SpiralMemory.prototype.generateSpiral = function(finalNumber) {
    let curPos = { x: 0, y: 0 };
    for (let i = 0; i < finalNumber; i++) {
        let num = i;

        // First, put the number in the current available space (caluclated by previous iteration)
        this.setValue(curPos, num);

        // Second, calculate the next available space
        if (this.isNextRotationSpaceEmpty(curPos.x, curPos.y)) {
            this.rotate();
        }

        curPos = this.moveForward(curPos.x, curPos.y, this.curDir);
    }
};

SpiralMemory.prototype.setValue = function(pos, num) {
    this.grid[num] = pos;
    const x = pos.x + this.coordBuffer;
    const y = pos.y + this.coordBuffer;
    if (!this.coords[y]) {
        this.coords[y] = [];
    }

    this.coords[y][x] = num + 1;
};

SpiralMemory.prototype.calculateDistance = function(fromNum, toNum) {
    const fromPos = this.grid[fromNum-1];
    const toPos = this.grid[toNum-1];
    return Math.abs(fromPos.x - toPos.x) + Math.abs(fromPos.y - toPos.y);
};

SpiralMemory.prototype.print = function() {
    for (let y = this.coords.length - 1; y >= 0; y--) {
        let row = '';
        if (!this.coords[y]) {
            continue;
        }

        for (let x = 0; x < this.coords[y].length; x++) {
            const num = this.coords[y][x];
            row += '\t' + (num ? num : '');
        }
        if (row) {
            console.log(row);
        }
    }
};

SpiralMemory.prototype.getNum = function(x, y) {
    if (!this.coords[y + this.coordBuffer]) {
        return null;
    }

    const num = this.coords[y + this.coordBuffer][x + this.coordBuffer];
    return num ? num : null;
};

SpiralMemory.prototype.isNextRotationSpaceEmpty = function(x, y) {
    const testDir = this.getNextDirection(this.curDir);
    const testPos = this.moveForward(x, y, testDir);
    return !this.getNum(testPos.x, testPos.y);
};

SpiralMemory.prototype.moveForward = function(x, y, dir) {
    switch (dir) {
        case Directions.right:
            x++;
            break;
        case Directions.up:
            y++;
            break;
        case Directions.left:
            x--;
            break;
        case Directions.down:
            y--;
            break;
    }

    return { x: x, y: y };
}

SpiralMemory.prototype.getNextDirection = function(dir) {
    let nextDir = dir + 1;
    if (!Directions[nextDir]) {
        nextDir = Directions.right;
    }
    return nextDir;
};

SpiralMemory.prototype.rotate = function() {
    this.curDir = this.getNextDirection(this.curDir);
};

module.exports = SpiralMemory;