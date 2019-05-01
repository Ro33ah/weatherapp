import axios from "axios";

const API_URL = "https://localhost:5001";

export class APIService {
  GetWeather(searchString) {
    const url = `${API_URL}/api/forecast/${searchString}/`;
    return axios.get(url).then(response => response.data);
  }
  GetHistory() {
    const historyUrl = `${API_URL}/api/history/`;
    return axios.get(historyUrl).then(response => response.data);
  }
}
