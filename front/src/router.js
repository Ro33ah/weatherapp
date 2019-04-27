import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
//import NotFound from "./views/NotFound.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/NotFound",
      name: "NotFound",
      // route level code-splitting
      // this generates a separate chunk (NotFound.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () =>
        import(/* webpackChunkName: "NotFound" */ "./views/NotFound.vue")
    },
    {
      name: "NotFound",
      path: "/views/NotFound",
      alias: "*",
      component: require("./views/NotFound.vue")
    }
  ]
});
