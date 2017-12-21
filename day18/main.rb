require_relative "duet.rb"
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
        duet = Duet.new(data)
        duet.play
    else
        p0 = DuetProgram.new(0, data)
        p1 = DuetProgram.new(1, data)
        while !p0.finished || !p1.finished
            p0.execute
            p1.execute
            while p0.outQueue.length > 0
                p1.inQueue << p0.outQueue.pop
            end
            while p1.outQueue.length > 0
                p0.inQueue << p1.outQueue.pop
            end

            if p0.waiting && p1.waiting
                break
            end
        end
        puts p1.sentCount
    end
end
