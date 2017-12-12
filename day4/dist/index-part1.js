"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var commander_1 = require("commander");
var fs = require("fs");
var path = require("path");
var os = require("os");
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
            for (var _i = 0, passphrases_1 = passphrases; _i < passphrases_1.length; _i++) {
                var p = passphrases_1[_i];
                console.log(p);
            }
        }
    });
}
else {
    console.error("Passphrase file is required.");
}
