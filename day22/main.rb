require_relative "virus.rb"
require_relative "strong-virus.rb"

if __FILE__ == $0
    if ARGV.length < 3
        raise("Command syntax error.

Usage: ruby main.rb <part1|part2> <puzzleInputFile> <iterations>
        ")
        fail
    end

    f = File.open(ARGV[1], 'r')
    data = []
    f.each_line do |l|
        data.push(l)
    end

    iter = ARGV[2].to_i

    if ARGV[0] == 'part1'
        v = Virus.new(data)
        iter.times do
            v.move
        end
        puts v.infectCount
    else
        sv = StrongVirus.new(data)
        iter.times do
            sv.move
        end
        puts sv.infectCount
    end
end
