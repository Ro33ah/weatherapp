<template>
  <div class="autocomplete">
    <img alt="Weather Image" src="../assets/weather.png" />
    <br>
    <input class="input-form" v-model="searchString" placeholder="Enter city name" @input="GetHistory()"
      @keydown.down="OnKeyDown" @keydown.up="OnKeyUp" @keydown.enter="OnKeyEnter">
      <button class="slay-button" @click="GetWeather(searchString)"> submit</button>
      <ul class="autocomplete-results" v-show="isOpen">
        <li class="autocomplete-result" :class="{ 'is-active': i === keyCounter }" v-for="(city, i) in cities" :key="i" @click="SelectHistory(city)"> {{city}}
        </li>
      </ul>
          
      <br>
    <canvas id="forecast-chart" v-show="isCanvasNull"></canvas>
  </div>
</template>

<script>
import Chart from "chart.js";
import {Line} from "vue-chartjs";
import {APIService} from '../APIService';
import { error } from 'util';
import { constants } from 'crypto';
import { compileFunction } from 'vm';
//const API_URL = 'https://localhost:5001';
const apiService = new APIService();

export default {
  name: 'Home',
  data(){
    return{
      searchString: "",
      cities: [],
      isCanvasNull: null,
      isOpen: false,
      keyCounter: 0,
    }
  },

  methods:{
    GetHistory() {
      this.isOpen = true;
      apiService.GetHistory().then(data => {
        this.cities = data.map(city => city.cityName);
      })
    },

    SelectHistory(city){
      this.searchString = city;
      this.isOpen = false;
    },

    OnKeyUp(){
      if (this.keyCounter  > 0){
        this.keyCounter = this.keyCounter - 1;
      }
    },

    OnKeyDown(){
      if (this.keyCounter < this.cities.length){
        this.keyCounter = this.keyCounter + 1;
      }
    },
    OnKeyEnter(){
      this.searchString = this.cities[this.keyCounter];
      this.isOpen = false;
      this.keyCounter = -1;
    },

    ClickOutEvent(event) {
      if (!this.$el.contains(event.target)) {
        this.isOpen = false;
        this.arrowCounter = -1;
      }
    },

    GetWeather(searchString){
        apiService.GetWeather(searchString).then((data)=> {
        this.dates = data.list.map(list => {
          return list.newDate;
        });
        this.temps = data.list.map( list => {
          return list.main.temp;
        });
        this.humidities = data.list.map( list => {
          return list.main.humidity;
        });
       
        var ctx = document.getElementById("forecast-chart").getContext('2d');
        this.chart = new Chart(ctx, {
        type: "line",
        data: {
          isCanvasNull: '',
          labels: this.dates,
          datasets: [
            {
              data: this.temps,
              label: "Av.Temp (F)",
              borderColor: ["rgb(54, 162, 235)"],
              borderWidth: 3,
            },
            {
              data: this.humidities,
              label: "Humidity (%)",
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
      }).catch(error => {
         if (error.response.status === 404) {
          this.$router.push({name: 'NotFound'})
        }
      });
    }
    },
    mounted() {
      document.addEventListener('click', this.ClickOutEvent);
      //this.isCanvasNull = this.canvas === null? false : true;
      //this.GetWeather();
    },
    destroyed() {
      document.removeEventListener('click', this.ClickOutEvent);
    }
};
</script>

<style scoped>
.input-form{
  width: 40%;
  height: 30px;
  margin: 20px;
}

.slay-button{
  height: 30px;
  width: 8%;
  margin: 10px;
  background-color:#4AAE9B;
}
#forecast-chart{
  background: #212733;
  border-radius: 15px;
  margin:  25px 0;
}
.autocomplete{
    font-size: 15px;
    position: relative;
    top:30px;
}
  .autocomplete-results {
    padding: 0;
    margin: 10px;
    border: 1px solid #eeeeee;
    height: 120px;
    overflow: auto;
  }
  
  .autocomplete-result {
    list-style: none;
    text-align: left;
    padding: 4px 2px;
    cursor: pointer;
  }
  .autocomplete-result.is-active,
  .autocomplete-result:hover {
    background-color: #4AAE9B;
    color: white;
  }
</style>
