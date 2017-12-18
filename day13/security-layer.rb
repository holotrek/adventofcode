class SecurityLayer
    attr_accessor :testVar
    
    def initialize(test)
        @testVar = test
    end

    def toString()
        return @testVar
    end
end