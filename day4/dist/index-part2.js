"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var commander_1 = require("commander");
var fs = require("fs");
var os = require("os");
var path = require("path");
var passphrase_validator_1 = require("./passphrase-validator");
var program = new commander_1.Command();
program
    .parse(process.argv);
var input = program.args.length ? program.args[0] : null;
if (input) {
    fs.readFile(path.join(__dirname, input), 'ascii', function (err, data) {
        if (err) {
            console.error(err);
        }
        else {
            var passphrases = data.split(os.EOL);
            var validCount = 0;
            for (var _i = 0, passphrases_1 = passphrases; _i < passphrases_1.length; _i++) {
                var p = passphrases_1[_i];
                if ((new passphrase_validator_1.PassphraseValidator(p, true)).validatePassphrase()) {
                    validCount++;
                }
            }
            console.log(validCount + " passphrases are valid");
        }
    });
}
else {
    console.error("Passphrase file is required.");
}
