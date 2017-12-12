import { Command } from 'commander';
import * as fs from 'fs';
import * as path from 'path';
import * as os from 'os';

const program = new Command();
program
    .parse(process.argv);

const input = program.args.length ? program.args[0] : null;
if (input) {
    fs.readFile(path.join(__dirname, input), 'ascii', (err, data) => {
        if (err) {
            console.error(err);
        }
        else {
            const passphrases = data.split(os.EOL);
            for (const p of passphrases) {
                console.log(p);
            }
        }
    })
}
else {
    console.error(`Passphrase file is required.`);
}