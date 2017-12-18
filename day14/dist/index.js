"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const disk_1 = require("./disk");
const commander_1 = require("commander");
const fs = require("fs");
const os = require("os");
const path = require("path");
const knot_hash_1 = require("./knot-hash");
const program = new commander_1.Command();
program
    .version('0.0.1')
    .option('-l, --length <length>', 'Indicates the [length] of the dense hash in bytes. Default: 16')
    .option('-p, --part <part>', 'Identifies which [part] of the puzzle to execute. Values: 1 or 2. Default: 1')
    .option('-f, --file <file>', 'The [file] to use for puzzle input (key). Overrides use of the -k/--key flag.')
    .option('-k, --key <key>', 'The puzzle input [key] if not using a file. Defaults to empty.')
    .parse(process.argv);
const partCommon = (data, length) => {
    const salt = [17, 31, 73, 47, 23];
    const hash = new knot_hash_1.KnotHash(length);
    const disk = new disk_1.Disk(hash, salt);
    disk.fillDisk(data);
    return disk;
};
const part1 = (data, length) => {
    const disk = partCommon(data, length);
    fs.writeFileSync(path.join(__dirname, '..', 'part1out.txt'), disk.printGrid(os.EOL));
    console.log(disk.countUsed());
};
const part2 = (data, length) => {
    const disk = partCommon(data, length);
    const regionCount = disk.countRegions();
    fs.writeFileSync(path.join(__dirname, '..', 'part2out.txt'), disk.printGrid(os.EOL, disk.regionGrid));
    console.log(regionCount);
};
const options = {
    length: +program.length || 16,
    part: +program.part || 1,
    file: program.file,
    key: program.key
};
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
