import { Disk } from './disk';
import { Command } from 'commander';
import * as fs from 'fs';
import * as path from 'path';

import { KnotHash } from './knot-hash';

const program = new Command();
program
    .version('0.0.1')
    .option('-l, --length <length>', 'Indicates the [length] of the dense hash in bytes. Default: 16')
    .option('-p, --part <part>', 'Identifies which [part] of the puzzle to execute. Values: 1 or 2. Default: 1')
    .option('-f, --file <file>', 'The [file] to use for puzzle input (key). Overrides use of the -k/--key flag.')
    .option('-k, --key <key>', 'The puzzle input [key] if not using a file. Defaults to empty.')
    .parse(process.argv);

const part1 = (data: string, length: number) => {
    const salt = [17, 31, 73, 47, 23];
    const hash = new KnotHash(length);
    const disk = new Disk(hash, salt);
    disk.fillDisk(data);
    for (const i of disk.grid) {
        console.log(i.join(''));
    }
    console.log(disk.countUsed());
};

const part2 = (data: string, length: number) => {
};

const options = {
    length: +program.length || 16,
    part: +program.part || 1,
    file: program.file,
    key: program.key
}

if (options.file) {
    fs.readFile(path.join(__dirname, '..', options.file), 'ascii', (err, data) => {
        if (err) {
            console.error(err);
        }
        else {
            if (options.part === 1) {
                part1(data.trim(), options.length);
            }
            else if (options.part === 2) {
                part2(data.trim(), options.length);
            }
        }
    });
}
else {
    if (options.part === 1) {
        part1(options.key || '', options.length);
    }
    else if (options.part === 2) {
        part2(options.key || '', options.length);
    }
}