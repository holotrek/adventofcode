require_relative "duet-program.rb"

if __FILE__ == $0
    if ARGV.length < 1
        raise("Command syntax error.

Usage: ruby main.rb <part1|part2> <puzzleInputFile>
        ")
        fail
    end

    f = File.open(ARGV[1], 'r')
    data = []
    f.each_line do |l|
        data.push(l)
    end

    if ARGV[0] == "part1"
        duet = DuetProgram.new(0, data)
        while !duet.finished
            duet.execute
        end
        puts duet.multCount
    else
        duet = DuetProgram.new(0, data, true)
        while !duet.finished
            duet.execute
            puts duet.toString
            if duet.registers['h'.ord]
                puts duet.registers['h'.ord]
            end
        end
    end
end
