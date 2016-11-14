Object.defineProperties(Location.prototype, {
    "hashQuery": {
        get: function () {
            var spltitedHash = this.hash.split("?");
            if (!spltitedHash[1])
                return {};
            var splitedSearch = spltitedHash[1].split('&');
            var ret = {};
            for (var i = 0; i < splitedSearch.length; i++) {
                var vals = splitedSearch[i].split("=");
                ret[vals[0]] = vals[1];
            }
            return ret;
        }
    },
    "hashPage": {
        get: function () {
            var spltitedHash = this.hash.split("?");
            var pageUrlReg = /\!(.*?)$/;
            var url = pageUrlReg.exec(spltitedHash[0])[1];
            return url;
        }
    },
    "navigate": {
        value: function (pageUrl, params) {
            var qs = "";
            if (params) {
                qs = "?";
                for (var key in params) {
                    qs = "" + qs + key + "=" + params[key] + "&";
                }
                qs = qs.slice(0, -1);
            }
            window.location.hash = "!/" + pageUrl + qs;
        }
    }
});
//# sourceMappingURL=location.js.map