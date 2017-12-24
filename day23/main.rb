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

    def is_prime?(num)
        return false if num <= 1
        Math.sqrt(num).to_i.downto(2).each {|i| return false if num % i == 0}
        true
    end

    if ARGV[0] == "part1"
        duet = DuetProgram.new(0, data)
        while !duet.finished
            duet.execute
        end
        puts duet.multCount
    else
        count = 0
        input = 57
        low = 100000 + (input * 100)
        high = low + 17000
        (low..high+1).step(17) do |i|
            if not is_prime?(i)
                count += 1
            end
        end
        puts count
    end
end
