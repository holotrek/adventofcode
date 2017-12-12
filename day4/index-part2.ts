import { Command } from 'commander';
import * as fs from 'fs';
import * as os from 'os';
import * as path from 'path';

import { PassphraseValidator } from './passphrase-validator';

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
            let validCount = 0;
            for (const p of passphrases) {
                if ((new PassphraseValidator(p, true)).validatePassphrase()) {
                    validCount++;
                }
            }
            console.log(`${validCount} passphrases are valid`);
        }
    });
}
else {
    console.error(`Passphrase file is required.`);
}