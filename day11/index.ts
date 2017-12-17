import { HexRoute } from './hex-route';
import { Command } from 'commander';
import * as fs from 'fs';
import * as path from 'path';

const program = new Command();
program
    .version('0.0.1')
    .option('-p, --part <part>', 'Identifies which [part] of the puzzle to execute. Values: 1 or 2. Default: 1')
    .option('-f, --file <file>', 'The [file] to use for puzzle input (key). Overrides use of the -r/--route flag.')
    .option('-r, --route <route>', 'The puzzle input [route] if not using a file. Defaults to empty.')
    .parse(process.argv);

const part1 = (data: string) => {
    const route = new HexRoute();
    route.calculateRoute(data.split(','));
    console.log(route.getDistanceFromOrigin());
};

const part2 = (data: string) => {
    const route = new HexRoute();
    route.calculateRoute(data.split(','));
    console.log(route.getMaxDistance());
};

const options = {
    part: +program.part || 1,
    file: program.file,
    route: program.route
}

if (options.file) {
    fs.readFile(path.join(__dirname, '..', options.file), 'ascii', (err, data) => {
        if (err) {
            console.error(err);
        }
        else {
            if (options.part === 1) {
                part1(data.trim());
            }
            else if (options.part === 2) {
                part2(data.trim());
            }
        }
    });
}
else {
    if (options.part === 1) {
        part1(options.route || '');
    }
    else if (options.part === 2) {
        part2(options.route || '');
    }
}