



declare interface Location {
    navigate(pageUrl: String, params?: any): void;
    hashPage:string;
    query: { [id: string]: string };
}


Object.defineProperties(Location.prototype,
    {
        "hashQuery": {
            get: function () {
                var spltitedHash: string = this.hash.split("?");
                if (!spltitedHash[1])
                    return {};

                var splitedSearch = spltitedHash[1].split('&');
                var ret = {};
                for (var i = 0; i < splitedSearch.length; i++) {
                    var vals = splitedSearch[i].split("=")
                    ret[vals[0]] = vals[1];
                }
                return ret;
            }
        },
        "hashPage":{
            get:function(){
                var spltitedHash: string = this.hash.split("?");
                var pageUrlReg = /\!(.*?)$/;
                var url = pageUrlReg.exec(spltitedHash[0])[1];
                return url;
            }
        },
        "navigate": {
            value: function (pageUrl: String, params?: any) {

                var qs = "";
                if (params) {
                    qs="?";
                    for (var key in params) {
                        qs = `${qs}${key}=${params[key]}&`;
                    }
                    qs = qs.slice(0, -1);
                }

                window.location.hash = `!/${pageUrl}${qs}`;
            }
        }
    })


