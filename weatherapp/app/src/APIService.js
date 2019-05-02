import axios from "axios";

export class APIService {
  GetWeather(searchString) {
    const url = `/api/forecast/${searchString}/`;
    return axios.get(url).then(response => response.data);
  }
  GetHistory() {
    const historyUrl = `/api/history/`;
    return axios.get(historyUrl).then(response => response.data);
  }
}
