module Firewall
    attr_accessor :secLayer

    class Firewall
        def initialize(test)
            @secLayer = SecurityLayer.new(test)
        end

        def toString()
            return @secLayer.toString
        end
    end
end