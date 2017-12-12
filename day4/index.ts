import { Command } from 'commander';

const program = new Command();
program
.version('0.0.1')
.command('part1 [passphraseFile]', 'Run the part1 script, specifying the option for a file containing a list of passphrases.')
//.command('part2 [X]', 'Run the part2 script, specifying X where the spiral memory stops when there exists a number > X.')
.parse(process.argv);
