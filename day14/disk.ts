import { KnotHash } from './knot-hash';

export class Disk {
    grid: Array<Array<number>>;
    size: number;

    constructor(
        private hash: KnotHash,
        private salt: Array<number>
    ) {
        this.grid = [];
        this.size = this.hash.length * 8;
    }

    fillDisk(key: string) {
        for (let i = 0; i < this.size; i++) {
            const rowKey = key + '-' + i.toString();
            const hash = this.hash.computeHash(rowKey, this.salt);
            let binary = hash.split('').map(x => ('0000' + parseInt(x, 16).toString(2)).slice(-4)).join('');
            this.grid.push(binary.split('').map(x => parseInt(x)));
        }
    }

    countUsed() {
        return this.grid.reduce((total: number, cur: number[]) => total += cur.filter(x => !!x).length, 0);
    }
}
