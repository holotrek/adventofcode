"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Disk {
    constructor(hash, salt) {
        this.hash = hash;
        this.salt = salt;
        this.grid = [];
        this.size = this.hash.length * 8;
    }
    fillDisk(key) {
        for (let i = 0; i < this.size; i++) {
            const rowKey = key + '-' + i.toString();
            const hash = this.hash.computeHash(rowKey, this.salt);
            let binary = hash.split('').map(x => ('0000' + parseInt(x, 16).toString(2)).slice(-4)).join('');
            this.grid.push(binary.split('').map(x => parseInt(x)));
        }
    }
    countUsed() {
        return this.grid.reduce((total, cur) => total += cur.filter(x => !!x).length, 0);
    }
    countRegions() {
        // Create clone of grid so we don't mess up original
        this.regionGrid = this.grid.map(y => y.map(x => x));
        let groupCount = 1;
        for (const i in this.regionGrid) {
            for (const j in this.regionGrid[i]) {
                if (this.regionGrid[i][j] === 1) {
                    this.assignGroup(this.regionGrid, [+i, +j], ++groupCount);
                }
            }
        }
        return groupCount - 1;
    }
    assignGroup(grid, pos, group) {
        grid[pos[0]][pos[1]] = group;
        if (this.regionGrid[pos[0] - 1] && this.regionGrid[pos[0] - 1][pos[1]] === 1) {
            this.assignGroup(grid, [pos[0] - 1, pos[1]], group);
        }
        if (this.regionGrid[pos[0] + 1] && this.regionGrid[pos[0] + 1][pos[1]] === 1) {
            this.assignGroup(grid, [pos[0] + 1, pos[1]], group);
        }
        if (this.regionGrid[pos[0]][pos[1] - 1] === 1) {
            this.assignGroup(grid, [pos[0], pos[1] - 1], group);
        }
        if (this.regionGrid[pos[0]][pos[1] + 1] === 1) {
            this.assignGroup(grid, [pos[0], pos[1] + 1], group);
        }
    }
    printGrid(delim, grid) {
        if (!grid) {
            grid = this.grid;
        }
        let str = '';
        for (const i of grid) {
            str += i.map(x => ('00' + x.toString(16)).slice(-2)).join(' ') + delim;
        }
        return str;
    }
}
exports.Disk = Disk;
