export class PassphraseValidator {
    private words: Array<string> = [];
    constructor(
        private passphrase: string,
        private disallowAnagrams: boolean = false
    ) {
        if (passphrase) {
            this.words = passphrase.split(' ');
        }
    }

    validatePassphrase(): boolean {
        if (!this.words.length) {
            return false;
        }

        const groups = this.words.reduce((pv: any, cv: string) => {
            const word = this.disallowAnagrams ? this.alphabeticallySortedAnagram(cv) : cv;
            pv[word] = pv[word] || [];
            pv[word] = pv[word] ? pv[word] + 1 : 1;
            return pv;
        }, {});
        for (const g in groups) {
            if (groups.hasOwnProperty(g) && groups[g] > 1) {
                return false;
            }
        }
        return true;
    }

    alphabeticallySortedAnagram(word: string): string {
        return word.split('').sort((a, b) => a > b ? 1 : (a < b ? -1 : 0)).join('');
    }
}
