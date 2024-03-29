class Duet
    attr_reader :instructions
    attr_reader :registers
    attr_accessor :lastSound
    attr_accessor :position
    attr_accessor :recoveredFreq

    def initialize(instructions)
        @registers = []
        @instructions = instructions
        @position = 0
    end

    def play()
        while @position < @instructions.length && !@recoveredFreq
            execute(@instructions[@position])
        end
    end

    def execute(str)
        parts = str.split(' ')
        reg = nil
        firstVal = nil
        value = nil

        if parts[1].match(/^[[:alpha:]]$/)
            reg = parts[1].ord
        else
            firstVal = parts[1].to_i
        end

        if parts.length > 2
            if parts[2].match(/^[[:alpha:]]$/)
                value = @registers[parts[2].ord]
            else
                value = parts[2].to_i
            end
        end

        if !@registers[reg]
            @registers[reg] = 0
        end
        
        case parts[0]
            when "snd"
                @lastSound = if reg then @registers[reg] else firstVal end
                puts "Played sound of freq #{lastSound}"
            when "set"
                @registers[reg] = value
                puts "Set register #{reg} to #{value}"
            when "add"
                @registers[reg] += value
                puts "Added #{value} to register #{reg} = #{@registers[reg]}"
            when "mul"
                @registers[reg] *= value
                puts "Multiplied #{value} to register #{reg} = #{@registers[reg]}"
            when "mod"
                @registers[reg] %= value
                puts "Modded #{value} to register #{reg} = #{@registers[reg]}"
            when "rcv"
                valToCheck = if reg then @registers[reg] else firstVal end
                if valToCheck > 0
                    @recoveredFreq = @lastSound
                    puts "Recovered frequency #{@lastSound}"
                else
                    puts "Could not recover frequency since check val is 0"
                end
            when "jgz"
                valToCheck = if reg then @registers[reg] else firstVal end
                if valToCheck > 0
                    @position += value
                    puts "Jumped #{value} instructions"
                    return
                else
                    puts "Could not jump since check val is 0"
                end
        end
        @position += 1
    end

end