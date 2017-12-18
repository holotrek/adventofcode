module Firewall
    attr_accessor :testVar

    class SecurityLayer
        def initialize(test)
            @testVar = test
        end

        def toString()
            return @testVar
        end
    end
end