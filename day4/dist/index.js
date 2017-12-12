"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var commander_1 = require("commander");
var program = new commander_1.Command();
program
    .version('0.0.1')
    .command('part1 [passphraseFile]', 'Run the part1 script, specifying the option for a file containing a list of passphrases.')
    .parse(process.argv);
