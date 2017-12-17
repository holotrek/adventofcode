export class KnotHash {
    values: Array<number>;
    curPos: number;
    skip: number;

    constructor(
        private length: number
    ) {
        this.values = Array.from(Array(length).keys());
        this.curPos = 0;
        this.skip = 0;
    }

    computeHash(key: string, salt: Array<number>, iterations = 64) {
        const asciiVals = key.split('').map(c => c.charCodeAt(0)).concat(salt);
        for (let i = 0; i < iterations; i++) {
            for (const j of asciiVals) {
                this.twist(j);
            }
        }

        const denseHash = [];
        for (var i = 0; i < this.values.length; i += 16) {
            let result = this.values[i];
            for (var j = i + 1; j < i + 16; j++) {
                result ^= this.values[j];
            }
            denseHash.push(result);
        }

        let hex = '';
        for (var i = 0; i < denseHash.length; i++) {
            let hexVal = denseHash[i].toString(16);
            hex += ('00' + hexVal).slice(-2);
        }

        return hex;
    }

    twist(length: number): void {
        let endPos = this.curPos + length - 1;
        let startLen = 0;
        if (endPos >= this.values.length) {
            endPos = this.values.length - 1;
            startLen = this.curPos + length - this.values.length;
        }

        let segment = this.values.slice(this.curPos, endPos + 1).concat(this.values.slice(0, startLen));
        segment = segment.reverse();

        let i = this.curPos;
        for (let s = 0; s < segment.length; s++) {
            this.values[i] = segment[s];
            i++;
            i = i % this.values.length;
        }

        this.curPos += length + this.skip;
        this.curPos = this.curPos % this.values.length;
        this.skip++;
    }
}