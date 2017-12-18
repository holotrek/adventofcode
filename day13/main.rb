require_relative "firewall.rb"

if __FILE__ == $0
    if ARGV.length < 2
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
    fw = Firewall.new(data)
    if ARGV[0] == "part1"
        puts fw.movePackets()
    else
        severity = 0
        delay = 0
        loop do
            severity = fw.movePackets(delay)
            break if !fw.isCaught
            puts delay
            delay += 1
        end
        puts delay
    end
end
