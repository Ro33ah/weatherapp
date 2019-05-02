<template>
  <div class="home">
    <WeatherInfo/>
  </div>
</template>

<script>
// @ is an alias to /src
import WeatherInfo from "@/components/WeatherInfo.vue";

export default {
  name: "home",
  components: {
    WeatherInfo
  }
};
</script>
