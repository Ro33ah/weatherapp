<template>
  <div class="home">
    <WeatherInfo v-bind:testprop="options"/>
  </div>
</template>

<script>
// @ is an alias to /src
import WeatherInfo from "@/components/WeatherInfo.vue";

export default {
  name: "home",
  components: {
    WeatherInfo
  },
   data() {
    return {
      selected: null,
      options: [ 
        { value: null, text: 'Choose a city', disabled: true},
        { text: 'Leipzig', value: '2879139'},
        { text: 'Erfurt', value: '2929670'},
        { text: 'Berlin', value: '2950158'},
        { text: 'Hamburg',value: '2911298'}
      ]
    }
  },
};
</script>
