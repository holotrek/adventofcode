export class HexRoute {
    coords: [number, number] = [0, 0];
    max: [number, number] = [0, 0];

    calculateRoute(path: string[]) {
        path.forEach(p => {
            switch (p) {
                case 'n':
                    this.coords[1]-=2;
                    break;
                case 's':
                    this.coords[1]+=2;
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
                this.max = [this.coords[0], this.coords[1]];
            }
        });
    }

    getDistanceFromOrigin(): number {
        return this.getDistance(this.coords);
    }

    getMaxDistance(): number {
        return this.getDistance(this.max);
    }

    private getDistance(coords: [number, number]): number {
        return (Math.abs(coords[0]) + Math.abs(coords[1])) / 2;
    }
}
