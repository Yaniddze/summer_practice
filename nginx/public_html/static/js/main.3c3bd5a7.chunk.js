(this.webpackJsonpweb_app=this.webpackJsonpweb_app||[]).push([[0],{31:function(e,t,r){e.exports={title:"styles_title__2YosF"}},34:function(e,t,r){e.exports=r(62)},62:function(e,t,r){"use strict";r.r(t);var n=r(1),a=r.n(n),c=r(12),o=r.n(c),s=r(11),u=r(32),i=r(8),l=r(28),f=r(9),p={data:{results:[]},isFetching:!1,error:!1},h=Object(i.combineReducers)({starships:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:p,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case"STARSHIPS_START":return Object(f.a)(Object(f.a)({},e),{},{error:!1,isFetching:!0});case"STARSHIPS_FINISH":return Object(f.a)(Object(f.a)({},e),{},{error:!1,isFetching:!1});case"STARSHIPS_ERROR":return Object(f.a)(Object(f.a)({},e),{},{isFetching:!1,error:t.payload});case"STARSHIPS_FILL":return Object(f.a)(Object(f.a)({},e),{},{error:!1,isFetching:!1,data:Object(f.a)({},t.payload)});case"STARSHIPS_FETCH_ASYNC":return e;default:}return e}}),S=r(5),d=r.n(S),m=r(7);function b(){return{type:"STARSHIPS_START"}}function v(){return{type:"STARSHIPS_FINISH"}}function O(e){return{type:"STARSHIPS_FILL",payload:e}}function j(e){return{type:"STARSHIPS_ERROR",error:!0,payload:e}}var w=d.a.mark(g);function g(e){var t,r,n,a,c,o,s;return d.a.wrap((function(u){for(;;)switch(u.prev=u.next){case 0:return t=e.fetcher,r=e.start,n=e.fetcherParam,a=e.fill,c=e.finish,o=e.error,u.prev=1,u.next=4,Object(m.d)(r());case 4:return u.next=6,Object(m.b)(t,n);case 6:return s=u.sent,u.next=9,Object(m.d)(a(s));case 9:u.next=15;break;case 11:return u.prev=11,u.t0=u.catch(1),u.next=15,Object(m.d)(o(u.t0.message));case 15:return u.prev=15,u.next=18,Object(m.d)(c());case 18:return u.finish(15);case 19:case"end":return u.stop()}}),w,null,[[1,11,15,19]])}var A=r(29),E=r.n(A),F={starships:{fetch:function(){return E.a.get("".concat("http://gateway:8080","/weatherforecast")).then((function(e){return{results:e.data.map((function(e){return{name:e.summary}}))}}))}}},R=d.a.mark(y);function y(){var e;return d.a.wrap((function(t){for(;;)switch(t.prev=t.next){case 0:return e={fetcher:F.starships.fetch,start:b,finish:v,fill:O,error:j},t.next=3,g(e);case 3:case"end":return t.stop()}}),R)}var I=d.a.mark(_),T=d.a.mark(x);function _(){return d.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(m.e)("STARSHIPS_FETCH_ASYNC",y);case 2:case"end":return e.stop()}}),I)}function x(){return d.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(m.a)([Object(m.b)(_)]);case 2:case"end":return e.stop()}}),T)}var H=d.a.mark(k);function k(){return d.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(m.a)([Object(m.c)(x)]);case 2:case"end":return e.stop()}}),H)}var P=r(30),N=r(33),C=(Object(P.createLogger)({duration:!0,collapsed:!0,colors:{title:function(e){return e.error?"firebrick":"deepskyblue"},prevState:function(){return"#1C5FAF"},action:function(){return"#149945"},nextState:function(){return"#A47104"},error:function(){return"#ff0005"}}}),Object(N.a)()),L=[C];var Y=Object(i.createStore)(h,Object(l.composeWithDevTools)(i.applyMiddleware.apply(void 0,Object(u.a)(L))));C.run(k);var W=r(31),B=r.n(W),J=function(e){var t=e.children;return a.a.createElement(a.a.Fragment,null,a.a.createElement("h1",{className:B.a.title},t))},M=function(){var e,t=function(){var e=Object(s.b)(),t=Object(s.c)((function(e){return e.starships})),r=t.data,a=t.isFetching,c=t.error;return Object(n.useEffect)((function(){e({type:"STARSHIPS_FETCH_ASYNC"})}),[e]),{data:r,isFetching:a,error:c}}(),r=t.isFetching,c=t.data,o=t.error&&a.a.createElement("p",null,"Not found!"),u=r&&a.a.createElement("p",null,"Loading data from API..."),i=r||(null===(e=c.results)||void 0===e?void 0:e.map((function(e,t){var r=e.name;return a.a.createElement("li",{key:Number(t)},r)})));return a.a.createElement(a.a.Fragment,null,a.a.createElement(J,null,"Starships"),o,u,a.a.createElement("ul",null,i))};function D(){return a.a.createElement(s.a,{store:Y},a.a.createElement("div",{className:"App"},a.a.createElement(M,null)))}Boolean("localhost"===window.location.hostname||"[::1]"===window.location.hostname||window.location.hostname.match(/^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/));o.a.render(a.a.createElement(a.a.StrictMode,null,a.a.createElement(D,null)),document.getElementById("root")),"serviceWorker"in navigator&&navigator.serviceWorker.ready.then((function(e){e.unregister()})).catch((function(e){console.error(e.message)}))}},[[34,1,2]]]);
//# sourceMappingURL=main.3c3bd5a7.chunk.js.map