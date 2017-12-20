require_relative "dance.rb"

if __FILE__ == $0
    if ARGV.length < 1
        raise("Command syntax error.

Usage: ruby main.rb <puzzleInputFile> [iterations(def: 1)]
        ")
        fail
    end

    iter = ARGV[1]&.to_i || 1
    data = File.read(ARGV[0])
    data = data.split(',')

    dance = Dance.new
    dances = []
    d = ''

    iter.times do |i|
        d = dance.programs.join('')
        if dances.include?(d)
            d = dances[iter % i]
            puts d
            break
        end
        
        dances.push(d)

        data.each do |m|
            dance.doMove(m)
        end
    end
end
