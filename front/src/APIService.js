import axios from "axios";

const API_URL = "https://localhost:5001";

export class APIService {
  getWeather(event) {
    const url = `${API_URL}/data/2.5/random/${event}/`;
    return axios.get(url).then(response => response.data);
  }
}
