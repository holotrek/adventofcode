class Virus
    attr_accessor :nodes
    attr_accessor :width
    attr_accessor :pos
    attr_accessor :direction
    attr_accessor :infectCount

    def initialize(data)
        @nodes = []
        data.each do |l|
            @width = 0
            l.split('').each do |c|
                if c != $/
                    @nodes.push(c)
                    @width += 1
                end
            end
        end
        @pos = [
            (@width / 2).floor, 
            (@nodes.length / @width / 2).floor
        ]
        @direction = 'up'
        @infectCount = 0
    end

    def move()
        idx = @pos[1] * @width + @pos[0]
        if @nodes[idx] == '#'
            turn(1)
            @nodes[idx] = '.'
        else
            turn(-1)
            @nodes[idx] = '#'
            @infectCount += 1
        end

        case @direction
            when 'up'
                @pos = [@pos[0], @pos[1] - 1]
            when 'down'
                @pos = [@pos[0], @pos[1] + 1]
            when 'left'
                @pos = [@pos[0] - 1, @pos[1]]
            when 'right'
                @pos = [@pos[0] + 1, @pos[1]]
        end
        checkPos()
    end

    def turn(dir)
        case @direction
            when 'up'
                @direction = dir > 0 ? 'right' : 'left'
            when 'down'
                @direction = dir > 0 ? 'left' : 'right'
            when 'left'
                @direction = dir > 0 ? 'up' : 'down'
            when 'right'
                @direction = dir > 0 ? 'down' : 'up'
        end
    end

    def checkPos()
        height = @nodes.length / @width
        if @pos[1] < 0
            @nodes = Array.new(@width, '.').concat(@nodes)
            @pos[1] = 0
        elsif @pos[1] == height
            @nodes = @nodes.concat(Array.new(@width, '.'))
        elsif @pos[0] < 0
            i = 0
            while i < height
                @nodes.insert(i * @width + i, '.')
                i += 1
            end
            @width += 1
            @pos[0] = 0
        elsif @pos[0] == @width
            i = 1
            while i < height
                @nodes.insert(i * @width + (i-1), '.')
                i += 1
            end
            @nodes.push('.')
            @width += 1
        end
    end

    def toString()
        str = ''
        posIdx = @pos[1] * @width + @pos[0]
        @nodes.each_with_index do |c, i|
            str += i == posIdx ? '[' : ' '
            str += c
            str += i == posIdx ? ']' : ' '
            if (i+1) % @width == 0
                str += $/
            end
        end
        return str
    end
end
