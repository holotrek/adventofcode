#!/usr/bin/env node
'use strict';

const Directions = require('./directions.js');

function AdditiveSpiralMemory(finalNumber, initialDirection) {
    this.curDir = initialDirection ? initialDirection : Directions.down;
    this.coords = [];
    this.coordBuffer = 0;
    this.largestSum = this.generateSpiral(finalNumber);
};

AdditiveSpiralMemory.prototype.generateSpiral = function(finalNumber) {
    let curPos = { x: 0, y: 0 };
    let sum = 0;
    while (sum <= finalNumber) {
        sum = 0;

        // First, calculate the next number by adding all the adjacent spaces with values
        for (let x = curPos.x - 1; x <= curPos.x + 1; x++) {
            for (let y = curPos.y - 1; y <= curPos.y + 1; y++) {
                if (x === curPos.x && y === curPos.y) {
                    continue;
                }
                const adjacent = this.getNum(x, y);
                sum += adjacent ? adjacent : 0;
            }
        }

        // Second, put the number in the current available space (caluclated by previous iteration)
        this.setValue(curPos, sum ? sum : 1);

        // Third, calculate the next available space
        if (this.isNextRotationSpaceEmpty(curPos.x, curPos.y)) {
            this.rotate();
        }

        curPos = this.moveForward(curPos.x, curPos.y, this.curDir);
    }

    return sum;
};

AdditiveSpiralMemory.prototype.increaseArraySize = function(pos) {
    if (pos.x < 0 || pos.y < 0) {
        const greatestNegative = Math.max(Math.abs(pos.x), Math.abs(pos.y));
        if (greatestNegative > this.coordBuffer) {
            const amountToInsert = greatestNegative - this.coordBuffer;
            this.coords = Array.apply([], { length: amountToInsert }).concat(this.coords);
            for (let y = 0; y < this.coords.length; y++) {
                this.coords[y] = Array.apply(null, { length: amountToInsert }).concat(this.coords[y]);
            }
            this.coordBuffer = greatestNegative;
        }
    }
};

AdditiveSpiralMemory.prototype.setValue = function(pos, num) {
    this.increaseArraySize(pos);
    const x = pos.x + this.coordBuffer;
    const y = pos.y + this.coordBuffer;
    if (!this.coords[y]) {
        this.coords[y] = [];
    }

    this.coords[y][x] = num;
};

AdditiveSpiralMemory.prototype.print = function() {
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

AdditiveSpiralMemory.prototype.getNum = function(x, y) {
    if (!this.coords[y + this.coordBuffer]) {
        return null;
    }

    const num = this.coords[y + this.coordBuffer][x + this.coordBuffer];
    return num ? num : null;
};

AdditiveSpiralMemory.prototype.isNextRotationSpaceEmpty = function(x, y) {
    const testDir = this.getNextDirection(this.curDir);
    const testPos = this.moveForward(x, y, testDir);
    return !this.getNum(testPos.x, testPos.y);
};

AdditiveSpiralMemory.prototype.moveForward = function(x, y, dir) {
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

AdditiveSpiralMemory.prototype.getNextDirection = function(dir) {
    let nextDir = dir + 1;
    if (!Directions[nextDir]) {
        nextDir = Directions.right;
    }
    return nextDir;
};

AdditiveSpiralMemory.prototype.rotate = function() {
    this.curDir = this.getNextDirection(this.curDir);
};

module.exports = AdditiveSpiralMemory;
