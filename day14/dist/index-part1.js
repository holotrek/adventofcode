"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const commander_1 = require("commander");
const program = new commander_1.Command();
program
    .parse(process.argv);
const length = program.args.length > 1 ? program.args[0] : null;
const input = program.args.length > 1 ? program.args[1] : null;
if (length && input) {
}
else {
    console.error(`String Length and Puzzle Input File are required.`);
}
