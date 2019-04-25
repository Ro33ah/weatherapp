import axios from "axios";

const API_URL = "https://localhost:5001";

export class APIService {
  getWeather(event) {
    const url = `${API_URL}/api/forecast/${event}/`;
    return axios.get(url).then(response => response.data);
  }
}
