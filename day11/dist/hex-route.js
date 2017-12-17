"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class HexRoute {
    constructor() {
        this.coords = [0, 0];
        this.max = [0, 0];
    }
    calculateRoute(path) {
        path.forEach(p => {
            switch (p) {
                case 'n':
                    this.coords[1] -= 2;
                    break;
                case 's':
                    this.coords[1] += 2;
                    break;
                case 'nw':
                    this.coords[0]--;
                    this.coords[1]--;
                    break;
                case 'ne':
                    this.coords[0]++;
                    this.coords[1]--;
                    break;
                case 'se':
                    this.coords[0]++;
                    this.coords[1]++;
                    break;
                case 'sw':
                    this.coords[0]--;
                    this.coords[1]++;
                    break;
            }
            if (this.getDistance(this.coords) > this.getDistance(this.max)) {
                console.log(this.coords);
                this.max = [this.coords[0], this.coords[1]];
            }
        });
    }
    getDistanceFromOrigin() {
        return this.getDistance(this.coords);
    }
    getMaxDistance() {
        return this.getDistance(this.max);
    }
    getDistance(coords) {
        return (Math.abs(coords[0]) + Math.abs(coords[1])) / 2;
    }
}
exports.HexRoute = HexRoute;
