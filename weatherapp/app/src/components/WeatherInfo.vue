<template>
  <div class="autocomplete" @click="ClickOutEvent()">
    <img alt="Weather Image" src="../assets/weather.png" />
    <br>
    <input class="input-form" v-model="searchString" placeholder="Enter city name" @input="GetHistory()"
      @keydown.down="OnKeyDown" @keydown.up="OnKeyUp" @keydown.enter="OnKeyEnter" >
      <button class="slay-button" @click="GetWeather(searchString)"> submit</button>
      <ul class="autocomplete-results" v-show="isOpen" >
        <li class="autocomplete-result" 
          :class="{ 'is-active': i === keyCounter }" 
          v-for="(city, i) in cities" :key="i"
          @click="SelectHistory(city)">
          {{city}}
        </li>
      </ul>
          
      <br>
    <canvas ref="forecastChart" class="forecast-chart" v-show="chart != null"></canvas>
  </div>
</template>

<script>
import Chart from "chart.js";
import {Line} from "vue-chartjs";
import {APIService} from '../APIService';
import { error } from 'util';
import { constants } from 'crypto';
import { compileFunction } from 'vm';


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
      chart: null,
      dates: null,
      temps: null,
      humidities: null,
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
      this.searchString = this.cities[this.keyCounter];
    },

    OnKeyDown(){
      if (this.keyCounter < this.cities.length - 1){
        this.keyCounter = this.keyCounter + 1;
      }
      this.searchString = this.cities[this.keyCounter];
    },

    OnKeyEnter(){
      this.GetWeather(this.searchString);
      this.isOpen = false;
    },

     ClickOutEvent(event) {
         this.isOpen = false;
         this.keyCounter = 0;
     },

    GetWeather(searchString){
        apiService.GetWeather(searchString)
          .then(response=> {
            this.dates = response.list.map(list => list.newDate);
            this.temps = response.list.map( list => list.main.temp);
            this.humidities = response.list.map( list => list.main.humidity);

            let data = {
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
              };
          
            if(this.chart) {
              this.chart.data = data;
              this.chart.update();
            } else {
              
              let chartConfig = {
                type: "line",
                data: data,
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
              };

              let ctx = this.$refs.forecastChart.getContext('2d');
              this.chart = new Chart(ctx, chartConfig)
            }
      }).catch(error => {
         if (error.response.status === 404) {
          this.$router.push({name: 'NotFound'})
        }
      });
    }
    },
};
</script>

<style lang="scss" scoped>

$baseColor: #2e3b57;
$height: 30px;

.input-form{
  width: 40%;
  height: $height;
  margin: 20px;
}

.slay-button {
  height: $height;
  width: 8%;
  margin: 10px;
  background-color:#4AAE9B;
}
.forecast-chart {
  background: $baseColor;
  border-radius: 15px;
  margin:  ($height - 5px) 0;

  &:hover {
    background: rgb(196, 136, 25);
  }
}


.autocomplete {
    font-size: 15px;
    position: relative;
    top:30px;

    
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

    &.is-active, &:hover {
      background-color: #4AAE9B;
      color: white;
    }
  }
}
</style>
