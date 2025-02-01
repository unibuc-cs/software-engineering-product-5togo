import { Component, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-location-weather',
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './location-weather.component.html',
  styleUrl: './location-weather.component.css'
})

export class LocationWeatherComponent {
  isNavActive = false;
  weatherData: any = null;
  searchQuery = '';
  bookmarkedLocations:Set<string> = new Set();
  next5DaysWeather = {
    "$id": "1",
    "id": 31,
    "locationName": "Arad",
    "forecastDate": "2025-02-01T00:00:00",
    "forecasts": {
      "$id": "2",
      "$values": [
        {
          "$id": "3",
          "id": 151,
          "date": "2025-02-02T00:00:00",
          "minTemperature": 0,
          "maxTemperature": 7,
          "condition": "CER PARTIAL NOROS",
          "weatherForecastId": 31,
          "weatherForecast": {
            "$ref": "1"
          }
        },
        {
          "$id": "4",
          "id": 152,
          "date": "2025-02-03T00:00:00",
          "minTemperature": -3,
          "maxTemperature": 4,
          "condition": "CER VARIABIL",
          "weatherForecastId": 31,
          "weatherForecast": {
            "$ref": "1"
          }
        },
        {
          "$id": "5",
          "id": 153,
          "date": "2025-02-04T00:00:00",
          "minTemperature": -5,
          "maxTemperature": 4,
          "condition": "CER VARIABIL",
          "weatherForecastId": 31,
          "weatherForecast": {
            "$ref": "1"
          }
        },
        {
          "$id": "6",
          "id": 154,
          "date": "2025-02-05T00:00:00",
          "minTemperature": -4,
          "maxTemperature": 4,
          "condition": "CER VARIABIL",
          "weatherForecastId": 31,
          "weatherForecast": {
            "$ref": "1"}
        }
      ]
    }
  };
  forecasts: any = null;

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {
    this.loadBookmarkedLocations();

    if(!this.searchQuery) {
      this.searchQuery = "ADAMCLISI";
    }

    if (this.next5DaysWeather?.forecasts?.$values?.length) {
      this.forecasts = this.next5DaysWeather.forecasts.$values;
      console.log(this.forecasts);
    }

    this.searchLocation();

    this.searchQuery = '';
  }

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }

  searchLocation() {
    console.log('Searching for:', this.searchQuery);

    if (!this.searchQuery) return;

    this.http
      .get(`http://localhost:5114/api/Weather/location/${this.searchQuery}`)
      .subscribe(
        (data: any) => {
          this.weatherData = data; // Update weather data
          console.log('Weather data received:', this.weatherData);
        },
        (error) => {
          console.error('Error fetching weather data:', error);
          alert('Location not found or an error occurred.');
        }
      );

  }

  favoriteSearch(location: any) {
    this.searchQuery = location;
    this.searchLocation();
    this.searchQuery = '';
  }

  addToFavorites() {
    if (!this.weatherData?.locationName) return;

    const locationName = this.weatherData.locationName;
    const token = localStorage.getItem('token');

    console.log("Token JWT:", token);
    if (!token) {
      alert("You are not authenticated!");
      return;
    }

    console.log(this.bookmarkedLocations);

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });


    this.http
      .post('http://localhost:5100/api/FavoriteLocations/add-to-favourites', { locationName }, { headers })
      .subscribe(
        (response: any) => {
          alert("Adaugat la favorite!");
          this.loadBookmarkedLocations();
          console.log('Added to favourites!');
        },
        (error) => {
          console.error('Error adding to favorites:', error);
          alert('Failed to add location to favorites.');
        }
      );
  }

  loadBookmarkedLocations() {
    const token = localStorage.getItem('token');

    console.log("Token JWT:", token);
    if (!token) {
      alert("You are not authenticated!");
      return;
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    this.http
      .get('http://localhost:5100/api/FavoriteLocations/get-favourites', { headers })
      .subscribe(
        (data: any) => {
          this.bookmarkedLocations = new Set(data);
          this.searchQuery = data[0];
        },
        (error) => {
          console.error('Error fetching bookmarked locations:', error);
        }
      );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  get bookmarkedLocationsArray() {
    return Array.from(this.bookmarkedLocations);
  }

  protected readonly console = console;
}

