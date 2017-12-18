class SecurityLayer
    attr_reader :range
    attr_accessor :hasPacket
    
    def initialize(range)
        @range = range
        @scannerPos = 0
        @scannerStep = 1
    end

    def packetCaught()
        return @hasPacket && @scannerPos == 0
    end

    def moveScanner(time)
        # Calculate scanner position based on time and range 
        
        # 1st: Assign middle values additional positions beyond the original range to accommodate for direction (up or down)
        newRange = (@range * 2) - 2

        # 2nd: Use modulo to determine the position of the scanner on the new range based on time
        newPos = time % newRange

        # 3rd: Recalculate position to real position, since the new range is larger than the actual range
        @scannerPos = newPos >= @range ? newRange - newPos : newPos
    end

    def toString()
        str = ''
        if @range > 0
            @range.times do |i|
                if hasPacket && i == 0
                    startBracket = '('
                    endBracket = ')'
                else
                    startBracket = '['
                    endBracket = ']'
                end

                str += startBracket + (i == @scannerPos ? 'S' : ' ') + endBracket + ' '
            end
        else
            str += hasPacket ? '(.)' : '...'
        end

        return str.strip
    end
end
