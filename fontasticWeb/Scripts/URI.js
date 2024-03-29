﻿/*! URI.js v1.10.1 http://medialize.github.com/URI.js/ */
/* build contains: URI.js */
(function (m, t) { "object" === typeof exports ? module.exports = t(require("./punycode"), require("./IPv6"), require("./SecondLevelDomains")) : "function" === typeof define && define.amd ? define(["./punycode", "./IPv6", "./SecondLevelDomains"], t) : m.URI = t(m.punycode, m.IPv6, m.SecondLevelDomains) })(this, function (m, t, s) {
    function d(a, b) { if (!(this instanceof d)) return new d(a, b); void 0 === a && (a = "undefined" !== typeof location ? location.href + "" : ""); this.href(a); return void 0 !== b ? this.absoluteTo(b) : this } function p(a) {
        return a.replace(/([.*+?^=!:${}()|[\]\/\\])/g,
        "\\$1")
    } function v(a) { return String(Object.prototype.toString.call(a)).slice(8, -1) } function k(a) { return "Array" === v(a) } function u(a, b) { var c, e; if (k(b)) { c = 0; for (e = b.length; c < e; c++) if (!u(a, b[c])) return !1; return !0 } var d = v(b); c = 0; for (e = a.length; c < e; c++) if ("RegExp" === d) { if ("string" === typeof a[c] && a[c].match(b)) return !0 } else if (a[c] === b) return !0; return !1 } function x(a, b) { if (!k(a) || !k(b) || a.length !== b.length) return !1; a.sort(); b.sort(); for (var c = 0, e = a.length; c < e; c++) if (a[c] !== b[c]) return !1; return !0 } function w(a) {
        return encodeURIComponent(a).replace(/[!'()*]/g,
        escape).replace(/\*/g, "%2A")
    } var f = d.prototype, q = Object.prototype.hasOwnProperty; d._parts = function () { return { protocol: null, username: null, password: null, hostname: null, urn: null, port: null, path: null, query: null, fragment: null, duplicateQueryParameters: d.duplicateQueryParameters } }; d.duplicateQueryParameters = !1; d.protocol_expression = /^[a-z][a-z0-9-+-]*$/i; d.idn_expression = /[^a-z0-9\.-]/i; d.punycode_expression = /(xn--)/i; d.ip4_expression = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/; d.ip6_expression = /^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$/;
    d.find_uri_expression = /\b((?:[a-z][\w-]+:(?:\/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?\u00ab\u00bb\u201c\u201d\u2018\u2019]))/ig; d.defaultPorts = { http: "80", https: "443", ftp: "21", gopher: "70", ws: "80", wss: "443" }; d.invalid_hostname_characters = /[^a-zA-Z0-9\.-]/; d.encode = w; d.decode = decodeURIComponent; d.iso8859 = function () { d.encode = escape; d.decode = unescape }; d.unicode = function () {
        d.encode =
        w; d.decode = decodeURIComponent
    }; d.characters = {
        pathname: { encode: { expression: /%(24|26|2B|2C|3B|3D|3A|40)/ig, map: { "%24": "$", "%26": "&", "%2B": "+", "%2C": ",", "%3B": ";", "%3D": "=", "%3A": ":", "%40": "@" } }, decode: { expression: /[\/\?#]/g, map: { "/": "%2F", "?": "%3F", "#": "%23" } } }, reserved: {
            encode: {
                expression: /%(21|23|24|26|27|28|29|2A|2B|2C|2F|3A|3B|3D|3F|40|5B|5D)/ig, map: {
                    "%3A": ":", "%2F": "/", "%3F": "?", "%23": "#", "%5B": "[", "%5D": "]", "%40": "@", "%21": "!", "%24": "$", "%26": "&", "%27": "'", "%28": "(", "%29": ")", "%2A": "*", "%2B": "+",
                    "%2C": ",", "%3B": ";", "%3D": "="
                }
            }
        }
    }; d.encodeQuery = function (a) { return d.encode(a + "").replace(/%20/g, "+") }; d.decodeQuery = function (a) { return d.decode((a + "").replace(/\+/g, "%20")) }; d.recodePath = function (a) { a = (a + "").split("/"); for (var b = 0, c = a.length; b < c; b++) a[b] = d.encodePathSegment(d.decode(a[b])); return a.join("/") }; d.decodePath = function (a) { a = (a + "").split("/"); for (var b = 0, c = a.length; b < c; b++) a[b] = d.decodePathSegment(a[b]); return a.join("/") }; var l = { encode: "encode", decode: "decode" }, j, r = function (a, b) {
        return function (c) {
            return d[b](c +
            "").replace(d.characters[a][b].expression, function (c) { return d.characters[a][b].map[c] })
        }
    }; for (j in l) d[j + "PathSegment"] = r("pathname", l[j]); d.encodeReserved = r("reserved", "encode"); d.parse = function (a, b) {
        var c; b || (b = {}); c = a.indexOf("#"); -1 < c && (b.fragment = a.substring(c + 1) || null, a = a.substring(0, c)); c = a.indexOf("?"); -1 < c && (b.query = a.substring(c + 1) || null, a = a.substring(0, c)); "//" === a.substring(0, 2) ? (b.protocol = "", a = a.substring(2), a = d.parseAuthority(a, b)) : (c = a.indexOf(":"), -1 < c && (b.protocol = a.substring(0,
        c), b.protocol && !b.protocol.match(d.protocol_expression) ? b.protocol = void 0 : "file" === b.protocol ? a = a.substring(c + 3) : "//" === a.substring(c + 1, c + 3) ? (a = a.substring(c + 3), a = d.parseAuthority(a, b)) : (a = a.substring(c + 1), b.urn = !0))); b.path = a; return b
    }; d.parseHost = function (a, b) {
        var c = a.indexOf("/"), e; -1 === c && (c = a.length); "[" === a.charAt(0) ? (e = a.indexOf("]"), b.hostname = a.substring(1, e) || null, b.port = a.substring(e + 2, c) || null) : a.indexOf(":") !== a.lastIndexOf(":") ? (b.hostname = a.substring(0, c) || null, b.port = null) : (e = a.substring(0,
        c).split(":"), b.hostname = e[0] || null, b.port = e[1] || null); b.hostname && "/" !== a.substring(c).charAt(0) && (c++, a = "/" + a); return a.substring(c) || "/"
    }; d.parseAuthority = function (a, b) { a = d.parseUserinfo(a, b); return d.parseHost(a, b) }; d.parseUserinfo = function (a, b) { var c = a.indexOf("@"), e = a.indexOf("/"); -1 < c && (-1 === e || c < e) ? (e = a.substring(0, c).split(":"), b.username = e[0] ? d.decode(e[0]) : null, e.shift(), b.password = e[0] ? d.decode(e.join(":")) : null, a = a.substring(c + 1)) : (b.username = null, b.password = null); return a }; d.parseQuery =
    function (a) { if (!a) return {}; a = a.replace(/&+/g, "&").replace(/^\?*&*|&+$/g, ""); if (!a) return {}; var b = {}; a = a.split("&"); for (var c = a.length, e, g, f = 0; f < c; f++) e = a[f].split("="), g = d.decodeQuery(e.shift()), e = e.length ? d.decodeQuery(e.join("=")) : null, b[g] ? ("string" === typeof b[g] && (b[g] = [b[g]]), b[g].push(e)) : b[g] = e; return b }; d.build = function (a) {
        var b = ""; a.protocol && (b += a.protocol + ":"); if (!a.urn && (b || a.hostname)) b += "//"; b += d.buildAuthority(a) || ""; "string" === typeof a.path && ("/" !== a.path.charAt(0) && "string" === typeof a.hostname &&
        (b += "/"), b += a.path); "string" === typeof a.query && a.query && (b += "?" + a.query); "string" === typeof a.fragment && a.fragment && (b += "#" + a.fragment); return b
    }; d.buildHost = function (a) { var b = ""; if (a.hostname) d.ip6_expression.test(a.hostname) ? b = a.port ? b + ("[" + a.hostname + "]:" + a.port) : b + a.hostname : (b += a.hostname, a.port && (b += ":" + a.port)); else return ""; return b }; d.buildAuthority = function (a) { return d.buildUserinfo(a) + d.buildHost(a) }; d.buildUserinfo = function (a) {
        var b = ""; a.username && (b += d.encode(a.username), a.password && (b +=
        ":" + d.encode(a.password)), b += "@"); return b
    }; d.buildQuery = function (a, b) { var c = "", e, g, f, n; for (g in a) if (q.call(a, g) && g) if (k(a[g])) { e = {}; f = 0; for (n = a[g].length; f < n; f++) void 0 !== a[g][f] && void 0 === e[a[g][f] + ""] && (c += "&" + d.buildQueryParameter(g, a[g][f]), !0 !== b && (e[a[g][f] + ""] = !0)) } else void 0 !== a[g] && (c += "&" + d.buildQueryParameter(g, a[g])); return c.substring(1) }; d.buildQueryParameter = function (a, b) { return d.encodeQuery(a) + (null !== b ? "=" + d.encodeQuery(b) : "") }; d.addQuery = function (a, b, c) {
        if ("object" === typeof b) for (var e in b) q.call(b,
        e) && d.addQuery(a, e, b[e]); else if ("string" === typeof b) void 0 === a[b] ? a[b] = c : ("string" === typeof a[b] && (a[b] = [a[b]]), k(c) || (c = [c]), a[b] = a[b].concat(c)); else throw new TypeError("URI.addQuery() accepts an object, string as the name parameter");
    }; d.removeQuery = function (a, b, c) {
        var e; if (k(b)) { c = 0; for (e = b.length; c < e; c++) a[b[c]] = void 0 } else if ("object" === typeof b) for (e in b) q.call(b, e) && d.removeQuery(a, e, b[e]); else if ("string" === typeof b) if (void 0 !== c) if (a[b] === c) a[b] = void 0; else {
            if (k(a[b])) {
                e = a[b]; var g = {},
                f, n; if (k(c)) { f = 0; for (n = c.length; f < n; f++) g[c[f]] = !0 } else g[c] = !0; f = 0; for (n = e.length; f < n; f++) void 0 !== g[e[f]] && (e.splice(f, 1), n--, f--); a[b] = e
            }
        } else a[b] = void 0; else throw new TypeError("URI.addQuery() accepts an object, string as the first parameter");
    }; d.hasQuery = function (a, b, c, e) {
        if ("object" === typeof b) { for (var g in b) if (q.call(b, g) && !d.hasQuery(a, g, b[g])) return !1; return !0 } if ("string" !== typeof b) throw new TypeError("URI.hasQuery() accepts an object, string as the name parameter"); switch (v(c)) {
            case "Undefined": return b in
            a; case "Boolean": return a = Boolean(k(a[b]) ? a[b].length : a[b]), c === a; case "Function": return !!c(a[b], b, a); case "Array": return !k(a[b]) ? !1 : (e ? u : x)(a[b], c); case "RegExp": return !k(a[b]) ? Boolean(a[b] && a[b].match(c)) : !e ? !1 : u(a[b], c); case "Number": c = String(c); case "String": return !k(a[b]) ? a[b] === c : !e ? !1 : u(a[b], c); default: throw new TypeError("URI.hasQuery() accepts undefined, boolean, string, number, RegExp, Function as the value parameter");
        }
    }; d.commonPath = function (a, b) {
        var c = Math.min(a.length, b.length), e; for (e =
        0; e < c; e++) if (a.charAt(e) !== b.charAt(e)) { e--; break } if (1 > e) return a.charAt(0) === b.charAt(0) && "/" === a.charAt(0) ? "/" : ""; "/" !== a.charAt(e) && (e = a.substring(0, e).lastIndexOf("/")); return a.substring(0, e + 1)
    }; d.withinString = function (a, b) { return a.replace(d.find_uri_expression, b) }; d.ensureValidHostname = function (a) {
        if (a.match(d.invalid_hostname_characters)) {
            if (!m) throw new TypeError("Hostname '" + a + "' contains characters other than [A-Z0-9.-] and Punycode.js is not available"); if (m.toASCII(a).match(d.invalid_hostname_characters)) throw new TypeError("Hostname '" +
            a + "' contains characters other than [A-Z0-9.-]");
        }
    }; f.build = function (a) { if (!0 === a) this._deferred_build = !0; else if (void 0 === a || this._deferred_build) this._string = d.build(this._parts), this._deferred_build = !1; return this }; f.clone = function () { return new d(this) }; f.valueOf = f.toString = function () { return this.build(!1)._string }; l = { protocol: "protocol", username: "username", password: "password", hostname: "hostname", port: "port" }; r = function (a) {
        return function (b, c) {
            if (void 0 === b) return this._parts[a] || ""; this._parts[a] =
            b; this.build(!c); return this
        }
    }; for (j in l) f[j] = r(l[j]); l = { query: "?", fragment: "#" }; r = function (a, b) { return function (c, e) { if (void 0 === c) return this._parts[a] || ""; null !== c && (c += "", c.charAt(0) === b && (c = c.substring(1))); this._parts[a] = c; this.build(!e); return this } }; for (j in l) f[j] = r(j, l[j]); l = { search: ["?", "query"], hash: ["#", "fragment"] }; r = function (a, b) { return function (c, e) { var d = this[a](c, e); return "string" === typeof d && d.length ? b + d : d } }; for (j in l) f[j] = r(l[j][1], l[j][0]); f.pathname = function (a, b) {
        if (void 0 ===
        a || !0 === a) { var c = this._parts.path || (this._parts.urn ? "" : "/"); return a ? d.decodePath(c) : c } this._parts.path = a ? d.recodePath(a) : "/"; this.build(!b); return this
    }; f.path = f.pathname; f.href = function (a, b) {
        var c; if (void 0 === a) return this.toString(); this._string = ""; this._parts = d._parts(); var e = a instanceof d, f = "object" === typeof a && (a.hostname || a.path); !e && (f && void 0 !== a.pathname) && (a = a.toString()); if ("string" === typeof a) this._parts = d.parse(a, this._parts); else if (e || f) for (c in e = e ? a._parts : a, e) q.call(this._parts,
        c) && (this._parts[c] = e[c]); else throw new TypeError("invalid input"); this.build(!b); return this
    }; f.is = function (a) {
        var b = !1, c = !1, e = !1, f = !1, h = !1, n = !1, k = !1, j = !this._parts.urn; this._parts.hostname && (j = !1, c = d.ip4_expression.test(this._parts.hostname), e = d.ip6_expression.test(this._parts.hostname), b = c || e, h = (f = !b) && s && s.has(this._parts.hostname), n = f && d.idn_expression.test(this._parts.hostname), k = f && d.punycode_expression.test(this._parts.hostname)); switch (a.toLowerCase()) {
            case "relative": return j; case "absolute": return !j;
            case "domain": case "name": return f; case "sld": return h; case "ip": return b; case "ip4": case "ipv4": case "inet4": return c; case "ip6": case "ipv6": case "inet6": return e; case "idn": return n; case "url": return !this._parts.urn; case "urn": return !!this._parts.urn; case "punycode": return k
        } return null
    }; var y = f.protocol, z = f.port, A = f.hostname; f.protocol = function (a, b) {
        if (void 0 !== a && a && (a = a.replace(/:(\/\/)?$/, ""), a.match(/[^a-zA-z0-9\.+-]/))) throw new TypeError("Protocol '" + a + "' contains characters other than [A-Z0-9.+-]");
        return y.call(this, a, b)
    }; f.scheme = f.protocol; f.port = function (a, b) { if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 !== a && (0 === a && (a = null), a && (a += "", ":" === a.charAt(0) && (a = a.substring(1)), a.match(/[^0-9]/)))) throw new TypeError("Port '" + a + "' contains characters other than [0-9]"); return z.call(this, a, b) }; f.hostname = function (a, b) { if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 !== a) { var c = {}; d.parseHost(a, c); a = c.hostname } return A.call(this, a, b) }; f.host = function (a, b) {
        if (this._parts.urn) return void 0 ===
        a ? "" : this; if (void 0 === a) return this._parts.hostname ? d.buildHost(this._parts) : ""; d.parseHost(a, this._parts); this.build(!b); return this
    }; f.authority = function (a, b) { if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a) return this._parts.hostname ? d.buildAuthority(this._parts) : ""; d.parseAuthority(a, this._parts); this.build(!b); return this }; f.userinfo = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a) {
            if (!this._parts.username) return ""; var c = d.buildUserinfo(this._parts); return c.substring(0,
            c.length - 1)
        } "@" !== a[a.length - 1] && (a += "@"); d.parseUserinfo(a, this._parts); this.build(!b); return this
    }; f.resource = function (a, b) { var c; if (void 0 === a) return this.path() + this.search() + this.hash(); c = d.parse(a); this._parts.path = c.path; this._parts.query = c.query; this._parts.fragment = c.fragment; this.build(!b); return this }; f.subdomain = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a) {
            if (!this._parts.hostname || this.is("IP")) return ""; var c = this._parts.hostname.length - this.domain().length -
            1; return this._parts.hostname.substring(0, c) || ""
        } c = this._parts.hostname.length - this.domain().length; c = this._parts.hostname.substring(0, c); c = RegExp("^" + p(c)); a && "." !== a.charAt(a.length - 1) && (a += "."); a && d.ensureValidHostname(a); this._parts.hostname = this._parts.hostname.replace(c, a); this.build(!b); return this
    }; f.domain = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; "boolean" === typeof a && (b = a, a = void 0); if (void 0 === a) {
            if (!this._parts.hostname || this.is("IP")) return ""; var c = this._parts.hostname.match(/\./g);
            if (c && 2 > c.length) return this._parts.hostname; c = this._parts.hostname.length - this.tld(b).length - 1; c = this._parts.hostname.lastIndexOf(".", c - 1) + 1; return this._parts.hostname.substring(c) || ""
        } if (!a) throw new TypeError("cannot set domain empty"); d.ensureValidHostname(a); !this._parts.hostname || this.is("IP") ? this._parts.hostname = a : (c = RegExp(p(this.domain()) + "$"), this._parts.hostname = this._parts.hostname.replace(c, a)); this.build(!b); return this
    }; f.tld = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" :
        this; "boolean" === typeof a && (b = a, a = void 0); if (void 0 === a) { if (!this._parts.hostname || this.is("IP")) return ""; var c = this._parts.hostname.lastIndexOf("."), c = this._parts.hostname.substring(c + 1); return !0 !== b && s && s.list[c.toLowerCase()] ? s.get(this._parts.hostname) || c : c } if (a) if (a.match(/[^a-zA-Z0-9-]/)) if (s && s.is(a)) c = RegExp(p(this.tld()) + "$"), this._parts.hostname = this._parts.hostname.replace(c, a); else throw new TypeError("TLD '" + a + "' contains characters other than [A-Z0-9]"); else {
            if (!this._parts.hostname ||
            this.is("IP")) throw new ReferenceError("cannot set TLD on non-domain host"); c = RegExp(p(this.tld()) + "$"); this._parts.hostname = this._parts.hostname.replace(c, a)
        } else throw new TypeError("cannot set TLD empty"); this.build(!b); return this
    }; f.directory = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a || !0 === a) {
            if (!this._parts.path && !this._parts.hostname) return ""; if ("/" === this._parts.path) return "/"; var c = this._parts.path.length - this.filename().length - 1, c = this._parts.path.substring(0,
            c) || (this._parts.hostname ? "/" : ""); return a ? d.decodePath(c) : c
        } c = this._parts.path.length - this.filename().length; c = this._parts.path.substring(0, c); c = RegExp("^" + p(c)); this.is("relative") || (a || (a = "/"), "/" !== a.charAt(0) && (a = "/" + a)); a && "/" !== a.charAt(a.length - 1) && (a += "/"); a = d.recodePath(a); this._parts.path = this._parts.path.replace(c, a); this.build(!b); return this
    }; f.filename = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a || !0 === a) {
            if (!this._parts.path || "/" === this._parts.path) return "";
            var c = this._parts.path.lastIndexOf("/"), c = this._parts.path.substring(c + 1); return a ? d.decodePathSegment(c) : c
        } c = !1; "/" === a.charAt(0) && (a = a.substring(1)); a.match(/\.?\//) && (c = !0); var e = RegExp(p(this.filename()) + "$"); a = d.recodePath(a); this._parts.path = this._parts.path.replace(e, a); c ? this.normalizePath(b) : this.build(!b); return this
    }; f.suffix = function (a, b) {
        if (this._parts.urn) return void 0 === a ? "" : this; if (void 0 === a || !0 === a) {
            if (!this._parts.path || "/" === this._parts.path) return ""; var c = this.filename(), e = c.lastIndexOf(".");
            if (-1 === e) return ""; c = c.substring(e + 1); c = /^[a-z0-9%]+$/i.test(c) ? c : ""; return a ? d.decodePathSegment(c) : c
        } "." === a.charAt(0) && (a = a.substring(1)); if (c = this.suffix()) e = a ? RegExp(p(c) + "$") : RegExp(p("." + c) + "$"); else { if (!a) return this; this._parts.path += "." + d.recodePath(a) } e && (a = d.recodePath(a), this._parts.path = this._parts.path.replace(e, a)); this.build(!b); return this
    }; f.segment = function (a, b, c) {
        var e = this._parts.urn ? ":" : "/", d = this.path(), f = "/" === d.substring(0, 1), d = d.split(e); "number" !== typeof a && (c = b, b = a, a =
        void 0); if (void 0 !== a && "number" !== typeof a) throw Error("Bad segment '" + a + "', must be 0-based integer"); f && d.shift(); 0 > a && (a = Math.max(d.length + a, 0)); if (void 0 === b) return void 0 === a ? d : d[a]; if (null === a || void 0 === d[a]) if (k(b)) d = b; else { if (b || "string" === typeof b && b.length) "" === d[d.length - 1] ? d[d.length - 1] = b : d.push(b) } else b || "string" === typeof b && b.length ? d[a] = b : d.splice(a, 1); f && d.unshift(""); return this.path(d.join(e), c)
    }; var B = f.query; f.query = function (a, b) {
        if (!0 === a) return d.parseQuery(this._parts.query);
        if ("function" === typeof a) { var c = d.parseQuery(this._parts.query), e = a.call(this, c); this._parts.query = d.buildQuery(e || c, this._parts.duplicateQueryParameters); this.build(!b); return this } return void 0 !== a && "string" !== typeof a ? (this._parts.query = d.buildQuery(a, this._parts.duplicateQueryParameters), this.build(!b), this) : B.call(this, a, b)
    }; f.setQuery = function (a, b, c) {
        var e = d.parseQuery(this._parts.query); if ("object" === typeof a) for (var f in a) q.call(a, f) && (e[f] = a[f]); else if ("string" === typeof a) e[a] = void 0 !==
        b ? b : null; else throw new TypeError("URI.addQuery() accepts an object, string as the name parameter"); this._parts.query = d.buildQuery(e, this._parts.duplicateQueryParameters); "string" !== typeof a && (c = b); this.build(!c); return this
    }; f.addQuery = function (a, b, c) { var e = d.parseQuery(this._parts.query); d.addQuery(e, a, void 0 === b ? null : b); this._parts.query = d.buildQuery(e, this._parts.duplicateQueryParameters); "string" !== typeof a && (c = b); this.build(!c); return this }; f.removeQuery = function (a, b, c) {
        var e = d.parseQuery(this._parts.query);
        d.removeQuery(e, a, b); this._parts.query = d.buildQuery(e, this._parts.duplicateQueryParameters); "string" !== typeof a && (c = b); this.build(!c); return this
    }; f.hasQuery = function (a, b, c) { var e = d.parseQuery(this._parts.query); return d.hasQuery(e, a, b, c) }; f.setSearch = f.setQuery; f.addSearch = f.addQuery; f.removeSearch = f.removeQuery; f.hasSearch = f.hasQuery; f.normalize = function () { return this._parts.urn ? this.normalizeProtocol(!1).normalizeQuery(!1).normalizeFragment(!1).build() : this.normalizeProtocol(!1).normalizeHostname(!1).normalizePort(!1).normalizePath(!1).normalizeQuery(!1).normalizeFragment(!1).build() };
    f.normalizeProtocol = function (a) { "string" === typeof this._parts.protocol && (this._parts.protocol = this._parts.protocol.toLowerCase(), this.build(!a)); return this }; f.normalizeHostname = function (a) { this._parts.hostname && (this.is("IDN") && m ? this._parts.hostname = m.toASCII(this._parts.hostname) : this.is("IPv6") && t && (this._parts.hostname = t.best(this._parts.hostname)), this._parts.hostname = this._parts.hostname.toLowerCase(), this.build(!a)); return this }; f.normalizePort = function (a) {
        "string" === typeof this._parts.protocol &&
        this._parts.port === d.defaultPorts[this._parts.protocol] && (this._parts.port = null, this.build(!a)); return this
    }; f.normalizePath = function (a) {
        if (this._parts.urn || !this._parts.path || "/" === this._parts.path) return this; var b, c, e = this._parts.path, f, h; "/" !== e.charAt(0) && ("." === e.charAt(0) && (c = e.substring(0, e.indexOf("/"))), b = !0, e = "/" + e); for (e = e.replace(/(\/(\.\/)+)|\/{2,}/g, "/") ; ;) {
            f = e.indexOf("/../"); if (-1 === f) break; else if (0 === f) { e = e.substring(3); break } h = e.substring(0, f).lastIndexOf("/"); -1 === h && (h = f); e =
            e.substring(0, h) + e.substring(f + 3)
        } b && this.is("relative") && (e = c ? c + e : e.substring(1)); e = d.recodePath(e); this._parts.path = e; this.build(!a); return this
    }; f.normalizePathname = f.normalizePath; f.normalizeQuery = function (a) { "string" === typeof this._parts.query && (this._parts.query.length ? this.query(d.parseQuery(this._parts.query)) : this._parts.query = null, this.build(!a)); return this }; f.normalizeFragment = function (a) { this._parts.fragment || (this._parts.fragment = null, this.build(!a)); return this }; f.normalizeSearch = f.normalizeQuery;
    f.normalizeHash = f.normalizeFragment; f.iso8859 = function () { var a = d.encode, b = d.decode; d.encode = escape; d.decode = decodeURIComponent; this.normalize(); d.encode = a; d.decode = b; return this }; f.unicode = function () { var a = d.encode, b = d.decode; d.encode = w; d.decode = unescape; this.normalize(); d.encode = a; d.decode = b; return this }; f.readable = function () {
        var a = this.clone(); a.username("").password("").normalize(); var b = ""; a._parts.protocol && (b += a._parts.protocol + "://"); a._parts.hostname && (a.is("punycode") && m ? (b += m.toUnicode(a._parts.hostname),
        a._parts.port && (b += ":" + a._parts.port)) : b += a.host()); a._parts.hostname && (a._parts.path && "/" !== a._parts.path.charAt(0)) && (b += "/"); b += a.path(!0); if (a._parts.query) { for (var c = "", e = 0, f = a._parts.query.split("&"), h = f.length; e < h; e++) { var j = (f[e] || "").split("="), c = c + ("&" + d.decodeQuery(j[0]).replace(/&/g, "%26")); void 0 !== j[1] && (c += "=" + d.decodeQuery(j[1]).replace(/&/g, "%26")) } b += "?" + c.substring(1) } return b += a.hash()
    }; f.absoluteTo = function (a) {
        var b = this.clone(), c = ["protocol", "username", "password", "hostname",
        "port"], e, f; if (this._parts.urn) throw Error("URNs do not have any generally defined hierachical components"); a instanceof d || (a = new d(a)); b._parts.protocol || (b._parts.protocol = a._parts.protocol); if (this._parts.hostname) return b; e = 0; for (f; f = c[e]; e++) b._parts[f] = a._parts[f]; c = ["query", "path"]; e = 0; for (f; f = c[e]; e++) !b._parts[f] && a._parts[f] && (b._parts[f] = a._parts[f]); "/" !== b.path().charAt(0) && (a = a.directory(), b._parts.path = (a ? a + "/" : "") + b._parts.path, b.normalizePath()); b.build(); return b
    }; f.relativeTo =
    function (a) {
        var b = this.clone(), c = ["protocol", "username", "password", "hostname", "port"], e; if (this._parts.urn) throw Error("URNs do not have any generally defined hierachical components"); a instanceof d || (a = new d(a)); if ("/" !== this.path().charAt(0) || "/" !== a.path().charAt(0)) throw Error("Cannot calculate common path from non-relative URLs"); e = d.commonPath(b.path(), a.path()); if (!e || "/" === e) return b; for (var f = 0, h; h = c[f]; f++) b._parts[h] = null; a = a.directory(); c = this.directory(); if (a === c) return b._parts.path =
        "./" + b.filename(), b.build(); a.substring(e.length); c = c.substring(e.length); if (a + "/" === e) return c && (c += "/"), b._parts.path = "./" + c + b.filename(), b.build(); c = "../"; e = RegExp("^" + p(e)); for (a = a.replace(e, "/").match(/\//g).length - 1; a--;) c += "../"; b._parts.path = b._parts.path.replace(e, c); return b.build()
    }; f.equals = function (a) {
        var b = this.clone(); a = new d(a); var c = {}, e = {}, f = {}, h; b.normalize(); a.normalize(); if (b.toString() === a.toString()) return !0; c = b.query(); e = a.query(); b.query(""); a.query(""); if (b.toString() !==
        a.toString() || c.length !== e.length) return !1; c = d.parseQuery(c); e = d.parseQuery(e); for (h in c) if (q.call(c, h)) { if (k(c[h])) { if (!x(c[h], e[h])) return !1 } else if (c[h] !== e[h]) return !1; f[h] = !0 } for (h in e) if (q.call(e, h) && !f[h]) return !1; return !0
    }; f.duplicateQueryParameters = function (a) { this._parts.duplicateQueryParameters = !!a; return this }; return d
});
