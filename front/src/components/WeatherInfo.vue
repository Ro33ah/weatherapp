<template>
  <div class="homeText">
    <br>
      <b-form-input list="test" @change="getWeather($event)"></b-form-input>
        <datalist id="test" >
          <option v-for="test in testprop" :key="test.text" > {{ test.text }}
          </option>
        </datalist>
    <br>
    <canvas id="forecastChart" style="display: block" v-show="isCanvasNull">
    </canvas>
  </div>
</template>

<script>
import {APIService} from '../APIService';
//const API_URL = 'https://localhost:5001';
const apiService = new APIService();
import Chart from "chart.js";
import {Line} from "vue-chartjs";

export default {
  name: 'Home',
  props: ['testprop'],
  // {
  //   value: String,
  //   text: String,
  //   msg: String,
  // },

  methods:{
    getWeather(event){
      apiService.getWeather(this.testprop.filter(x => x.text === event)[0].value).then((data)=> {
        this.dates = data.list.map(list => {
          return list.newDate;
        });
        this.temps = data.list.map( list => {
          return list.main.temp;
        });
        this.humidities = data.list.map( list => {
          return list.main.humidity;
        });
        console.log(this.dates);
        console.log(this.temps);
        console.log(this.humidities);
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
      });
    
    } 
    },
    mounted() {
// this.getWeather();
this.isCanvasNull = this.canvas === null? false : true;
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
  display: block;
  background: #212733;
  border-radius: 15px;
  /* box-shadow: 0px 2px 15px rgba(25, 25, 25, 0.27); */
  margin:  25px 0;
}
</style>
