"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const commander_1 = require("commander");
const fs = require("fs");
const path = require("path");
const knot_hash_1 = require("./knot-hash");
const program = new commander_1.Command();
program
    .parse(process.argv);
const length = program.args.length > 1 ? program.args[0] : null;
const input = program.args.length > 1 ? program.args[1] : null;
if (length && input) {
    fs.readFile(path.join(__dirname, input), 'ascii', (err, data) => {
        if (err) {
            console.error(err);
        }
        else {
            const salt = [17, 31, 73, 47, 23];
            const hash = new knot_hash_1.KnotHash(+length);
            console.log(hash.computeHash(data, salt));
        }
    });
}
else {
    console.error(`String Length and Puzzle Input File are required.`);
}
