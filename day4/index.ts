import { Command } from 'commander';

const program = new Command();
program
.version('0.0.1')
.command('part1 [passphraseFile]', 'Run the part1 script, specifying the option for a file containing a list of passphrases.')
.command('part2 [passphraseFile]', 'Run the part2 script, specifying the option for a file containing a list of passphrases.')
.parse(process.argv);
