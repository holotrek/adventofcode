class Dance
    attr_reader :programs

    def initialize(count = 16)
        @programs = (0..count-1).map { |i| (i + 97).chr }
    end

    def reorder(list)
        @programs = list
    end

    def doMove(str)
        case str[0]
            when 's' 
                spin(str[1..-1].to_i)
            when 'x'
                posses = str[1..-1].split('/')
                exchange(posses[0].to_i, posses[1].to_i)
            when 'p'
                posses = str[1..-1].split('/')
                partner(posses[0], posses[1])
        end
    end

    def spin(val)
        @programs.rotate!(-val)
    end

    def exchange(pos1, pos2)
        val1 = @programs[pos1]
        @programs[pos1] = @programs[pos2]
        @programs[pos2] = val1
    end

    def partner(val1, val2)
        pos1 = @programs.index(val1)
        pos2 = @programs.index(val2)
        @programs[pos1] = val2
        @programs[pos2] = val1
    end

end