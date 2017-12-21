require_relative "duet.rb"

if __FILE__ == $0
    if ARGV.length < 1
        raise("Command syntax error.

Usage: ruby main.rb <puzzleInputFile>
        ")
        fail
    end

    f = File.open(ARGV[1], 'r')
    data = []
    f.each_line do |l|
        data.push(l)
    end

    duet = Duet.new

    data.each do |instr|
        duet.execute(instr)
    end
end
