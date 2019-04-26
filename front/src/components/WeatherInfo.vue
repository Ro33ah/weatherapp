<template>
  <p class="homeText">
      <b-form-input id="inputBar" v-model="input" placeholder="Enter city name" @change="getWeather($event)"></b-form-input>
      <br>
      <canvas id="forecastChart" v-show="isCanvasNull"></canvas>
  </p>
</template>

<script>
import Chart from "chart.js";
import {Line} from "vue-chartjs";
import {APIService} from '../APIService';
//const API_URL = 'https://localhost:5001';
const apiService = new APIService();

export default {
  name: 'Home',
  props: ['value'],
  data(){
    return{
      input: '',
     isCanvasNull: null,

    }
  },


  methods:{
    getWeather(event){
      // if (document.getElementById("inputBar") == null){
      //   this.isCanvasNull = null;
      // };
      apiService.getWeather(event).then((data)=> {
        this.dates = data.list.map(list => {
          return list.newDate;
        });
        this.temps = data.list.map( list => {
          return list.main.temp;
        });
        this.humidities = data.list.map( list => {
          return list.main.humidity;
        });
        var ctx = document.getElementById("forecastChart").getContext('2d');
        this.chart = new Chart(ctx, {
        type: "line",
        data: {
          isCanvasNull: '',
          labels: this.dates,
          datasets: [
            {
              data: this.temps,
              label: "Av.Temp (F)",
              //backgroundColor: ["rgba(54, 162, 235, 0.5)"],
              borderColor: ["rgb(54, 162, 235)"],
              borderWidth: 3,
            },
            {
              data: this.humidities,
              label: "Humidity (%)",
              //backgroundColor: ["rgba(71, 183,132,0.5)"],
              borderColor: ["rgb(71, 183,132,.5)"],
              borderWidth: 3,
            }
          ]
        },
        options: {
          lineTension: 6,
          responsive: true,
          title: {
            display: true,
            fontSize: 30,
            fontColor: '#FA8072',
            text: "5-day Temperature & Humidity"
          },
          scales: {
            yAxes: [{
              ticks: {
                beginAtZero: true
              }
            }],
            xAxes: [{
              ticks:{
                beginAtZero: true
              }
            }]
          }
        }
      })
      this.isCanvasNull = this.canvas === null? false : true;
      });
    
    } 
    },
    mounted() {
      //this.isCanvasNull = this.canvas === null? false : true;
      //this.getWeather();
    }
};
</script>

<style scoped>

.homeText{
    font-size: 35px;
    color: black;
    text-align: center;
    position: relative;
    top:30px;
    text-shadow: 2px 2px 2px gray;
}
.header{
  font-size: 25px;
    color: black;
    text-align: center;
    position: relative;
    text-shadow: 2px 2px 2px gray;
}
.col-sm-4{
  display: inline-block;
}
#forecastChart{
  background: #212733;
  border-radius: 15px;
  /* box-shadow: 0px 2px 15px rgba(25, 25, 25, 0.27); */
  margin:  25px 0;
}
</style>
