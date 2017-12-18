require_relative "security-layer.rb"

class Firewall
    attr_accessor :secLayer
    
    def initialize(test)
        @secLayer = SecurityLayer.new(test)
    end

    def toString()
        return @secLayer.toString
    end
end