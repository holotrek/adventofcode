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
}
exports.Disk = Disk;
