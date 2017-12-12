"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var PassphraseValidator = /** @class */ (function () {
    function PassphraseValidator(passphrase, disallowAnagrams) {
        if (disallowAnagrams === void 0) { disallowAnagrams = false; }
        this.passphrase = passphrase;
        this.disallowAnagrams = disallowAnagrams;
        this.words = [];
        if (passphrase) {
            this.words = passphrase.split(' ');
        }
    }
    PassphraseValidator.prototype.validatePassphrase = function () {
        var _this = this;
        if (!this.words.length) {
            return false;
        }
        var groups = this.words.reduce(function (pv, cv) {
            var word = _this.disallowAnagrams ? _this.alphabeticallySortedAnagram(cv) : cv;
            pv[word] = pv[word] || [];
            pv[word] = pv[word] ? pv[word] + 1 : 1;
            return pv;
        }, {});
        for (var g in groups) {
            if (groups.hasOwnProperty(g) && groups[g] > 1) {
                return false;
            }
        }
        return true;
    };
    PassphraseValidator.prototype.alphabeticallySortedAnagram = function (word) {
        return word.split('').sort(function (a, b) { return a > b ? 1 : (a < b ? -1 : 0); }).join('');
    };
    return PassphraseValidator;
}());
exports.PassphraseValidator = PassphraseValidator;
