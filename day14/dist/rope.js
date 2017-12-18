"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Rope {
    constructor(length) {
        this.length = length;
        this.values = Array.from(Array(length).keys());
        this.curPos = 0;
        this.skip = 0;
    }
    twist(length) {
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
exports.Rope = Rope;
