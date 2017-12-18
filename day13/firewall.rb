require_relative "security-layer.rb"

class Firewall
    attr_accessor :layers
    attr_accessor :isCaught
    attr_accessor :severity
    
    def initialize(lines)
        @layers = []
        lines.each do |l|
            keyval = l.split(':').map(&:to_i)
            layers[keyval[0]] = SecurityLayer.new(keyval[1])
        end
        layers.each_with_index do |l, i|
            if l.nil?
                layers[i] = SecurityLayer.new(0)
            end
        end
    end

    def movePackets(delay = 0, debug = false)
        pico = delay
        caughtIn = []

        if debug then puts "Initial State:" end
        if debug then puts toString end

        @layers.each_with_index do |l, i|
            if movePacket(l, pico, debug)
                caughtIn.push(i)
            end
            pico += 1
        end
        
        @severity = caughtIn.inject do |memo, i|
            memo += i * @layers[i].range
        end
        @isCaught = caughtIn.length > 0
        return @severity
    end

    def movePacket(layer, pico, debug = false)
        isCaught = false
        layer.moveScanner(pico)
        layer.hasPacket = true

        if debug then printDebug(pico) end

        if layer.packetCaught
            isCaught = true
        end

        if debug then printDebug() end

        layer.hasPacket = false

        return isCaught
    end

    def printDebug(pico = nil)
        if pico.nil?
            puts "-------------------"
        else
            puts "==================="
            puts "Picosecond #{pico}:"
        end
        puts toString
    end

    def toString()
        str = ''
        @layers.each_with_index do |l, i|
            str += i.to_s + ': ' + (l ? l.toString : '')
            str += $/
        end

        return str
    end
end
