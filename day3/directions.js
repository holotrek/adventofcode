#!/usr/bin/env node
'use strict';

let Dir;
((Dir) => {
    Dir[Dir["right"] = 1] = "right";
    Dir[Dir["up"] = 2] = "up";
    Dir[Dir["left"] = 3] = "left";
    Dir[Dir["down"] = 4] = "down";
})(Dir || (Dir = {}));

module.exports = Dir;