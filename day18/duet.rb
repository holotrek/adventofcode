class Duet
    attr_reader :registers
    attr_accessor :lastSound

    def initialize()
    end

    def execute(str)
        parts = str.split(' ')
        if !parts[2].match(/^[[:alpha:]]$/)
            parts[2] = parts[2].to_i
        end
        
        case parts[0]
            when "snd"
                lastSound = registers[parts[1]]
                puts "Played sound of freq #{lastSound}"
        end
    end

end