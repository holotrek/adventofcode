class Group
    GROUP_OPENER = "{"
    GROUP_CLOSER = "}"
    GARBAGE_OPENER = "<"
    GARBAGE_CLOSER = ">"
    CANCELLER = "!"

    attr_accessor :data
    attr_reader :innerGroups
  
    # Create the object
    def initialize(data)
        @data = data
        @innerGroups = []
        if data[0] != GROUP_OPENER
            raise("Group must start with #{GROUP_OPENER}.")
            fail
        end
    end

    def consume()
        puts "Consuming #{data}"
        i = 1
        cancelled = false
        inGarbage = false
        while i <= @data.length
            print "Pos #{i}: #{@data[i]} - "
            if cancelled
                puts "cancelled"
                cancelled = false
            else
                case @data[i]
                    when CANCELLER
                        puts "cancelling next"
                        cancelled = true
                    when GROUP_OPENER
                        if !inGarbage
                            puts "opening new group"
                            grp = Group.new(@data[i..-1])
                            @innerGroups.push(grp)
                            i += grp.consume || 0
                        end
                    when GROUP_CLOSER
                        if !inGarbage
                            puts "closing group"
                            @data = @data[0, i+1]
                            return i
                        end
                    when GARBAGE_OPENER
                        puts "starting garbage"
                        inGarbage = true
                    when GARBAGE_CLOSER
                        puts "ending garbage"
                        inGarbage = false
                    else
                        puts "random char or in garbage"
                end
            end
            i += 1
        end
    end

    def printGroup(level = 0)
        puts "level #{level}: #{@data}"
        if innerGroups.length > 0
            innerGroups.each do |grp|
                grp.printGroup(level + 1)
            end
        end
    end

    def getScore(level = 0)
        level += 1
        score = level
        innerGroups.each do |grp|
            score += grp.getScore(level)
        end
        return score
    end
end

if __FILE__ == $0
    if ARGV.length < 2
        raise("Command syntax error.

Usage: ruby group.rb <part1|part2> <puzzleInput|puzzleInputFile>
    part1           Run part1 of Day 9
    part2           Run part2 of Day 9
    puzzleInput     Input the line of text to parse in double quotes, i.e. \"{{<\\!>}}\" (note: that any '!' must be escaped by a '\\').
    puzzleInputFile Or use the name of a file containing the line of text.
        ")
        fail
    end

    data = ARGV[1].dup
    
    # Strip escape characters before !, i.e. \!
    data.gsub! '\\!', '!'

    if File.file?(data)
        # File exists, so data is a filename, so read the contents
        data = File.read(data)
    end

    grp = Group.new(data)
    grp.consume
    grp.printGroup

    if ARGV[0] == "part1"
        puts grp.getScore
    else
    end
end