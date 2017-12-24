class DuetProgram
    attr_reader :instructions
    attr_reader :registers
    attr_reader :inQueue
    attr_reader :outQueue
    attr_accessor :position
    attr_accessor :finished
    attr_accessor :waiting
    attr_accessor :sentCount
    attr_accessor :multCount

    def initialize(progId, instructions, setA1 = false)
        @registers = []
        if setA1
            @registers['a'.ord] = 1
        end
        @registers['p'.ord] = progId
        @instructions = instructions
        @position = 0
        @sentCount = 0
        @multCount = 0
        @inQueue = Queue.new
        @outQueue = Queue.new
    end

    def input(val)
        @inQueue << val
    end

    def increment(value)
        @position += value
        if @position >= @instructions.length
            @finished = true
        end
    end

    def toString()
        str = ''
        @registers.each_with_index do |r, i|
            if r
                str += i.chr + ':' + r.to_s + "\t"
            end
        end
        return str
    end

    def execute()
        if @finished
            return
        end

        str = @instructions[@position]
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

        if reg && !@registers[reg]
            @registers[reg] = 0
        end
        
#        print "Program #{@registers['p'.ord]}: "
        case parts[0]
            when "snd"
                send = if reg then @registers[reg] else firstVal end
                @outQueue << send
                @sentCount += 1
                increment(1)
#                puts "Sent #{send}"
            when "set"
                @registers[reg] = value
                increment(1)
#                puts "Set register #{reg} to #{value}"
            when "add"
                @registers[reg] += value
                increment(1)
#                puts "Added #{value} to register #{reg} = #{@registers[reg]}"
            when "sub"
                @registers[reg] -= value
                increment(1)
#                puts "Decreased register #{reg} by #{value} = #{@registers[reg]}"
            when "mul"
                @registers[reg] *= value
                increment(1)
                @multCount += 1
#                puts "Multiplied #{value} to register #{reg} = #{@registers[reg]}"
            when "mod"
                @registers[reg] %= value
                increment(1)
#                puts "Modded #{value} to register #{reg} = #{@registers[reg]}"
            when "rcv"
                if @inQueue.length > 0
                    @registers[reg] = @inQueue.pop
                    increment(1)
                    @waiting = false
#                    puts "Received #{@registers[reg]} into register #{reg}"
                else
                    @waiting = true
#                    puts "Nothing in queue to receive. Waiting..."
                end
            when "jgz"
                valToCheck = if reg then @registers[reg] else firstVal end
                if valToCheck > 0
                    increment(value)
#                    puts "Jumped #{value} instructions"
                    return
                else
                    increment(1)
#                    puts "Could not jump since check val is <= 0"
                end
            when "jnz"
                valToCheck = if reg then @registers[reg] else firstVal end
                if valToCheck != 0
                    increment(value)
#                    puts "Jumped #{value} instructions"
                    return
                else
                    increment(1)
#                    puts "Could not jump since check val is 0"
                end
        end

        if @finished
            puts "Program #{@registers['p'.ord]} finished."
        end
    end
end
